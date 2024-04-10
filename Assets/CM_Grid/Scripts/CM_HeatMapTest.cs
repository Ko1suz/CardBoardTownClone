using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CM_HeatMapTest : MonoBehaviour
{
    [SerializeField] private CM_HeatMapVisual heatMapVisual;
    [SerializeField] private CM_HeatMapBoolVisual heatMapBoolVisual;
    [SerializeField] private CM_HeatMapGenericVisual heatMapGenericVisual;
    CM_Grid<CM_HeatMapGridObject> grid;
    CM_Grid<CM_StringGridObject> stringGrid;
    void Start()
    {
        //grid = new CM_Grid<CM_HeatMapGridObject>(10, 10, 10f, new Vector3(-50, -50, 0), 
        //    (CM_Grid<CM_HeatMapGridObject> g, int x, int y) => new CM_HeatMapGridObject(g,x,y));

        stringGrid = new CM_Grid<CM_StringGridObject>(10, 10, 10f, new Vector3(-50, -50, 0),(CM_Grid<CM_StringGridObject> g, int x, int y) => new CM_StringGridObject(g, x, y));

        //heatMapVisual.SetGrid(grid);
        // heatMapBoolVisual.SetGrid(grid);
        //heatMapGenericVisual.SetGrid(grid);
    }

    private void Update()
    {
        Vector3 position = CM_Testing.GetMousePos2D();
        if (Input.GetMouseButtonDown(0))
        {
          
            //int value = grid.GetValue(position);
            //grid.SetValue(position, value + 5);

            CM_HeatMapGridObject cm_heatMapGridObject = grid.GetGridObject(position);
            if (cm_heatMapGridObject != null){
                cm_heatMapGridObject.AddValue(5);
            }
        }

        if (Input.GetKeyDown(KeyCode.A)) { stringGrid.GetGridObject(position).AddLetter("A"); }
        if (Input.GetKeyDown(KeyCode.B)) { stringGrid.GetGridObject(position).AddLetter("B"); }
        if (Input.GetKeyDown(KeyCode.C)) { stringGrid.GetGridObject(position).AddLetter("C"); }

        if (Input.GetKeyDown(KeyCode.Alpha0)) { stringGrid.GetGridObject(position).AddLetter("0"); }
        if (Input.GetKeyDown(KeyCode.Alpha1)) { stringGrid.GetGridObject(position).AddLetter("1"); }
        if (Input.GetKeyDown(KeyCode.Alpha2)) { stringGrid.GetGridObject(position).AddLetter("2"); }
    }
}

public class CM_HeatMapGridObject
{
    private const int MIN = 0;
    private const int MAX = 100;


    private CM_Grid<CM_HeatMapGridObject> grid;
    private int x;
    private int y;
    public int value;

    public CM_HeatMapGridObject(CM_Grid<CM_HeatMapGridObject> grid, int x, int y)
    {
        this.grid = grid;
        this.x = x;
        this.y = y;
    }
    public void AddValue(int addValue)
    {
        value += addValue;
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

public class CM_StringGridObject
{
    private CM_Grid<CM_StringGridObject> grid;
    int x, y;
    public string letters, numbers;

    public CM_StringGridObject(CM_Grid<CM_StringGridObject> grid, int x, int y)
    {
        this.grid = grid;
        this.x = x;
        this.y = y;
        letters = "";
        numbers = "";
    }
    
    public void AddLetter(string letter)
    {
        letters += letter;
        grid.TriggerGridObjectChanged(x, y);
    }
    public void AddNumber(string number)
    {
        numbers += number;
        grid.TriggerGridObjectChanged(x, y);
    }

    public override string ToString()
    {
        return letters + "\n" + numbers;
    }
}

