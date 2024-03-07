using CodeMonkey.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class My_XZ_Grid<TGridObject> : MonoBehaviour
{
    private int width;
    private int height;
    private float cellSize;
    private Vector3 originPosition;
    private TGridObject[,] gridArray;


    public My_XZ_Grid(TGridObject gridObject, int width, int height, float cellSize, Vector3 originPosition)
    {
        this.width = width;
        this.height = height;
        this.cellSize = cellSize;
        this.originPosition = originPosition;

        gridArray = new TGridObject[width, height];

    }
     
}

public class TestGridObject
{
    public GameObject gridObject;
    public int width;
    public int height;
    public float cellSize;
    public Vector3 originPosition;

    public TestGridObject(GameObject gridObject, int width, int height, float cellsize, Vector3 originPosition)
    {
        this.gridObject = gridObject;
        this.width = width;
        this.height = height;
        this.cellSize = cellsize;
        this.originPosition = originPosition;
    }
}
