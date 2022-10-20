using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Testing : MonoBehaviour
{

    [SerializeField] private GameObject pcInstance;
    [SerializeField] private GameObject iftsInstance, ifthInstance, iftkInstance;
    [SerializeField] private TextMeshProUGUI tcredits;
    private Grid grid;
    private int type = 3, credits, cost;
    private GameState _gameState = GameState.prepare;
    //private GameObject pcObject, iftObject;
    private int x, y;

    void Start()
    {
        #region BuildingGrid
        grid = new Grid(18, 5, 10f);
        for (int i = 0; i < 6; i++)
        {
            for (int j = 0; j < 5; j++)
            {
                grid.SetValue(i, j, 1);
            }
        }

        for (int i = 17; i >= 12; i--)
        {
            for (int j = 0; j < 5; j++)
            {
                grid.SetValue(i, j, 2);
            }
        }

        for (int i = 1; i < 4; i++)
        {
            grid.SetValue(15, i, 3);
        }
        #endregion
        #region TestingObjects
        Instantiate(pcInstance, grid.GetWorldPosition(17, 2) + new Vector3(grid.GetCellSize(), grid.GetCellSize()) * .5f, Quaternion.identity);
        #endregion
        credits = 15;
    }

    private void Update()
    {
        switch (_gameState)
        {
            #region Prepare
            case GameState.prepare:
                tcredits.text = "Credits: " + credits;
                if (Input.GetMouseButtonDown(0))
                {
                    if (grid.GetValue(GetMouseWorldPosition()) == 1)
                    {
                        switch (type)
                        {
                            case (0):
                                cost = 1;
                                if (credits >= cost)
                                {
                                    grid.SetValue(GetMouseWorldPosition(), 0);
                                    grid.GetXY(GetMouseWorldPosition(), out x, out y);
                                    Instantiate(iftsInstance, grid.GetWorldPosition(x, y) + new Vector3(grid.GetCellSize(), grid.GetCellSize()) * .5f, Quaternion.identity);
                                }
                                break;
                            case (1):
                                cost = 2;
                                if (credits >= cost)
                                {
                                    grid.SetValue(GetMouseWorldPosition(), 0);
                                    grid.GetXY(GetMouseWorldPosition(), out x, out y);
                                    Instantiate(ifthInstance, grid.GetWorldPosition(x, y) + new Vector3(grid.GetCellSize(), grid.GetCellSize()) * .5f, Quaternion.identity); 
                                }
                                break;
                            case (2):
                                cost = 3;
                                if (credits >= cost)
                                {
                                    grid.SetValue(GetMouseWorldPosition(), 0);
                                    grid.GetXY(GetMouseWorldPosition(), out x, out y);
                                    Instantiate(iftkInstance, grid.GetWorldPosition(x, y) + new Vector3(grid.GetCellSize(), grid.GetCellSize()) * .5f, Quaternion.identity);
                                }
                                break;
                        }
                        credits -= cost;
                    }
                }
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
