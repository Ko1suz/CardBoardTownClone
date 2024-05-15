using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class test_GridXYZ 
{
    int grid_x_lenght;
    int grid_y_lenght;
    int grid_z_lenght;
    float baseGridSize;
    Vector3 originPosition;

    test_BaseGrid[,,] gridObjcets;

    public test_GridXYZ(int grid_x_lenght, int grid_y_lenght, int grid_z_lenght, float baseGridSize, Vector3 originPosition)
    {
        this.grid_x_lenght = grid_x_lenght; 
        this.grid_y_lenght = grid_y_lenght;
        this.grid_z_lenght = grid_z_lenght;
        this.baseGridSize = baseGridSize;
        this.originPosition = originPosition;
        gridObjcets = new test_BaseGrid[grid_x_lenght, grid_y_lenght, grid_z_lenght];

        for (int y = 0; y < gridObjcets.GetLength(1); y++)
        {
            for (int z = 0; z < gridObjcets.GetLength(2); z++)
            {
                for (int x = 0; x < gridObjcets.GetLength(0); x++)
                {
                    if (x <=0 || x % 2 == 0)
                    {
                        gridObjcets[x, y, z] = new test_OctagonGrid(x, y, z, baseGridSize, y <= 0);
                        gridObjcets[x, y, z].DrawDebugLines(GetWorldPositionOctagonGrid(x, y, z));
                    }
                    else
                    {
                        gridObjcets[x, y, z] = new test_SquareGrid(x, y, z, baseGridSize, y <= 0);
                        gridObjcets[x, y, z].DrawDebugLines(GetWorldPositionSquareGrid(x, y, z));
                    }
                }
            }
        }
    }

    public Vector3 GetWorldPositionOctagonGrid(int x, int y, int z)
    {
        return new Vector3(x, y, z * 2) * baseGridSize + originPosition;
    }
    public Vector3 GetWorldPositionSquareGrid(int x, int y, int z)
    {
        return new Vector3(x, y, (z * 2) + 1) * baseGridSize + originPosition;
    }

    public void GetGridXYZOctagon(Vector3 worldPosition, out int x, out int y, out int z)
    {
        x = Mathf.FloorToInt((worldPosition - originPosition).x / 2 / baseGridSize);
        y = Mathf.FloorToInt((worldPosition - originPosition).y / baseGridSize);
        z = Mathf.FloorToInt((worldPosition - originPosition).z / baseGridSize);
    }

    public void GetGridXYZSquare(Vector3 worldPosition, out int x, out int y, out int z)
    {
        x = Mathf.FloorToInt((((worldPosition - originPosition).x / 2) - 1) / baseGridSize);
        y = Mathf.FloorToInt((worldPosition - originPosition).y / baseGridSize);
        z = Mathf.FloorToInt((worldPosition - originPosition).z / baseGridSize);
    }

    public void GetGridXYZ(Vector3 worldPosition, out int x, out int y, out int z)
    {
        x = Mathf.FloorToInt((worldPosition - originPosition).x / baseGridSize);
        y = Mathf.FloorToInt((worldPosition - originPosition).y / baseGridSize);
        z = Mathf.FloorToInt((worldPosition - originPosition).z / baseGridSize);
    }
}
