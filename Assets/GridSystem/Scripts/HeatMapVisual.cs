using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using CodeMonkey.Utils;
public class HeatMapVisual : MonoBehaviour
{
    private Grid grid;
    private Mesh mesh;
    private bool updateMesh;
    private void Awake()
    {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;
    }
    //public void SetGrid(Grid grid)
    //{
    //    this.grid = grid;
    //    UpdateHeatMapVisual();

    //    grid.OnGridValueChanged += Grid_OnGridValueChanged;
    //}

    //private void Grid_OnGridValueChanged(object sender, Grid.OnGridValueChangedEventArgs e)
    //{
    //    Debug.Log("Grid_OnGridValueChanged");
    //    //UpdateHeatMapVisual();
    //    updateMesh = true;
    //}

    //private void LateUpdate()
    //{
    //    if (updateMesh)
    //    {
    //        updateMesh = false;
    //        UpdateHeatMapVisual();
    //    }
    //}

    //private void UpdateHeatMapVisual()
    //{
    //    MeshUtils.CreateEmptyMeshArrays(grid.getWidht * grid.getHeight, out Vector3[] vertices, out Vector2[] uv, out int[] triangles);

    //    for (int x = 0; x < grid.getWidht; x++)
    //    {
    //        for (int y = 0; y < grid.getHeight; y++)
    //        {
    //            int index = x * grid.getHeight + y;
    //            Vector3 quadSize = new Vector3(1, 1) * grid.getCellSize;

    //            int gridValue = grid.GetValue(x,y);
    //            float gridValueNormalized = (float)gridValue / (float)Grid.HEAT_MAP_MAX_VALUE;
    //            Vector2 gridValueUV = new Vector2(gridValueNormalized, 0f);

    //            MeshUtils.AddToMeshArrays(vertices,uv, triangles, index, grid.GetWorldPosition(x,y) + quadSize * 0.5f, 0f, quadSize, gridValueUV, gridValueUV);
    //        }
    //    }

    //    mesh.vertices = vertices;
    //    mesh.uv = uv;
    //    mesh.triangles = triangles;
    //}
}
