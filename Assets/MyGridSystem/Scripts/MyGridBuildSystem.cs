using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class MyGridBuildSystem : MonoBehaviour
{

    [SerializeField] private GameObject gridPrefab;

    private void Awake()
    {
        int gridWidht = 10;
        int gridHeight = 10;
        float cellSize = 10;
        //Grid obje þekli -> köþe sayýsý
        //Grid size -> kodun içinde tanýmladýðým

        //grid = new GridXZ<GridObjectTesting>(gridWidht, gridHeight, cellSize, Vector3.zero,
           //(GridXZ<GridObjectTesting> g, int x, int z) => new GridObjectTesting(g, x, z, testTransform));
    }

}
