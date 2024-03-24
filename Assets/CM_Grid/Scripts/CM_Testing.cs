using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CM_Testing : MonoBehaviour
{
    private CM_Grid cm_Grid;
    void Start()
    {
        cm_Grid = new CM_Grid(4,2, 10f);
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
