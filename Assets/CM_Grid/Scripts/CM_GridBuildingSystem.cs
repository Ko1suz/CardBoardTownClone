using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CM_GridBuildingSystem : MonoBehaviour
{
    CM_GridXZ<CM_GridObject> grid;

    private void Awake()
    {
        int gridWidth = 10;
        int gridHeight = 10;
        float cellSize = 10;

        grid = new CM_GridXZ<CM_GridObject> (gridWidth, gridHeight, cellSize, Vector3.zero, (CM_GridXZ<CM_GridObject> g, int x, int z) => new CM_GridObject(g,x,z));
    }
}


public class CM_GridObject
{
    private CM_GridXZ<CM_GridObject> grid;
    private int x;
    private int z;

    public CM_GridObject(CM_GridXZ<CM_GridObject> grid, int x, int z)
    {
        this.grid = grid;
        this.x = x;
        this.z = z;
    }

    public override string ToString()
    {
        return x + " " + z;
    }
}
