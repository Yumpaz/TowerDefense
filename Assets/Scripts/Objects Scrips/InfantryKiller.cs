using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfantryKiller : MonoBehaviour
{
    private int life, attack, speed, cost, range, type, x, y, currentPathIndex;
    private List<Vector3> pathVectorList;

    void Start()
    {
        life = 8;
        attack = 1;
        speed = 2;
        cost = 3;
        range = 1;
        speed *= 5;
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
        if (pathVectorList != null)
        {
            Vector3 targetPosition = pathVectorList[currentPathIndex];
            if (Vector3.Distance(transform.position, targetPosition) > 1f)
            {
                Vector3 moveDir = (targetPosition - transform.position).normalized;
                float distanceBefore = Vector3.Distance(transform.position, targetPosition);
                transform.position = transform.position + moveDir * speed * Time.deltaTime;
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
    public void Delete(Grid<PathNode> grid)
    {
        grid.SetGridObject(x, y, new PathNode(grid, x, y, 2));
        Destroy(this.gameObject);
    }

    private void Update()
    {
        
    }
}
