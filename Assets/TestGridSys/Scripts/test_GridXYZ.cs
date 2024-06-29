using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using ColorUtility = UnityEngine.ColorUtility;

public class test_GridXYZ
{
    int grid_x_lenght;
    int grid_y_lenght;
    int grid_z_lenght;
    float baseGridSize;
    Vector3 originPosition;

    test_BaseGrid[,,] gridObjcets;

    public event EventHandler<OnGridObjectChangedEventArgs> OnGridObjectChanged;
    public class OnGridObjectChangedEventArgs : EventArgs
    {
        public int x;
        public int y;
        public int z;
    }

    public test_GridXYZ(int grid_x_lenght, int grid_y_lenght, int grid_z_lenght, float baseGridSize, Vector3 originPosition, Material LineMat)
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
                        //gridObjcets[x, y, z].DrawDebugLines(GetWorldPositionOctagonGrid(x, y, z));
                        if (y <= 0) { gridObjcets[x, y, z].CreateLineRenderer(GetWorldPositionOctagonGrid(x, y, z), LineMat); }
                    }
                    else
                    {
                        gridObjcets[x, y, z] = new test_SquareGrid(x, y, z, baseGridSize, y <= 0);
                        //gridObjcets[x, y, z].DrawDebugLines(GetWorldPositionSquareGrid(x, y, z));
                        if (y <= 0) { gridObjcets[x, y, z].CreateLineRenderer(GetWorldPositionSquareGrid(x, y, z), LineMat); }
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

    public test_BaseGrid GetGridObject(int x, int y, int z)
    {
        if (x>=0 && z>=0 && y>=0 && x < grid_x_lenght && z < grid_z_lenght)
        {
            return gridObjcets[x, y, z];
        }
        else
        {
            return null;
        }
    }

    public void TriggerGridObjectChanged(int x, int y, int z)
    {
        OnGridObjectChanged?.Invoke(this, new OnGridObjectChangedEventArgs { x = x, y = y, z = z });
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


    Material MaterialCreate()
    {
        Material sharedMaterial = new Material(Shader.Find("Universal Render Pipeline/Unlit"));
        sharedMaterial.SetFloat("_Surface", 1); // 1: Transparent, 0: Opaque
        sharedMaterial.SetFloat("_Blend", 2); // 0: Alpha, 1: Premultiply, 2: Additive, 3: Multiply
        sharedMaterial.SetInt("_Cull", (int)UnityEngine.Rendering.CullMode.Front + 1); // Front: 2, Back: 1, Both: 0



        float green = 62;
        float red = 142;
        float blue = 54;
        float alpha = 100;
        Color color = new Color(red / 255f, green / 255f, blue / 255f, alpha / 255f);

        sharedMaterial.SetColor("_BaseColor", Color.red);
        sharedMaterial.enableInstancing = true;

        return sharedMaterial;
    }
}
