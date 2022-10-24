using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathNode : MonoBehaviour
{

    private Grid<PathNode> grid;
    private int x, y, value;

    public int gCost, hCost, fCost;

    public PathNode cameFromNode;

    public PathNode(Grid<PathNode> grid, int x, int y, int value)
    {
        this.grid = grid;
        this.x = x;
        this.y = y;
        this.value = value;
    }

    public int GetValue()
    {
        return value;
    }

    public void SetValue(int value)
    {
        this.value = value;
    }

    public void CalculatefCost()
    {
        fCost = gCost + hCost;
    }

    public override string ToString()
    {
        return value.ToString();
    }
}
