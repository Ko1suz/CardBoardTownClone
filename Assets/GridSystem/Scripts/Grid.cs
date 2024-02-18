using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;
using Unity.Mathematics;
using static UnityEngine.Rendering.DebugUI;
using System;

public class Grid 
{
    public const int HEAT_MAP_MAX_VALUE = 100;
    public const int HEAT_MAP_MIN_VALUE = 0;
    public event EventHandler<OnGridValueChangedEventArgs> OnGridValueChanged;
    public class OnGridValueChangedEventArgs : EventArgs
    {
        public int x;
        public int y;
    }

    private int widht;
    private int height;
    private float cellSize;
    private Vector3 originPosition;
    private bool itsThreeD;
    private int[,] gridArray;
    private TextMesh[,] debugTextArray;

    public int getWidht { get => widht;  }
    public int getHeight { get => height; }
    public float getCellSize { get => cellSize; }

    public Grid(int widht, int height, float cellSize, Vector3 originPosition, bool itsThreeD)
    {
        this.widht = widht;
        this.height = height;
        this.cellSize = cellSize;
        this.originPosition = originPosition;
        this.itsThreeD = itsThreeD;

        gridArray = new int[widht, height];
        bool showDebug = true;
        if (showDebug)
        {
            debugTextArray = new TextMesh[widht, height];
            for (int x = 0; x < gridArray.GetLength(0); x++)
            {
                for (int y = 0; y < gridArray.GetLength(1); y++)
                {
                    debugTextArray[x, y] = UtilsClass.CreateWorldText(gridArray[x, y].ToString(), null, GetWorldPosition(x, y) + new Vector3(cellSize, cellSize) * .5f, 20, Color.white, TextAnchor.MiddleCenter);
                    Debug.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x, y + 1), Color.green, 100f);
                    Debug.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x + 1, y), Color.green, 100f);
                }
            }
            Debug.DrawLine(GetWorldPosition(0, height), GetWorldPosition(widht, height), Color.green, 100f);
            Debug.DrawLine(GetWorldPosition(widht, 0), GetWorldPosition(widht, height), Color.green, 100f);

            OnGridValueChanged += (object sender, OnGridValueChangedEventArgs eventArgs) =>
            {
                debugTextArray[eventArgs.x, eventArgs.y].text = gridArray[eventArgs.x, eventArgs.y].ToString();
            };
        }
       
    }

    public Vector3 GetWorldPosition(int x, int y)
    {
        // b = a != null ? a.Value : yeet(b);
        return itsThreeD == false ?  new Vector3(x, y) * cellSize + originPosition : new Vector3(y, 0, x) * cellSize + originPosition; // new Vector3(x, 0, y) * cellSize
    }

    private void GetXY(Vector3 worldPosition, out int x, out int y)
    {
        x = Mathf.FloorToInt((worldPosition - originPosition).x / cellSize);
        y = Mathf.FloorToInt((worldPosition - originPosition).y / cellSize);
    }
    public void SetValue(int x, int y, int value)
    {
        if (x >= 0 && y >= 0 && x < widht && y < height)
        {
            gridArray[x, y] = Mathf.Clamp(value, HEAT_MAP_MIN_VALUE, HEAT_MAP_MAX_VALUE);
            //debugTextArray[x, y].text = gridArray[x, y].ToString();
            if (OnGridValueChanged != null) { OnGridValueChanged(this, new OnGridValueChangedEventArgs { x = x, y = y }); }
        }
    }

    public void SetValue(Vector3 worldPosition, int value)
    {
        int x, y;
        GetXY(worldPosition, out x, out y);
        SetValue(x, y, value);
    }
    public void AddValue(int x, int y, int value)
    {
        SetValue(x, y, GetValue(x, y) + value);
    }

    public int GetValue(int x, int y)
    {
        if (x >= 0 && y >= 0 && x < widht && y < height)
        {
           return gridArray[x, y];
        }
        else
        {
            return -1;
        }
    }

    public int GetValue(Vector3 worldPosition)
    {
        int x, y;
        GetXY(worldPosition, out x, out y);
        return GetValue(x, y);
    }

    public void AddValue(Vector3 worldPosition, int value, int fullValueRange, int totalRange)
    {
        int lowerValueAmount = Mathf.RoundToInt((float)value / (totalRange - fullValueRange));

        GetXY(worldPosition, out int originX, out int originY);
        for (int x = 0; x < totalRange; x++)
        {
            for (int y = 0; y < totalRange - x; y++)
            {
                int radius = x + y;
                int addValueAmount = value;
                if (radius > fullValueRange)
                {
                    addValueAmount -= lowerValueAmount * (radius - fullValueRange);
                }

                AddValue(originX + x, originY + y, addValueAmount);
                if (x != 0 )
                { AddValue(originX - x, originY + y, addValueAmount); }

                if(y != 0)
                {
                    AddValue(originX + x, originY - y, addValueAmount);
                    if (x != 0)
                    { AddValue(originX - x, originY - y, addValueAmount); }
                }
                
            }
        }
    }


}
