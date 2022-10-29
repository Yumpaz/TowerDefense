using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfantrySmall : MonoBehaviour
{
    private int life, attack, speed, cost, range, type, x, y, currentPathIndex;
    private List<Vector3> pathVectorList;
    [SerializeField] private GameObject bullet, bulletInstance;
    private BulletBehaviour bulletscript;
    private List<PathNode> NodesInRange;
    private bool canMove = true;

    void Start()
    {
        life = 4;
        attack = 1;
        speed = 2;
        cost = 1;
        range = 2;
        speed *= 7;
    }

    #region PositionsFunctions
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

    #region MovementFunctions
    public Vector3 GetPosition()
    {
        return transform.position;
    }

    public void SetTargetPosition(Vector3 targetPosition)
    {
        currentPathIndex = 0;
        pathVectorList = Pathfinding.Instance.FindPath(GetPosition(), targetPosition);

        if (pathVectorList != null && pathVectorList.Count > 1)
        {
            pathVectorList.RemoveAt(0);
        }
        Movement();
    }

    private void Movement()
    {
        if (pathVectorList != null && canMove)
        {
            Vector3 targetPosition = pathVectorList[currentPathIndex];
            if (Vector3.Distance(transform.position, targetPosition) > 1f)
            {
                Vector3 moveDir = (targetPosition - transform.position).normalized;
                float distanceBefore = Vector3.Distance(transform.position, targetPosition);
                transform.position = transform.position + speed * Time.deltaTime * moveDir;
            }
            else
            {
                currentPathIndex++;
                if (currentPathIndex >= pathVectorList.Count)
                {
                    StopMoving();
                }
            }
        }
    }

    private void StopMoving()
    {
        pathVectorList = null;
    }
    #endregion

    #region AttackFunctions
    private void GetCurrentNode()
    {
        PathNode currentNode = Pathfinding.Instance.GetGrid().GetGridObject(GetPosition());
        Debug.Log("Current Node: " + currentNode.GetX() + " " + currentNode.GetY());
        NodesInRange = Pathfinding.Instance.GetNodesInRange(currentNode, range);
        StartShooting();

    }

    private void StartShooting()
    {
        foreach (PathNode node in NodesInRange)
        {
            if (node.GetValue() == 4)
            {
                Debug.Log(node.GetValue());
                canMove = false;
                StartAttacking();
            }
        }
    }

    public void StartAttacking()
    {
        canMove = true;
    }
    #endregion
    public void Delete(Grid<PathNode> grid)
    {
        grid.SetGridObject(x, y, new PathNode(grid, x, y, 2));
        Destroy(this.gameObject);
    }

    private void FixedUpdate()
    {
        GetCurrentNode();
    }
}
