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

    public void Delete(Grid<PathNode> grid)
    {
        grid.SetGridObject(x, y, new PathNode(grid, x, y, 2));
        Destroy(this.gameObject);
    }

    private void Update()
    {
        
    }
}
