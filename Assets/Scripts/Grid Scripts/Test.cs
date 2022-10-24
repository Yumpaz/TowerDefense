using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    void Start()
    {
        Pathfinding pathfinding = new Pathfinding(18, 5, 0);
        for (int i = 0; i < 6; i++)
        {
            for (int j = 0; j < 5; j++)
            {
                pathfinding.SetNodeValue(i, j, new PathNode(pathfinding.GetGrid(), i, j, 1));
            }
        }

        for (int i = 17; i >= 12; i--)
        {
            for (int j = 0; j < 5; j++)
            {
                pathfinding.SetNodeValue(i, j, new PathNode(pathfinding.GetGrid(), i, j, 2));
            }
        }

        for (int i = 1; i < 4; i++)
        {
            pathfinding.SetNodeValue(15, i, new PathNode(pathfinding.GetGrid(), 15, i, 3));
        }
    }
}
