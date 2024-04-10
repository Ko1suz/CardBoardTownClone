using CodeMonkey.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class CM_Grid <CM_TGridObjcet>
{
    public const int HEAT_MAP_MAX_VALUE = 100;
    public const int HEAT_MAP_MIN_VALUE = 0;
    public event EventHandler<OnGridValueChangedEventArgs> OnGridValueChanged;
    public class OnGridValueChangedEventArgs : EventArgs
    {
        public int x;
        public int y;
    }


    int width;
    int height;
    float cellSize;
    Vector3 originPosition;
    CM_TGridObjcet[,] gridArray;
    private TextMesh[,] debugTextArray;
    bool debug = true;
    //private GameObject[,] textObjcets;
    public CM_Grid(int width, int height, float cellSize, Vector3 originPosition, Func<CM_Grid<CM_TGridObjcet>, int, int, CM_TGridObjcet> createGridObject)
    {
        this.width = width;
        this.height = height;
        this.cellSize = cellSize;
        this.originPosition = originPosition;   
        gridArray = new CM_TGridObjcet[width, height];

        for (int x = 0; x < gridArray.GetLength(0); x++)
        {
            for (int y = 0; y < gridArray.GetLength(1); y++)
            {
                gridArray[x, y] = createGridObject(this, x, y);
            }
        }


        if (debug)
        {
            debugTextArray = new TextMesh[width, height];
            for (int x = 0; x < gridArray.GetLength(0); x++)
            {
                for (int y = 0; y < gridArray.GetLength(1); y++)
                {
                    //debugTextArray[x,y] = UtilsClass.CreateWorldText(gridArray[x,y].ToString(), null, GetWorldPosition3D(x, 0, y) + new Vector3(cellSize, 0 , cellSize) * 0.5f, 20 , Color.green, TextAnchor.MiddleCenter);
                    //Debug.DrawLine(GetWorldPosition3D(x, 0, y), GetWorldPosition3D(x, 0, y + 1), Color.green, 100f);
                    //Debug.DrawLine(GetWorldPosition3D(x, 0, y), GetWorldPosition3D(x + 1, 0, y), Color.green, 100f);

                    debugTextArray[x, y] = UtilsClass.CreateWorldText(gridArray[x, y]?.ToString(), null, GetWorldPosition2D(x, y) + new Vector3(cellSize, cellSize) * 0.5f, 20, Color.white, TextAnchor.MiddleCenter);
                    Debug.DrawLine(GetWorldPosition2D(x, y), GetWorldPosition2D(x, y + 1), Color.white, 100f);
                    Debug.DrawLine(GetWorldPosition2D(x, y), GetWorldPosition2D(x + 1, y), Color.white, 100f);
                }
            }
            //Debug.DrawLine(GetWorldPosition3D(0, 0, height), GetWorldPosition3D(width, 0, height), Color.green, 100f);
            //Debug.DrawLine(GetWorldPosition3D(width, 0, 0), GetWorldPosition3D(width, 0, height), Color.green, 100f);

            Debug.DrawLine(GetWorldPosition2D(0, height), GetWorldPosition2D(width, height), Color.white, 100f);
            Debug.DrawLine(GetWorldPosition2D(width, 0), GetWorldPosition2D(width, height), Color.white, 100f);

            OnGridValueChanged += (object sender, OnGridValueChangedEventArgs eventArgs) =>
            {
                debugTextArray[eventArgs.x, eventArgs.y].text = gridArray[eventArgs.x, eventArgs.y]?.ToString();
            };
        }
    }

    public int GetWidth()
    {
        return width;
    }
    public int GetHeight()
    {
        return height;
    }
    public float GetCellSize()
    {
        return cellSize;
    }

    public Vector3 GetWorldPosition2D(int x, int y)
    {
        return new Vector3(x , y) * cellSize + originPosition;
    }

    public Vector3 GetWorldPosition3D(int x, int y, int z)
    {
        return new Vector3(x, y, z) * cellSize + originPosition;
    }

    private void GetXYZ(Vector3 worldPosition, out int x,out int y, out int z)
    {
        x = Mathf.FloorToInt((worldPosition - originPosition).x / cellSize);
        y = Mathf.FloorToInt((worldPosition - originPosition).y / cellSize);
        z = Mathf.FloorToInt((worldPosition - originPosition).z / cellSize);
    }

    public void SetGridObject(int x, int y, int z, CM_TGridObjcet value)
    {
        if (x >= 0 && y >= 0 && x < width && y < height)
        {
            gridArray[x, y] = value;
            //debugTextArray[x, y].text = gridArray[x, y].ToString();
            if (OnGridValueChanged != null) OnGridValueChanged(this, new OnGridValueChangedEventArgs { x = x, y = y });
        }
    }

    public void SetGridObject(int x, int y, CM_TGridObjcet value)
    {
        if (x >= 0 && y >= 0 && x < width && y < height)
        {
            gridArray[x, y] = value;
            //debugTextArray[x, y].text = gridArray[x, y].ToString();
            if (OnGridValueChanged != null) OnGridValueChanged(this, new OnGridValueChangedEventArgs { x = x, y = y });
        }
    }

    public void TriggerGridObjectChanged(int x, int y)
    {
        if (OnGridValueChanged != null) OnGridValueChanged(this, new OnGridValueChangedEventArgs { x = x, y = y });
    }

    public void SetGridObject(Vector3 worldPosition, CM_TGridObjcet value)
    {
        int x, y, z;
        GetXYZ(worldPosition, out x, out y, out z);
        Debug.Log("x->" + x + " y->" + y + " z->" + z);
        SetGridObject(x,y,z, value);
    }

    public CM_TGridObjcet GetGridObject(int x, int y, int z)
    {
        if (x >= 0 && y >= 0 && x < width && y < height)
        {
            return gridArray[x, y];
        }
        else
        {
            return default(CM_TGridObjcet);
        }
    }

    public CM_TGridObjcet GetGridObject(int x, int y)
    {
        if (x >= 0 && y >= 0 && x < width && y < height)
        {
            return gridArray[x, y];
        }
        else
        {
            return default(CM_TGridObjcet);
        }
    }
    public CM_TGridObjcet GetGridObject(Vector3 worldPosition)
    {
        int x,y,z;
        GetXYZ(worldPosition, out x, out y, out z);
        return GetGridObject(x, y, z);
    }
}
