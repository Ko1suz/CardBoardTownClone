using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test_PlacebleObject : MonoBehaviour
{
    test_PlacebleObjectSCO test_PlacebleObjectSCO;
    Vector3 worldPos;
    int dir;
    public bool isSquare = false;

    test_GridXYZ grid;

    public static test_PlacebleObject Create(Vector3 worldPos, int direction, test_PlacebleObjectSCO test_PlacebleObjectSCO, test_GridXYZ grid)
    {
        Transform placedObjectTransform = Instantiate(test_PlacebleObjectSCO.prefab, worldPos, Quaternion.Euler(0,direction,0));
        test_PlacebleObject test_PlacebleObject = placedObjectTransform.GetComponent<test_PlacebleObject>();

        test_PlacebleObject.test_PlacebleObjectSCO = test_PlacebleObjectSCO;
        test_PlacebleObject.worldPos = worldPos;
        test_PlacebleObject.dir = direction;
        test_PlacebleObject.grid = grid;
        test_PlacebleObject.isSquare = test_PlacebleObjectSCO.isSquare;

        return test_PlacebleObject;
    }


    public void DestroySelf()
    {
        test_BaseGrid test_BaseGrid = grid.GetGridObject((int)worldPos.x, (int)worldPos.y, (int)worldPos.z);
        Destroy(test_BaseGrid.ClearPlacedObjcet());
    }
}
