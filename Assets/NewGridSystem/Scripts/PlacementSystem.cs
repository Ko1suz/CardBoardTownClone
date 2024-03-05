using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacementSystem : MonoBehaviour
{
    [SerializeField] GameObject mouseIndicator, cellIndicator;
    [SerializeField] InputManager inputManager;
    [SerializeField] Grid grid;

    private void Update()
    {
        Vector3 mousePostion = inputManager.GetSelectedMapPosition();
        Vector3Int gridPosition = grid.WorldToCell(mousePostion);
        mouseIndicator.transform.position = mousePostion;   
        cellIndicator.transform.position = grid.CellToWorld(gridPosition);
    }
}
