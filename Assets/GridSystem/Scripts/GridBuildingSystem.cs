using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class GridBuildingSystem : MonoBehaviour
{
    [SerializeField] private Transform testTransform;
    private GridXZ<GridObjectTesting> grid;

    private void Awake()
    {
        int gridWidht = 10;
        int gridHeight = 10;
        float cellSize = 10;
        grid = new GridXZ<GridObjectTesting>(gridWidht, gridHeight, cellSize, Vector3.zero, 
            (GridXZ<GridObjectTesting> g, int x, int z) => new GridObjectTesting(g,x,z, testTransform));
    }

    [SerializeField] static LayerMask mouseColliderLayerMask;
    public static Vector3 GetMousePos()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit raycastHit, 999f, mouseColliderLayerMask))
        {
            return raycastHit.point;
        }
        else
        {
            return Vector3.zero;
        }
    }
}

public class GridObjectTesting
{
    private GridXZ<GridObjectTesting> grid;
    private int x;
    private int z;
    private Transform testTransform;
    public GridObjectTesting(GridXZ<GridObjectTesting> grid, int x, int z, Transform testTransform)
    {
        this.grid = grid;
        this.x = x;
        this.z = z;
        this.testTransform = testTransform;
    }

    public override string ToString()
    {
        return x + ", " + z;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("Bastý");
            MonoBehaviour.Instantiate(testTransform, GridBuildingSystem.GetMousePos(), Quaternion.identity);
        }
    }
}



