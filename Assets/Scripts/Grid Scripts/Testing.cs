using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Testing : MonoBehaviour
{

    [SerializeField] private GameObject pcInstance;
    [SerializeField] private GameObject iftsInstance, ifthInstance, iftkInstance, twhInstance, twsInstance;
    [SerializeField] private TextMeshProUGUI tcredits, tecredits;
    Pathfinding pathfinding;
    private int type = 3, credits, cost, enemycredits, enemycost, random, random2, randomx, randomy;
    private GameState _gameState = GameState.prepare;
    //private GameObject pcObject, iftObject;
    private int x, y;

    void Start()
    {
        #region BuildingGrid
        pathfinding = new Pathfinding(18, 5, 0);
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
        #endregion
        #region TestingObjects
        Instantiate(pcInstance, pathfinding.GetGrid().GetWorldPosition(17, 2) + new Vector3(pathfinding.GetGrid().GetCellSize(),
                    pathfinding.GetGrid().GetCellSize()) * .5f, Quaternion.identity);
        pathfinding.GetGrid().SetGridObject(pathfinding.GetGrid().GetWorldPosition(17, 2), new PathNode(pathfinding.GetGrid(), randomx, randomy, 0));
        #endregion
        credits = 15;
        enemycredits = 15;
        #region EnemyPrepare
        while (enemycredits - enemycost >= 0)
        {
            tecredits.text = "Enemy Credits: " + enemycredits;
            randomx = Random.Range(12, 18);
            randomy = Random.Range(0, 5);
            if (pathfinding.GetGrid().GetGridObject(randomx, randomy).GetValue() == 2)
            {
                random = Random.Range(0, 2);
                if (random == 0)
                {
                    enemycost = 2;
                    Instantiate(twsInstance, pathfinding.GetGrid().GetWorldPosition(randomx, randomy) + new Vector3(pathfinding.GetGrid().GetCellSize(), 
                                pathfinding.GetGrid().GetCellSize()) * .5f, Quaternion.identity);
                    pathfinding.GetGrid().SetGridObject(pathfinding.GetGrid().GetWorldPosition(randomx, randomy), 
                                                        new PathNode(pathfinding.GetGrid(), randomx, randomy, 0));
                    enemycredits -= enemycost;
                }
                else
                {
                    enemycost = 3;
                    Instantiate(twhInstance, pathfinding.GetGrid().GetWorldPosition(randomx, randomy) + new Vector3(pathfinding.GetGrid().GetCellSize(),
                                pathfinding.GetGrid().GetCellSize()) * .5f, Quaternion.identity);
                    pathfinding.GetGrid().SetGridObject(pathfinding.GetGrid().GetWorldPosition(randomx, randomy),
                                                        new PathNode(pathfinding.GetGrid(), randomx, randomy, 0));
                    enemycredits -= enemycost;
                }
            }
            tecredits.text = "Enemy Credits: " + enemycredits;
        }
        #endregion
    }

    private void Update()
    {
        switch (_gameState)
        {
            #region Prepare
            case GameState.prepare:
                #region PlayerPrepare
                tcredits.text = "Credits: " + credits;
                if (Input.GetMouseButtonDown(0))
                {
                    if (pathfinding.GetGrid().GetGridObject(GetMouseWorldPosition()).GetValue() == 1)
                    {
                        switch (type)
                        {
                            case (0):
                                cost = 1;
                                if (credits >= cost)
                                {
                                    pathfinding.GetGrid().SetGridObject(GetMouseWorldPosition(), new PathNode(pathfinding.GetGrid(), randomx, randomy, 0));
                                    pathfinding.GetGrid().GetXY(GetMouseWorldPosition(), out x, out y);
                                    Instantiate(iftsInstance, pathfinding.GetGrid().GetWorldPosition(x, y) + new Vector3(pathfinding.GetGrid().GetCellSize(),
                                                pathfinding.GetGrid().GetCellSize()) * .5f, Quaternion.identity);
                                    credits -= cost;
                                }
                                break;
                            case (1):
                                cost = 2;
                                if (credits >= cost)
                                {
                                    pathfinding.GetGrid().SetGridObject(GetMouseWorldPosition(), new PathNode(pathfinding.GetGrid(), randomx, randomy, 0));
                                    pathfinding.GetGrid().GetXY(GetMouseWorldPosition(), out x, out y);
                                    Instantiate(ifthInstance, pathfinding.GetGrid().GetWorldPosition(x, y) + new Vector3(pathfinding.GetGrid().GetCellSize(),
                                                pathfinding.GetGrid().GetCellSize()) * .5f, Quaternion.identity);
                                    credits -= cost;
                                }
                                break;
                            case (2):
                                cost = 3;
                                if (credits >= cost)
                                {
                                    pathfinding.GetGrid().SetGridObject(GetMouseWorldPosition(), new PathNode(pathfinding.GetGrid(), randomx, randomy, 0));
                                    pathfinding.GetGrid().GetXY(GetMouseWorldPosition(), out x, out y);
                                    Instantiate(iftkInstance, pathfinding.GetGrid().GetWorldPosition(x, y) + new Vector3(pathfinding.GetGrid().GetCellSize(),
                                                pathfinding.GetGrid().GetCellSize()) * .5f, Quaternion.identity);
                                    credits -= cost;
                                }
                                break;
                        }
                    }
                }
                #endregion
                break;
            #endregion
            #region Play
            case GameState.play:

                break;
            #endregion
            #region End
            case GameState.end:

                break;
            #endregion
        }
    }

    public enum GameState
    {
        prepare,
        play,
        end
    }

    public void UpdateGameState(GameState gameState)
    {
        _gameState = gameState;
    }

    #region Utils
    public static Vector3 GetMouseWorldPosition()
    {
        Vector3 vec = GetMouseWorldPositionWithZ(Input.mousePosition, Camera.main);
        vec.z = 0f;
        return vec;
    }
    public static Vector3 GetMouseWorldPositionWithZ()
    {
        return GetMouseWorldPositionWithZ(Input.mousePosition, Camera.main);
    }
    public static Vector3 GetMouseWorldPositionWithZ(Camera worldCamera)
    {
        return GetMouseWorldPositionWithZ(Input.mousePosition, worldCamera);
    }
    public static Vector3 GetMouseWorldPositionWithZ(Vector3 screenPosition, Camera worldCamera)
    {
        Vector3 worldPosition = worldCamera.ScreenToWorldPoint(screenPosition);
        return worldPosition;
    }

    public void SetType0()
    {
        type = 0;
    }

    public void SetType1()
    {
        type = 1;
    }

    public void SetType2()
    {
        type = 2;
    }
    #endregion
}
