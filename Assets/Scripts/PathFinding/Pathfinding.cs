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
    private int pathCost;

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

    public List<PathNode> FindPath(int startX, int startY, int endX, int endY)
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
                pathNode.gCost = 999999999;
                pathNode.CalculatefCost();
                pathNode.cameFromNode = null;
            }
        }

        startNode.gCost = 0;
        startNode.hCost = CalculateDistanceCost(startNode, endNode);
        startNode.CalculatefCost();

        while (openList.Count > 0)
        {
            PathNode currentNode = GetLowestfCostNode(openList);
            if (currentNode == endNode)
            {
                Debug.Log("PathFound");
                return CalculatePath(endNode);
            }
            openList.Remove(currentNode);
            closedList.Add(currentNode);
            foreach (PathNode neighbourNode in GetNeighbourList(currentNode))
            {
                if (!closedList.Contains(neighbourNode))
                {
                    if (!neighbourNode.isWalkable && neighbourNode != endNode)
                    {
                        closedList.Add(neighbourNode);
                    }
                    else
                    {
                        int tentativegCost = currentNode.gCost + CalculateDistanceCost(currentNode, neighbourNode);
                        if (tentativegCost < neighbourNode.gCost)
                        {
                            neighbourNode.cameFromNode = currentNode;
                            neighbourNode.gCost = tentativegCost;
                            neighbourNode.hCost = CalculateDistanceCost(neighbourNode, endNode);
                            neighbourNode.CalculatefCost();

                            if (!openList.Contains(neighbourNode))
                            {
                                openList.Add(neighbourNode);
                            }
                        }
                    }
                }
            }
        }
        //Out of nodes on the openList
        return null;
    }

    public PathNode GetNode(int x, int y)
    {
        return grid.GetGridObject(x, y);
    }

    private List<PathNode> GetNeighbourList(PathNode currentNode)
    {
        List<PathNode> neighbourList = new List<PathNode>();
        if (currentNode.GetX() - 1 >= 0)
        {
            //Left
            neighbourList.Add(GetNode(currentNode.GetX() - 1, currentNode.GetY()));
            //Left Down
            if (currentNode.GetY() - 1 >= 0)
            {
                neighbourList.Add(GetNode(currentNode.GetX() - 1, currentNode.GetY() - 1));
            }
            //Left Up
            if (currentNode.GetY() + 1 < grid.GetHeight())
            {
                neighbourList.Add(GetNode(currentNode.GetX() - 1, currentNode.GetY() + 1));
            }
        }
        if (currentNode.GetX() + 1 < grid.GetWidth())
        {
            //Right
            neighbourList.Add(GetNode(currentNode.GetX() + 1, currentNode.GetY()));
            //Right Down
            if(currentNode.GetY() - 1 >= 0)
            {
                neighbourList.Add(GetNode(currentNode.GetX() + 1, currentNode.GetY() - 1));
            }
            //Right Up
            if (currentNode.GetY() + 1 < grid.GetHeight())
            {
                neighbourList.Add(GetNode(currentNode.GetX() + 1, currentNode.GetY() + 1));
            }
        }
        //Down
        if (currentNode.GetY() - 1 >= 0)
        {
            neighbourList.Add(GetNode(currentNode.GetX(), currentNode.GetY() - 1));
        }
        //Up
        if (currentNode.GetY() + 1 < grid.GetHeight())
        {
            neighbourList.Add(GetNode(currentNode.GetX(), currentNode.GetY() + 1));
        }

        return neighbourList;
    }

    private List<PathNode> CalculatePath(PathNode endNode)
    {
        pathCost = 0;
        List<PathNode> path = new List<PathNode>();
        path.Add(endNode);
        pathCost += endNode.fCost;
        PathNode currentNode = endNode;
        while (currentNode.cameFromNode != null)
        {
            path.Add(currentNode.cameFromNode);
            currentNode = currentNode.cameFromNode;
            pathCost += currentNode.fCost;
        }
        path.Reverse();
        return path;
    }

    public int GetPathCost()
    {
        return pathCost;
    }

    private int CalculateDistanceCost(PathNode a, PathNode b)
    {
        int xDistance = Mathf.Abs(a.GetX() - b.GetX());
        int yDistance = Mathf.Abs(a.GetY() - b.GetY());
        int remaining = Mathf.Abs(xDistance - yDistance);
        return MOVE_DIAGONAL_COST * Mathf.Min(xDistance, yDistance) + MOVE_STRAIGHT_COST * remaining;

    }

    private PathNode GetLowestfCostNode(List<PathNode> pathNodeList)
    {
        PathNode lowestfCostNode = pathNodeList[0];
        for (int i = 1; i < pathNodeList.Count; i++)
        {
            if (pathNodeList[i].fCost < lowestfCostNode.fCost)
            {
                lowestfCostNode = pathNodeList[i];
            }
        }
        return lowestfCostNode;
    }
}
