using CodeMonkey.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using UnityEngine;

public class CM_Grid 
{
    int width;
    int height;
    float cellSize;
    Vector3 originPosition;
    int[,] gridArray;
    private TextMesh[,] debugTextArray;
    //private GameObject[,] textObjcets;
    public CM_Grid(int width, int height, float cellSize, Vector3 originPosition)
    {
        this.width = width;
        this.height = height;
        this.cellSize = cellSize;
        this.originPosition = originPosition;   
        gridArray = new int[width, height];
        debugTextArray = new TextMesh[width, height];

        //Debug.Log("Width-> " + width + "- Height-> " + height);

        for (int x = 0; x < gridArray.GetLength(0); x++)
        {
            for (int y = 0; y < gridArray.GetLength(1); y++)
            {
                debugTextArray[x,y] = UtilsClass.CreateWorldText(gridArray[x,y].ToString(), null, GetWorldPosition3D(x, 0, y) + new Vector3(cellSize, 0 , cellSize) * 0.5f, 20 , Color.green, TextAnchor.MiddleCenter);
                Debug.DrawLine(GetWorldPosition3D(x, 0, y), GetWorldPosition3D(x, 0, y + 1), Color.green, 100f);
                Debug.DrawLine(GetWorldPosition3D(x, 0, y), GetWorldPosition3D(x + 1, 0, y), Color.green, 100f);
            }
        }
        Debug.DrawLine(GetWorldPosition3D(0, 0, height), GetWorldPosition3D(width, 0, height), Color.green, 100f);
        Debug.DrawLine(GetWorldPosition3D(width, 0, 0), GetWorldPosition3D(width, 0, height), Color.green, 100f);

        SetValue(2, 0, 1, 46);
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

    public void SetValue(int x, int y, int z, int value)
    {
        if (x >= 0 && z >= 0 && x < width && z < height)
        {
            gridArray[x, z] = value;
            debugTextArray[x, z].text = gridArray[x, z].ToString();
        }
    }

    public void SetValue(Vector3 worldPosition, int value)
    {
        int x, y, z;
        GetXYZ(worldPosition, out x, out y, out z);
        Debug.Log("x->" + x + " y->" + y + " z->" + z);
        SetValue(x,0,z, value);
    }

    public int GetValue(int x, int y, int z)
    {
        if (x >= 0 && z >= 0 && x < width && z < height)
        {
            return gridArray[x, z];
        }
        else
        {
            return -1;
        }
    }
    public int GetValue(Vector3 worldPosition)
    {
        int x,y,z;
        GetXYZ(worldPosition, out x, out y, out z);
        return GetValue(x, y, z);
    }
}
