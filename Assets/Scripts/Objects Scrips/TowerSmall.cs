using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerSmall : MonoBehaviour
{
    private int life, attack, speed, cost, range, type, x, y;

    void Start()
    {
        life = 3;
        attack = 2;
        speed = 0;
        cost = 2;
        range = 2;
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
