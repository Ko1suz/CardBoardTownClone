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
                        gridObjcets[x, y, z] = new test_OctagonGrid(x, y, z, baseGridSize, y <= 5);
                        gridObjcets[x, y, z].DrawDebugLines(GetWorldPositionOctagonGrid(x, y, z));
                    }
                    else
                    {
                        gridObjcets[x, y, z] = new test_SquareGrid(x, y, z, baseGridSize, y <= 5);
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

    public Vector3 GetWorldPositionGrid(int x, int y, int z)
    {
        bool isSquareGrid = gridObjcets[x, y, z].isSquareGrid;
        if (isSquareGrid)
        {
            return new Vector3(x, y, (z * 2) + 1) * baseGridSize + originPosition;
        }
        else
        {
            return new Vector3(x, y, z * 2) * baseGridSize + originPosition;
        }
    }

    public void GetGridIndexAtWorldPosition(Vector3 worldPosition, out int x, out int y, out int z)
    {
        float octagonDistance = 0.925f * baseGridSize;
        float squareDistance = 0.425f * baseGridSize;
        int yIndex = Mathf.FloorToInt(worldPosition.y + 0.1f);
        bool isSquareGrid = false;

        // Baþlangýçta x, y, ve z deðerlerini -1 olarak atýyoruz.
        x = -1;
        z = -1;
        y = -1;
        for (int zIndex = 0; zIndex < gridObjcets.GetLength(2); zIndex++)
        {
            for (int xIndex = 0; xIndex < gridObjcets.GetLength(0); xIndex++)
            {
                Vector3 gridPosition = gridObjcets[xIndex, yIndex, zIndex].GetWorldPosition();
                float distance = Mathf.Abs(Vector3.Distance(worldPosition, gridPosition));
                isSquareGrid = gridObjcets[xIndex, yIndex, zIndex].isSquareGrid;
                // Kare grid için mesafe kontrolü
                if (isSquareGrid && distance < squareDistance)
                {
                    x = xIndex;
                    z = zIndex;
                    break;
                }
                // Sekizgen grid için mesafe kontrolü
                else if (!isSquareGrid && distance < octagonDistance)
                {
                    x = xIndex;
                    z = zIndex;
                    break;
                }
            }
        }

        y = yIndex;
    }
}
