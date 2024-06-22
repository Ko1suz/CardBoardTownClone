using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class test_BaseGrid 
{
    protected int x, y, z;
    protected float baseGridSize = 1;
    protected Transform placedObject;
    protected Transform lineRenderObj;
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
    public virtual void CreateLineRenderer(Vector3 worldPos, Material lineMat)
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

    public void SetPlacedObject(Transform placedObject)
    {
        this.placedObject = placedObject;
        //grid.TriggerGridObjectChanged(x, z);
    }

    public bool CheckPlacedObject()
    {
        if (placedObject == null){ return true; }
        else{ return false; }
    }

    public Transform GetPlacedObjet()
    {
        return placedObject;
    }

    public int GetXIndex()
    {
        return x;
    }
    public int GetYIndex()
    {
        return y;
    }
    public int GetZIndex()
    {
        return z;
    }

    public GameObject ClearPlacedObjcet()
    {
        GameObject deletedRef = this.placedObject.gameObject;
        this.placedObject = null;
        //grid.TriggerGridObjectChanged(x, z);
        return deletedRef;
    }

    public bool CanBuild()
    {
        return placedObject == null;
    }
}
