using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;

public class Testing : MonoBehaviour
{
    [SerializeField] private HeatMapVisual heatMapVisual;
    [SerializeField] private HeatMapBoolVisual heatMapBoolVisual;
    private Grid<HeatMapGridObject> grid;
    private void Start()
    {
        grid = new Grid<HeatMapGridObject>
            (10, 10, 10f, new Vector3(-50, -50), (Grid<HeatMapGridObject> g, int x, int y) => new HeatMapGridObject(g, x, y), false);
        //heatMapVisual.SetGrid(grid);
        //heatMapBoolVisual.SetGrid(grid);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 position = UtilsClass.GetMouseWorldPosition();

            HeatMapGridObject heatMapGridObject = grid.GetGridObject(position);
            if (heatMapGridObject != null)
            {
                heatMapGridObject.AddValue(5);
            }

            //grid.SetValue(position, true);
            //grid.AddValue(position, 100, 5, 20);
        }
    }
}

public class HeatMapGridObject
{
    private const int MIN = 0;
    private const int MAX = 100;
    public int x;
    public int y;
    public int value;
    private Grid<HeatMapGridObject> grid;
    public HeatMapGridObject(Grid<HeatMapGridObject> grid, int x, int y)
    {
        this.grid = grid;
        this.x = x;
        this.y = y;
    }

    public void AddValue(int addValue)
    {
        this.value += addValue;
        value = Mathf.Clamp(value, MIN, MAX);
        grid.TriggerGridObjectChanged(x, y);
    }

    public float GetValueNormalized()
    {
        return (float)value / MAX;
    }

    public override string ToString()
    {
        return value.ToString();
    }
}
