using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using CodeMonkey.Utils;

public class CM_Testing : MonoBehaviour
{
    private CM_Grid cm_Grid;
    void Start()
    {
        cm_Grid = new CM_Grid(4,2, 10f, new Vector3(0,0,0));

        CM_HeatMapVisual cm_HeatMapVisual = new CM_HeatMapVisual(cm_Grid, GetComponent<MeshFilter>());
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            cm_Grid.SetValue(GetMousePos(), 5);
            Debug.Log(GetMousePos());
        }
        else if (Input.GetMouseButtonDown(1))
        {
            Debug.Log(cm_Grid.GetValue(GetMousePos()));
        }
    }
    public Vector3 GetMousePos()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit raycastHit))
        {
            return raycastHit.point;
        }
        else
        {
            return Vector3.zero;
        }
    }
}

public class CM_HeatMapVisual
{
    private CM_Grid cm_Grid;

    public CM_HeatMapVisual(CM_Grid cm_Grid, MeshFilter meshFilter)
    {
        this.cm_Grid = cm_Grid;

        Vector3[] vertices;
        Vector2[] uv;
        int[] triangles;

        MeshUtils.CreateEmptyMeshArrays(cm_Grid.GetWidth() * cm_Grid.GetHeight(), out vertices, out uv, out triangles);
        for (int x = 0; x < cm_Grid.GetWidth(); x++)
        {
            for (int y = 0; y < cm_Grid.GetHeight(); y++)
            {
                int index = x * cm_Grid.GetHeight() + y;
                Vector3 baseSize = new Vector3(1,1) * cm_Grid.GetCellSize();
                MeshUtils.AddToMeshArrays(vertices, uv, triangles, index, cm_Grid.GetWorldPosition3D(x,0,y) + baseSize * .5f, 0f, baseSize, Vector2.zero, Vector2.zero);
            }
        }

        Mesh mesh = new Mesh();

        mesh.vertices = vertices;
        mesh.uv = uv;
        mesh.triangles = triangles;

        meshFilter.mesh = mesh;
    }
}
