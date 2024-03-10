using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyGridBuildSystem : MonoBehaviour
{

    [SerializeField] private Transform testTransform;
    private My_XZ_Grid<GridObjectTesting> grid;

    private void Awake()
    {
        //int gridWidht = 10;
        //int gridHeight = 10;
        //float cellSize = 10;
        //grid = new My_XZ_Grid<GridObject>(gridWidht, gridHeight, cellSize, Vector3.zero, (GridXZ<GridObject> g, int x, int z) => new GridObject(g, x, z, testTransform));
    }

}
