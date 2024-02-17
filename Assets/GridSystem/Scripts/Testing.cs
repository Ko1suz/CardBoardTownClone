using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;

public class Testing : MonoBehaviour
{
    [SerializeField] private HeatMapVisual heatMapVisual;
    private Grid grid;
    private void Start()
    {
        grid = new Grid(8,4, 10f, new Vector3(-20,-10), false);
        heatMapVisual.SetGrid(grid);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 position = UtilsClass.GetMouseWorldPosition();
            int value = grid.GetValue(UtilsClass.GetMouseWorldPosition());
            grid.SetValue(position, value + 5);
        }
    }

    void ChangeText()
    {
        if (Input.GetMouseButtonDown(0))
        {
            grid.SetValue(UtilsClass.GetMouseWorldPosition(), 56);
        }

        if (Input.GetMouseButtonDown(1))
        {
            Debug.Log(grid.GetValue(UtilsClass.GetMouseWorldPosition()));
        }
    }
}
