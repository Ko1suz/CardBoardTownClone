using CodeMonkey.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class My_XZ_Grid : MonoBehaviour
{
    [SerializeField] private GameObject gridPrefab;
    [SerializeField] private int x_axis_length;
    [SerializeField] private int y_axis_length;
    [SerializeField] private int z_axis_length;
    [SerializeField] private float cellSize;
    [SerializeField] private Vector3 originPosition;
    [SerializeField] private GameObject[,] gridArray;

    private void Start()
    {
        CreateGrid(gridPrefab, x_axis_length, z_axis_length, cellSize, originPosition);
    }
    public void CreateGrid(GameObject gridPrefab, int x_axis_lenght, int z_axis_length, float cellSize, Vector3 originPosition)
    {
        this.gridPrefab = gridPrefab;
        this.x_axis_length = x_axis_lenght;
        this.z_axis_length = z_axis_length;
        this.cellSize = cellSize;
        this.originPosition = originPosition;

        gridArray = new GameObject[x_axis_length, z_axis_length];
        for (int x = 0; x < gridArray.GetLength(0); x++)
        {
            for (int z = 0; z < gridArray.GetLength(1); z++)
            {
                GameObject cloneGrid = Instantiate(this.gridPrefab);
                cloneGrid.gameObject.SetActive(false);
                gridArray[x, z] = cloneGrid;
                cloneGrid.transform.parent = this.transform;
                if (z%2 == 0 )
                {
                    cloneGrid.GetComponent<GridObject>().Shape = GridObject._shape.Square;
                    cloneGrid.GetComponent<GridObject>().GridSize = 1 * cellSize;
                    cloneGrid.transform.localPosition = new Vector3(x * 4, 0, z * 2);
                }
                else
                {
                    cloneGrid.GetComponent<GridObject>().Shape = GridObject._shape.Octagon;
                    cloneGrid.GetComponent<GridObject>().GridSize = 2 * cellSize;
                    cloneGrid.transform.localPosition = new Vector3((x * 4) + 2, 0, (z *2));
                }
                cloneGrid.SetActive(true);
            }
        }
    }


}
