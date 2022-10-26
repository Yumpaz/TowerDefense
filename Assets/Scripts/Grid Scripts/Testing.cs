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
    private int type = 3, credits, cost, enemycredits, enemycost, random, randomx, randomy;
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
        #region TestingObjects
        Object1 = Instantiate(pcInstance, pathfinding.GetGrid().GetWorldPosition(17, 2) + new Vector3(pathfinding.GetGrid().GetCellSize(),
                              pathfinding.GetGrid().GetCellSize()) * .5f, Quaternion.identity);
        pcscript = Object1.GetComponent<PowerCore>();
        pcscript.UpdatePosition(17, 2);
        PWUnits.Add(Object1);
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
                    if (enemycredits - enemycost >= 0)
                    {
                        Object1 = Instantiate(twsInstance, pathfinding.GetGrid().GetWorldPosition(randomx, randomy) + new Vector3(pathfinding.GetGrid().GetCellSize(),
                                          pathfinding.GetGrid().GetCellSize()) * .5f, Quaternion.identity);
                        twsscript = Object1.GetComponent<TowerSmall>();
                        twsscript.UpdatePosition(randomx, randomy);
                        TWSUnits.Add(Object1);
                        pathfinding.GetGrid().SetGridObject(pathfinding.GetGrid().GetWorldPosition(randomx, randomy),
                                                            new PathNode(pathfinding.GetGrid(), randomx, randomy, 0));
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
                                    //x = Mathf.FloorToInt(GetMouseWorldPosition().x / pathfinding.GetGrid().GetCellSize());
                                    //y = Mathf.FloorToInt(GetMouseWorldPosition().y / pathfinding.GetGrid().GetCellSize());
                                    pathfinding.GetGrid().SetGridObject(GetMouseWorldPosition(), new PathNode(pathfinding.GetGrid(), x, y, 0));
                                    Object1 = Instantiate(iftsInstance, pathfinding.GetGrid().GetWorldPosition(x, y) + new Vector3(pathfinding.GetGrid().GetCellSize(),
                                                pathfinding.GetGrid().GetCellSize()) * .5f, Quaternion.identity);
                                    iftsscript = Object1.GetComponent<InfantrySmall>();
                                    iftsscript.UpdatePosition(x, y);
                                    IFTSUnits.Add(Object1);
                                    credits -= cost;
                                }
                                break;
                            case (1):
                                cost = 2;
                                if (credits >= cost)
                                {
                                    pathfinding.GetGrid().GetXY(GetMouseWorldPosition(), out x, out y);
                                    //x = Mathf.FloorToInt(GetMouseWorldPosition().x / pathfinding.GetGrid().GetCellSize());
                                    //y = Mathf.FloorToInt(GetMouseWorldPosition().y / pathfinding.GetGrid().GetCellSize());
                                    pathfinding.GetGrid().SetGridObject(GetMouseWorldPosition(), new PathNode(pathfinding.GetGrid(), x, y, 0));
                                    Object1 = Instantiate(ifthInstance, pathfinding.GetGrid().GetWorldPosition(x, y) + new Vector3(pathfinding.GetGrid().GetCellSize(),
                                                pathfinding.GetGrid().GetCellSize()) * .5f, Quaternion.identity);
                                    ifthscript = Object1.GetComponent<InfantryHeavy>();
                                    ifthscript.UpdatePosition(x, y); ;
                                    IFTHUnits.Add(Object1);
                                    credits -= cost;
                                }
                                break;
                            case (2):
                                cost = 3;
                                if (credits >= cost)
                                {
                                    pathfinding.GetGrid().GetXY(GetMouseWorldPosition(), out x, out y);
                                    //x = Mathf.FloorToInt(GetMouseWorldPosition().x / pathfinding.GetGrid().GetCellSize());
                                    //y = Mathf.FloorToInt(GetMouseWorldPosition().y / pathfinding.GetGrid().GetCellSize());
                                    pathfinding.GetGrid().SetGridObject(GetMouseWorldPosition(), new PathNode(pathfinding.GetGrid(), x, y, 0));
                                    Object1 = Instantiate(iftkInstance, pathfinding.GetGrid().GetWorldPosition(x, y) + new Vector3(pathfinding.GetGrid().GetCellSize(),
                                                pathfinding.GetGrid().GetCellSize()) * .5f, Quaternion.identity);
                                    iftkscript = Object1.GetComponent<InfantryKiller>();
                                    iftkscript.UpdatePosition(x, y);
                                    IFTKUnits.Add(Object1);
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
                foreach (GameObject currentUnit in IFTSUnits)
                {
                    foreach (GameObject enemyUnit in TWSUnits)
                    {
                        List<PathNode> path = pathfinding.FindPath(currentUnit.GetComponent<InfantrySmall>().GetX(),
                                                                   currentUnit.GetComponent<InfantrySmall>().GetY(),
                                                                   enemyUnit.GetComponent<TowerSmall>().GetX(),
                                                                   enemyUnit.GetComponent<TowerSmall>().GetY());
                        if (path != null)
                        {
                            for(int i = 0; i < path.Count - 1; i++)
                            {
                                Debug.DrawLine(new Vector3(path[i].GetX(), path[i].GetY()) * 10f + Vector3.one * 5f, new Vector3(path[i + 1].GetX(),
                                               path[i + 1].GetY()) * 10f + Vector3.one * 5f, Color.red, 5f);
                            }
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
