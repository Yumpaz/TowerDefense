using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerSmall : MonoBehaviour
{
    private int life, attack, speed, cost, range, type, x, y;
    [SerializeField] private GameObject bullet, bulletInstance;
    private List<PathNode> NodesInRange;
    private bool canShoot = true;

    void Start()
    {
        life = 3;
        attack = 2;
        speed = 0;
        cost = 2;
        range = 3;
    }

    #region PositionFunctions
    public void UpdatePosition(int x, int y)
    {
        this.x = x;
        this.y = y;
    }

    public int GetX()
    {
        return x;
    }

    public int GetY()
    {
        return y;
    }

    public Vector3 GetPosition()
    {
        return transform.position;
    }
    #endregion

    #region AttackFunctions
    private void GetCurrentNode()
    {
        PathNode currentNode = Pathfinding.Instance.GetGrid().GetGridObject(GetPosition());
        NodesInRange = Pathfinding.Instance.GetNodesInRange(currentNode, range);
        StartShooting();
    }

    private void StartShooting()
    {
        foreach (PathNode node in NodesInRange)
        {
            if (node != null && node.GetValue() == -1 && canShoot == true)
            {
                Debug.Log(node.GetValue());
                canShoot = false;
                StartAttacking(node);
            }
        }
    }

    public void StartAttacking(PathNode node)
    {
        bulletInstance = Instantiate(bullet, Pathfinding.Instance.GetGrid().GetWorldPosition(x, y) + new Vector3(Pathfinding.Instance.GetGrid().GetCellSize(),
                                     Pathfinding.Instance.GetGrid().GetCellSize()) * .5f, Quaternion.identity);
        Vector3 shootDir = (Pathfinding.Instance.GetGrid().GetWorldPosition(node.GetX(), node.GetY()) - Pathfinding.Instance.GetGrid().GetWorldPosition(x, y)).normalized;
        bulletInstance.GetComponent<BulletBehaviour>().Setup(shootDir, attack);
        StartCoroutine(SpawnBulletTimer());
    }

    public IEnumerator SpawnBulletTimer()
    {
        yield return new WaitForSeconds(3f);
        canShoot = true;
    }
    #endregion

    #region DamageFunctions
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Bullet")
        {
            Debug.Log("ReceiveDamage");
            int damage = collision.gameObject.GetComponent<BulletBehaviour>().GetDamage();
            LoseHealth(damage);
            Destroy(collision.gameObject);
        }
    }

    public void LoseHealth(int damage)
    {
        life -= damage;
    }
    #endregion
    public void Delete(Grid<PathNode> grid)
    {
        grid.SetGridObject(x, y, new PathNode(grid, x, y, 2));
        Testing.Instance.TWSUnits.Remove(this.gameObject);
        Destroy(this.gameObject);
    }

    public void Erase(Grid<PathNode> grid)
    {
        grid.SetGridObject(x, y, new PathNode(grid, x, y, 2));
        Destroy(this.gameObject);
    }

    private void Update()
    {
        GetCurrentNode();
        if (life <= 0)
        {
            Delete(Pathfinding.Instance.GetGrid());
        }
    }
}
