using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;
using static UnityEngine.Rendering.DebugUI;
using UnityEngine.UIElements;

public class Testing : MonoBehaviour
{
    [SerializeField] private HeatMapVisual heatMapVisual;
    [SerializeField] private HeatMapBoolVisual heatMapBoolVisual;
    [SerializeField] private HeatMapGenericVisual heatMapGenericVisual;
    private Grid<HeatMapGridObject> grid;
    private Grid<StringGridObject> stringGrid;
    private void Start()
    {
        //grid = new Grid<HeatMapGridObject>(10, 10, 10f, new Vector3(-50, -50), (Grid<HeatMapGridObject> g, int x, int y) => new HeatMapGridObject(g, x, y), false);
        stringGrid = new Grid<StringGridObject>(10, 10, 10f, new Vector3(-50, -50), (Grid<StringGridObject> g, int x, int y) => new StringGridObject(g, x, y), false);
        //heatMapVisual.SetGrid(grid);
        //heatMapBoolVisual.SetGrid(grid);  
        //heatMapGenericVisual.SetGrid(grid);
    }

    private void Update()
    {
        Vector3 position = UtilsClass.GetMouseWorldPosition();

        //if (Input.GetMouseButtonDown(0))
        //{
        //    HeatMapGridObject heatMapGridObject = grid.GetGridObject(position);
        //    if (heatMapGridObject != null)
        //    {
        //        heatMapGridObject.AddValue(5);
        //    }
        //    //grid.SetValue(position, true);
        //    //grid.AddValue(position, 100, 5, 20);
        //}

        if (Input.GetKeyDown(KeyCode.A)) { stringGrid.GetGridObject(position).AddLetter("A"); };
        if (Input.GetKeyDown(KeyCode.B)) { stringGrid.GetGridObject(position).AddLetter("B"); };
        if (Input.GetKeyDown(KeyCode.C)) { stringGrid.GetGridObject(position).AddLetter("C"); };

        if (Input.GetKeyDown(KeyCode.Alpha1)) { stringGrid.GetGridObject(position).AddNumber("1"); };
        if (Input.GetKeyDown(KeyCode.Alpha2)) { stringGrid.GetGridObject(position).AddNumber("2"); };
        if (Input.GetKeyDown(KeyCode.Alpha3)) { stringGrid.GetGridObject(position).AddNumber("3"); };
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

public class StringGridObject
{
    private Grid<StringGridObject> grid;
    public int x;
    public int y;


    private string letters;
    private string numbers;

    public StringGridObject(Grid<StringGridObject> grid, int x, int y)
    {
        this.grid = grid;
        this.x = x;
        this.y = y;
        letters = "";
        numbers = "";
    }
    public void AddNumber(string number)
    {
        numbers += number;
        grid.TriggerGridObjectChanged(x, y);
    }

    public void AddLetter(string letter)
    {
        letters += letter;
        grid.TriggerGridObjectChanged(x, y);
    }

    public override string ToString()
    {
        return letters + "\n" + numbers;
    }
}
