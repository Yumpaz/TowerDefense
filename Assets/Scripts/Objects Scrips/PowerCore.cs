using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerCore : MonoBehaviour
{
    private int life, cost, x, y;

    private void Awake()
    {
        life = 14;
        cost = 0;
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
        Testing.Instance.PWUnits.Remove(this.gameObject);
        Destroy(this.gameObject);
    }

    public void Erase(Grid<PathNode> grid)
    {
        grid.SetGridObject(x, y, new PathNode(grid, x, y, 2));
        Destroy(this.gameObject);
    }

    private void Update()
    {
        if (life <= 0)
        {
            Delete(Pathfinding.Instance.GetGrid());
        }
    }
}
