using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using CodeMonkey.Utils;
public class HeatMapVisual : MonoBehaviour
{
    private Grid grid;
    private Mesh mesh;
    private void Awake()
    {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;
    }
    public void SetGrid(Grid grid)
    {
        this.grid = grid;
        UpdateHeatMapVisual();
    }

    private void UpdateHeatMapVisual()
    {
        MeshUtils.CreateEmptyMeshArrays(grid.getWidht * grid.getHeight, out Vector3[] vertices, out Vector2[] uv, out int[] triangles);

        for (int x = 0; x < grid.getWidht; x++)
        {
            for (int y = 0; y < grid.getHeight; y++)
            {
                int index = x * grid.getHeight + y;
                Debug.Log(index);
            }
        }
    }
}
