using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using CodeMonkey.Utils;

public class CM_Testing : MonoBehaviour
{
    private CM_Grid<bool> cm_Grid;
    void Start()
    {
        //cm_Grid = new CM_Grid<bool>(20,10, 10f, Vector3.zero);

        //CM_HeatMapVisual cm_HeatMapVisual = new CM_HeatMapVisual(cm_Grid, GetComponent<MeshFilter>());
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //cm_Grid.SetValue(GetMousePos3D(), 5);
            //Debug.Log(GetMousePos3D());

            //cm_Grid.SetValue(GetMousePos2D(), 5);
            Debug.Log(GetMousePos2D());
        }
        else if (Input.GetMouseButtonDown(1))
        {
            //Debug.Log(cm_Grid.GetValue(GetMousePos3D()));

            Debug.Log(cm_Grid.GetGridObject(GetMousePos2D()));
        }
    }
    //3D
    public static Vector3 GetMousePos3D()
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

    public static Vector3 GetMousePos3D(LayerMask layerMask)
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit raycastHit, layerMask))
        {
            return raycastHit.point;
        }
        else
        {
            return Vector3.zero;
        }
    }

    public static GameObject GetRaycastHitObject(LayerMask layerMask)
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit raycastHit, layerMask))
        {
            return raycastHit.collider.gameObject;
        }
        else
        {
            return null;
        }
    }


    public static Vector3 GetMousePos2D()
    {
        Vector3 vec = GetMouseWoldPosWithZ(Input.mousePosition, Camera.main);
        vec.z = 0;
        return vec;
    }

    public static Vector3 GetMouseWoldPosWithZ()
    {
        return GetMouseWoldPosWithZ(Input.mousePosition, Camera.main);
    }
    public static Vector3 GetMouseWoldPosWithZ(Camera worldCamera)
    {
        return GetMouseWoldPosWithZ(Input.mousePosition, worldCamera);
    }
    public static Vector3 GetMouseWoldPosWithZ(Vector3 screenPosition, Camera worldCamera)
    {
        Vector3 worldPosition = worldCamera.ScreenToWorldPoint(screenPosition);
        return worldPosition;
    }

}

//public class CM_HeatMapVisual_InClass
//{
//    private CM_Grid cm_Grid;

//    public CM_HeatMapVisual_InClass(CM_Grid cm_Grid, MeshFilter meshFilter)
//    {
//        this.cm_Grid = cm_Grid;

//        Vector3[] vertices;
//        Vector2[] uv;
//        int[] triangles;

//        MeshUtils.CreateEmptyMeshArrays(cm_Grid.GetWidth() * cm_Grid.GetHeight(), out vertices, out uv, out triangles);
//        for (int x = 0; x < cm_Grid.GetWidth(); x++)
//        {
//            for (int y = 0; y < cm_Grid.GetHeight(); y++)
//            {
//                int index = x * cm_Grid.GetHeight() + y;
//                Vector3 baseSize = new Vector3(1, 1) * cm_Grid.GetCellSize();
//                MeshUtils.AddToMeshArrays(vertices, uv, triangles, index, cm_Grid.GetWorldPosition3D(x, 0, y) + baseSize * .5f, 0f, baseSize, Vector2.zero, Vector2.zero);
//            }
//        }

//        Mesh mesh = new Mesh();

//        mesh.vertices = vertices;
//        mesh.uv = uv;
//        mesh.triangles = triangles;

//        meshFilter.mesh = mesh;
//    }
//}
