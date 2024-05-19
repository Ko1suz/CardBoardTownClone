using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test_BaseGrid 
{
    protected int x, y, z;
    protected float baseGridSize = 1;
    protected Transform placedObject;
    protected bool showDebug;
    public bool isSquareGrid;

    public test_BaseGrid(int x, int y, int z, float baseGridSize, bool showDebug = false) 
    {
        this.x = x;
        this.y = y;
        this.z = z;
        this.baseGridSize = baseGridSize;   
        this.showDebug = showDebug;
    }

    public virtual void DrawDebugLines(Vector3 worldPos)
    {

    }

    public virtual Vector3 GetWorldPosition()
    {
        return Vector3.zero;
    }

    public virtual float GetGridSizeMultiplier()
    {
        return 0f;
    }
}
