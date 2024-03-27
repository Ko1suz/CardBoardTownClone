using System.Collections;
using System.Collections.Generic;
using System.Xml.Schema;
using UnityEngine;

public class CM_HeatMapVisual : MonoBehaviour
{
    CM_Grid grid;
    Mesh mesh;  
    private void Awake()
    {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;
    }
    public void SetGrid(CM_Grid grid)
    {
        this.grid = grid;   
        UpdateHeatMapVisual();
    }

    private void UpdateHeatMapVisual()
    {
        MeshUtils.CreateEmptyMeshArrays(grid.GetWidth() * grid.GetHeight(), out Vector3[] vertices, out Vector2[] uv, out int[] triangles);

        for (int x = 0; x < grid.GetWidth(); x++)
        {
            for (int y = 0; y < grid.GetHeight(); y++)
            {
                int index = x * grid.GetHeight() + y;
                Vector3 quadSize = new Vector3(1, 1) * grid.GetCellSize();
                MeshUtils.AddToMeshArrays(vertices, uv, triangles, index, grid.GetWorldPosition3D(x, 0, y), 270, quadSize, Vector2.zero, Vector2.zero);
            }
        }

        mesh.vertices = vertices;
        mesh.uv = uv;
        mesh.triangles = triangles;
    }
}
