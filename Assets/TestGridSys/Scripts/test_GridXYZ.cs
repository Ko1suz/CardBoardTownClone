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

    public void GetGridIndexAtWorldPosition(Vector3 worldPosition, out int x, out int y, out int z)
    {
        float smallestDistance = float.MaxValue * baseGridSize;
        bool isSquareGrid = false;

        // Ba�lang��ta x, y, ve z de�erlerini -1 olarak at�yoruz.
        x = -1;
        y = -1;
        z = -1;

        for (int yIndex = 0; yIndex < gridObjcets.GetLength(1); yIndex++)
        {
            for (int zIndex = 0; zIndex < gridObjcets.GetLength(2); zIndex++)
            {
                for (int xIndex = 0; xIndex < gridObjcets.GetLength(0); xIndex++)
                {
                    Vector3 gridPosition = gridObjcets[xIndex, yIndex, zIndex].GetWorldPosition();
                    float gridSizeMultiplier = gridObjcets[xIndex, yIndex, zIndex].GetGridSizeMultiplier();
                    float distance = Vector3.Distance(worldPosition, gridPosition);

                    // Kare grid i�in mesafe kontrol�
                    if (!isSquareGrid && distance < smallestDistance)
                    {
                        smallestDistance = distance;
                        x = xIndex;
                        z = zIndex;
                        isSquareGrid = true;
                    }
                    // Sekizgen grid i�in mesafe kontrol�
                    else if (isSquareGrid && distance < smallestDistance * gridSizeMultiplier)
                    {
                        smallestDistance = distance / gridSizeMultiplier;
                        x = xIndex;
                        z = zIndex;
                        isSquareGrid = false;
                    }
                }
            }
        }
        y = Mathf.FloorToInt(worldPosition.y + 0.1f);

        // E�er en yak�n grid'in mesafesi belirli bir e�ik de�erden b�y�kse, ge�ersiz bir grid olarak i�aretliyoruz.
        if (isSquareGrid)
        {
            if (smallestDistance*2 > baseGridSize)
            {
                x = -1;
                y = -1;
                z = -1;
            }
        }
        else
        {
            if (smallestDistance > baseGridSize)
            {
                x = -1;
                y = -1;
                z = -1;
            }
        }

    }
}
