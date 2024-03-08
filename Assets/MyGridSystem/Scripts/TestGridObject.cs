using UnityEngine;

public class TestGridObject
{
    public GameObject gridObject;
    public int width;
    public int height;
    public float cellSize;
    public Vector3 originPosition;

    public TestGridObject(GameObject gridObject, int width, int height, float cellsize, Vector3 originPosition)
    {
        this.gridObject = gridObject;
        this.width = width;
        this.height = height;
        this.cellSize = cellsize;
        this.originPosition = originPosition;
    }
}
