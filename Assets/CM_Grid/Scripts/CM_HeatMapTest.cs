using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CM_HeatMapTest : MonoBehaviour
{
    [SerializeField] private CM_HeatMapVisual heatMapVisual;
    CM_Grid grid;
    void Start()
    {
        grid = new CM_Grid(100, 100, 4f, new Vector3(-200, -200, 0));   
        heatMapVisual.SetGrid(grid);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 position = CM_Testing.GetMousePos2D();
            //int value = grid.GetValue(position);
            //grid.SetValue(position, value + 5);

            grid.AddValue(position, 100, 5, 20);
        }
    }
}
