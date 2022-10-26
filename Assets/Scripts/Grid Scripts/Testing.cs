using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Testing : MonoBehaviour
{

    [SerializeField] private GameObject Object1, Object2, pcInstance, iftsInstance, ifthInstance, iftkInstance, twhInstance, twsInstance, wallInstance;
    [SerializeField] private TextMeshProUGUI tcredits, tecredits;
    private bool SelectionActive = false;
    private Pathfinding pathfinding;
    List<PathNode> minpath;
    private int type = 3, credits, cost, enemycredits, enemycost, random, randomx, randomy, minpathCost;
    private GameState _gameState = GameState.prepare;
    #region Lists
    private List<GameObject> PWUnits = new List<GameObject>();
    private List<GameObject> TWHUnits = new List<GameObject>();
    private List<GameObject> TWSUnits = new List<GameObject>();
    private List<GameObject> IFTSUnits = new List<GameObject>();
    private List<GameObject> IFTHUnits = new List<GameObject>();
    private List<GameObject> IFTKUnits = new List<GameObject>();
    #endregion
    #region ObjectsScripts
    private PowerCore pcscript;
    private TowerSmall twsscript;
    private TowerHeavy twhscript;
    private InfantrySmall iftsscript;
    private InfantryHeavy ifthscript;
    private InfantryKiller iftkscript;
    #endregion
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
            pathfinding.GetNode(15, i).SetIsWalkable(!pathfinding.GetNode(15, i).isWalkable);
            Instantiate(wallInstance, pathfinding.GetGrid().GetWorldPosition(15, i) + new Vector3(pathfinding.GetGrid().GetCellSize(),
                        pathfinding.GetGrid().GetCellSize()) * .5f, Quaternion.identity);
        }
        #endregion
        #region PowerCore
        Object1 = Instantiate(pcInstance, pathfinding.GetGrid().GetWorldPosition(17, 2) + new Vector3(pathfinding.GetGrid().GetCellSize(),
                              pathfinding.GetGrid().GetCellSize()) * .5f, Quaternion.identity);
        pcscript = Object1.GetComponent<PowerCore>();
        pcscript.UpdatePosition(17, 2);
        pathfinding.GetGrid().SetGridObject(pathfinding.GetGrid().GetWorldPosition(17, 2), new PathNode(pathfinding.GetGrid(), 17, 2, 0));
        pathfinding.GetNode(17, 2).SetIsWalkable(!pathfinding.GetNode(17, 2).isWalkable);
        PWUnits.Add(Object1);
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
                    if (enemycredits - enemycost >= 0)
                    {
                        Object1 = Instantiate(twsInstance, pathfinding.GetGrid().GetWorldPosition(randomx, randomy) + new Vector3(pathfinding.GetGrid().GetCellSize(),
                                          pathfinding.GetGrid().GetCellSize()) * .5f, Quaternion.identity);
                        twsscript = Object1.GetComponent<TowerSmall>();
                        twsscript.UpdatePosition(randomx, randomy);
                        TWSUnits.Add(Object1);
                        pathfinding.GetGrid().SetGridObject(pathfinding.GetGrid().GetWorldPosition(randomx, randomy),
                                                            new PathNode(pathfinding.GetGrid(), randomx, randomy, 0));
                        pathfinding.GetNode(randomx, randomy).SetIsWalkable(!pathfinding.GetNode(randomx, randomy).isWalkable);
                        enemycredits -= enemycost;
                    }
                }
                else
                {
                    enemycost = 3;
                    if (enemycredits - enemycost >= 0)
                    {
                        Object1 = Instantiate(twhInstance, pathfinding.GetGrid().GetWorldPosition(randomx, randomy) + new Vector3(pathfinding.GetGrid().GetCellSize(),
                                          pathfinding.GetGrid().GetCellSize()) * .5f, Quaternion.identity);
                        twhscript = Object1.GetComponent<TowerHeavy>();
                        twhscript.UpdatePosition(randomx, randomy);
                        TWHUnits.Add(Object1);
                        pathfinding.GetGrid().SetGridObject(pathfinding.GetGrid().GetWorldPosition(randomx, randomy),
                                                            new PathNode(pathfinding.GetGrid(), randomx, randomy, 0));
                        pathfinding.GetNode(randomx, randomy).SetIsWalkable(!pathfinding.GetNode(randomx, randomy).isWalkable);
                        enemycredits -= enemycost;
                    }
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
                if (Input.GetMouseButtonDown(0) && SelectionActive == true)
                {
                    if (pathfinding.GetGrid().GetGridObject(GetMouseWorldPosition()).GetValue() == 1)
                    {
                        switch (type)
                        {
                            case (0):
                                cost = 1;
                                if (credits >= cost)
                                {
                                    pathfinding.GetGrid().GetXY(GetMouseWorldPosition(), out x, out y);
                                    pathfinding.GetGrid().SetGridObject(GetMouseWorldPosition(), new PathNode(pathfinding.GetGrid(), x, y, 0));
                                    Object1 = Instantiate(iftsInstance, pathfinding.GetGrid().GetWorldPosition(x, y) + new Vector3(pathfinding.GetGrid().GetCellSize(),
                                                          pathfinding.GetGrid().GetCellSize()) * .5f, Quaternion.identity);
                                    iftsscript = Object1.GetComponent<InfantrySmall>();
                                    iftsscript.UpdatePosition(x, y);
                                    IFTSUnits.Add(Object1);
                                    pathfinding.GetNode(x, y).SetIsWalkable(!pathfinding.GetNode(x, y).isWalkable);
                                    credits -= cost;
                                }
                                break;
                            case (1):
                                cost = 2;
                                if (credits >= cost)
                                {
                                    pathfinding.GetGrid().GetXY(GetMouseWorldPosition(), out x, out y);
                                    pathfinding.GetGrid().SetGridObject(GetMouseWorldPosition(), new PathNode(pathfinding.GetGrid(), x, y, 0));
                                    Object1 = Instantiate(ifthInstance, pathfinding.GetGrid().GetWorldPosition(x, y) + new Vector3(pathfinding.GetGrid().GetCellSize(),
                                                          pathfinding.GetGrid().GetCellSize()) * .5f, Quaternion.identity);
                                    ifthscript = Object1.GetComponent<InfantryHeavy>();
                                    ifthscript.UpdatePosition(x, y); ;
                                    IFTHUnits.Add(Object1);
                                    pathfinding.GetNode(x, y).SetIsWalkable(!pathfinding.GetNode(x, y).isWalkable);
                                    credits -= cost;
                                }
                                break;
                            case (2):
                                cost = 3;
                                if (credits >= cost)
                                {
                                    pathfinding.GetGrid().GetXY(GetMouseWorldPosition(), out x, out y);
                                    pathfinding.GetGrid().SetGridObject(GetMouseWorldPosition(), new PathNode(pathfinding.GetGrid(), x, y, 0));
                                    Object1 = Instantiate(iftkInstance, pathfinding.GetGrid().GetWorldPosition(x, y) + new Vector3(pathfinding.GetGrid().GetCellSize(),
                                                          pathfinding.GetGrid().GetCellSize()) * .5f, Quaternion.identity);
                                    iftkscript = Object1.GetComponent<InfantryKiller>();
                                    iftkscript.UpdatePosition(x, y);
                                    IFTKUnits.Add(Object1);
                                    pathfinding.GetNode(x, y).SetIsWalkable(!pathfinding.GetNode(x, y).isWalkable);
                                    credits -= cost;
                                }
                                break;
                        }
                    }
                    SelectionState(false);
                }
                #endregion
                break;
            #endregion
            #region Play
            case GameState.play:
                #region InfantrySmall
                foreach (GameObject currentUnit in IFTSUnits)
                {
                    minpathCost = 999999999;
                    foreach (GameObject enemyUnit in TWSUnits)
                    {
                        List<PathNode> path = pathfinding.FindPath(currentUnit.GetComponent<InfantrySmall>().GetX(),
                                                                   currentUnit.GetComponent<InfantrySmall>().GetY(),
                                                                   enemyUnit.GetComponent<TowerSmall>().GetX(),
                                                                   enemyUnit.GetComponent<TowerSmall>().GetY());
                        if (path != null)
                        {
                            if(pathfinding.GetPathCost() < minpathCost)
                            {
                                minpathCost = pathfinding.GetPathCost();
                                minpath = path;
                            }
                        }
                    }
                    foreach (GameObject enemyUnit in TWHUnits)
                    {
                        List<PathNode> path = pathfinding.FindPath(currentUnit.GetComponent<InfantrySmall>().GetX(),
                                                                   currentUnit.GetComponent<InfantrySmall>().GetY(),
                                                                   enemyUnit.GetComponent<TowerHeavy>().GetX(),
                                                                   enemyUnit.GetComponent<TowerHeavy>().GetY());
                        if (path != null)
                        {
                            if (pathfinding.GetPathCost() < minpathCost)
                            {
                                minpathCost = pathfinding.GetPathCost();
                                minpath = path;
                            }
                        }
                    }
                    foreach (GameObject enemyUnit in PWUnits)
                    {
                        List<PathNode> path = pathfinding.FindPath(currentUnit.GetComponent<InfantrySmall>().GetX(),
                                                                   currentUnit.GetComponent<InfantrySmall>().GetY(),
                                                                   enemyUnit.GetComponent<PowerCore>().GetX(),
                                                                   enemyUnit.GetComponent<PowerCore>().GetY());
                        if (path != null)
                        {
                            if (pathfinding.GetPathCost() < minpathCost)
                            {
                                minpathCost = pathfinding.GetPathCost();
                                minpath = path;
                            }
                        }
                    }
                    for (int i = 0; i < minpath.Count - 1; i++)
                    {
                        Debug.DrawLine(new Vector3(minpath[i].GetX(), minpath[i].GetY()) * 10f + Vector3.one * 5f, new Vector3(minpath[i + 1].GetX(),
                                       minpath[i + 1].GetY()) * 10f + Vector3.one * 5f, Color.yellow, 5f);
                    }
                    currentUnit.GetComponent<InfantrySmall>().SetTargetPosition(new Vector3(minpath[minpath.Count - 1].GetX(), minpath[minpath.Count - 1].GetY()) * 10f + Vector3.one * 5f);
                }
                #endregion
                #region InfantryHeavy
                foreach (GameObject currentUnit in IFTHUnits)
                {
                    minpathCost = 999999999;
                    foreach (GameObject enemyUnit in TWSUnits)
                    {
                        List<PathNode> path = pathfinding.FindPath(currentUnit.GetComponent<InfantryHeavy>().GetX(),
                                                                   currentUnit.GetComponent<InfantryHeavy>().GetY(),
                                                                   enemyUnit.GetComponent<TowerSmall>().GetX(),
                                                                   enemyUnit.GetComponent<TowerSmall>().GetY());
                        if (path != null)
                        {
                            if (pathfinding.GetPathCost() < minpathCost)
                            {
                                minpathCost = pathfinding.GetPathCost();
                                minpath = path;
                            }
                        }
                    }
                    foreach (GameObject enemyUnit in TWHUnits)
                    {
                        List<PathNode> path = pathfinding.FindPath(currentUnit.GetComponent<InfantryHeavy>().GetX(),
                                                                   currentUnit.GetComponent<InfantryHeavy>().GetY(),
                                                                   enemyUnit.GetComponent<TowerHeavy>().GetX(),
                                                                   enemyUnit.GetComponent<TowerHeavy>().GetY());
                        if (path != null)
                        {
                            if (pathfinding.GetPathCost() < minpathCost)
                            {
                                minpathCost = pathfinding.GetPathCost();
                                minpath = path;
                            }
                        }
                    }
                    foreach (GameObject enemyUnit in PWUnits)
                    {
                        List<PathNode> path = pathfinding.FindPath(currentUnit.GetComponent<InfantryHeavy>().GetX(),
                                                                   currentUnit.GetComponent<InfantryHeavy>().GetY(),
                                                                   enemyUnit.GetComponent<PowerCore>().GetX(),
                                                                   enemyUnit.GetComponent<PowerCore>().GetY());
                        if (path != null)
                        {
                            if (pathfinding.GetPathCost() < minpathCost)
                            {
                                minpathCost = pathfinding.GetPathCost();
                                minpath = path;
                            }
                        }
                    }
                    for (int i = 0; i < minpath.Count - 1; i++)
                    {
                        Debug.DrawLine(new Vector3(minpath[i].GetX(), minpath[i].GetY()) * 10f + Vector3.one * 5f, new Vector3(minpath[i + 1].GetX(),
                                       minpath[i + 1].GetY()) * 10f + Vector3.one * 5f, Color.blue, 5f);
                    }
                    currentUnit.GetComponent<InfantryHeavy>().SetTargetPosition(new Vector3(minpath[minpath.Count - 1].GetX(), minpath[minpath.Count - 1].GetY()) * 10f + Vector3.one * 5f);
                }
                #endregion
                #region InfantryKiller
                foreach (GameObject currentUnit in IFTKUnits)
                {
                    minpathCost = 999999999;
                    foreach (GameObject enemyUnit in PWUnits)
                    {
                        List<PathNode> path = pathfinding.FindPath(currentUnit.GetComponent<InfantryKiller>().GetX(),
                                                                   currentUnit.GetComponent<InfantryKiller>().GetY(),
                                                                   enemyUnit.GetComponent<PowerCore>().GetX(),
                                                                   enemyUnit.GetComponent<PowerCore>().GetY());
                        if (path != null)
                        {
                            if (pathfinding.GetPathCost() < minpathCost)
                            {
                                minpathCost = pathfinding.GetPathCost();
                                minpath = path;
                            }
                        }
                    }
                    Debug.Log("Objetivo: " + minpath[minpath.Count - 1].GetX() + " " + minpath[minpath.Count - 1].GetY());
                    for (int i = 0; i < minpath.Count - 1; i++)
                    {
                        Debug.DrawLine(new Vector3(minpath[i].GetX(), minpath[i].GetY()) * 10f + Vector3.one * 5f, new Vector3(minpath[i + 1].GetX(),
                                       minpath[i + 1].GetY()) * 10f + Vector3.one * 5f, Color.black, 5f);
                    }
                }
                #endregion
                if (Input.GetMouseButtonDown(0))
                {
                    pathfinding.GetGrid().GetXY(GetMouseWorldPosition(), out x, out y);
                    foreach (GameObject unit in TWSUnits)
                    {
                        if(unit.GetComponent<TowerSmall>().GetX() == x && unit.GetComponent<TowerSmall>().GetY() == y)
                        {
                            TWSUnits.Remove(unit);
                            unit.GetComponent<TowerSmall>().Delete(pathfinding.GetGrid());
                        }
                    }
                    foreach (GameObject unit in TWHUnits)
                    {
                        if (unit.GetComponent<TowerHeavy>().GetX() == x && unit.GetComponent<TowerHeavy>().GetY() == y)
                        {
                            TWHUnits.Remove(unit);
                            unit.GetComponent<TowerHeavy>().Delete(pathfinding.GetGrid());
                        }
                    }
                }
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
        SelectionState(true);
    }

    public void SetType1()
    {
        type = 1;
        SelectionState(true);
    }

    public void SetType2()
    {
        type = 2;
        SelectionState(true);
    }

    public void SelectionState(bool State)
    {
        SelectionActive = State;
    }

    public void StartGame()
    {
        UpdateGameState(GameState.play);
    }
    #endregion
}
