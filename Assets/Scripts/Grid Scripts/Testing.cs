using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Testing : MonoBehaviour
{

    [SerializeField] private GameObject pcInstance;
    private Grid grid;
    private GameObject pcObject;
    private PowerCore pcScript;

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
        pcObject = Instantiate(pcInstance, grid.GetWorldPosition(17, 2) + new Vector3(grid.GetCellSize(), grid.GetCellSize()) * .5f, Quaternion.identity);
        #endregion
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (grid.GetValue(GetMouseWorldPosition()) == 1)
            {
                grid.SetValue(GetMouseWorldPosition(), 4);
            }
        }
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
    #endregion
}
