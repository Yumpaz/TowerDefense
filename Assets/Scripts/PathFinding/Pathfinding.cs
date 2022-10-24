using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinding
{

    private const int MOVE_STRAIGHT_COST = 10;
    private const int MOVE_DIAGONAL_COST = 14;
    
    private Grid<PathNode> grid;
    private List<PathNode> openList;
    private List<PathNode> closedList;

    public Pathfinding(int width, int height, int value)
    {
        grid = new Grid<PathNode>(width, height, 10f, (Grid<PathNode> g, int x, int y) => new PathNode(g, x, y, value));
    }

    public void SetNodeValue(int x, int y, PathNode value)
    {
        grid.SetGridObject(x, y, value);
    }

    public Grid<PathNode> GetGrid()
    {
        return grid;
    }

    private List<PathNode> FindPath(int startX, int startY, int endX, int endY)
    {
        PathNode startNode = grid.GetGridObject(startX, startY);
        PathNode endNode = grid.GetGridObject(endX, endY);

        openList = new List<PathNode> { startNode };
        closedList = new List<PathNode>();

        for (int i=0; i < grid.GetWidth(); i++)
        {
            for (int j=0; j < grid.GetHeight(); j++)
            {
                PathNode pathNode = grid.GetGridObject(i, j);
                pathNode.gCost = int.MaxValue;
                pathNode.CalculatefCost();
                pathNode.cameFromNode = null;
            }
        }

        startNode.gCost = 0;
        startNode.hCost = CalculateDistanceCost(startNode, endNode);
        startNode.CalculatefCost();

        while (openList.Count > 0)
        {

        }
    }

    private int CalculateDistanceCost(PathNode a, PathNode b)
    {
        int xDistance = Mathf.Abs(a.GetX() - b.GetX());
        int yDistance = Mathf.Abs(a.GetY() - b.GetY());
        int remaining = Mathf.Abs(xDistance - yDistance);
        return MOVE_DIAGONAL_COST * Mathf.Min(xDistance, yDistance) + MOVE_STRAIGHT_COST * remaining;

    }
}
