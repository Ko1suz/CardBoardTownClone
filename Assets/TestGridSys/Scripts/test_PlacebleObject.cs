using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test_PlacebleObject : MonoBehaviour
{
    test_PlacebleObjectSCO test_PlacebleObjectSCO;
    Vector3 worldPos;
    Vector3 gridIndex;
    int dir;
    public bool isSquare = false;

    test_GridXYZ grid;

    public static test_PlacebleObject Create(Vector3 worldPos, Vector3 gridIndex, int direction, test_PlacebleObjectSCO test_PlacebleObjectSCO, test_GridXYZ grid)
    {
        Transform placedObjectTransform = Instantiate(test_PlacebleObjectSCO.prefab, worldPos, Quaternion.Euler(0,direction,0));
        test_PlacebleObject test_PlacebleObject = placedObjectTransform.GetComponent<test_PlacebleObject>();

        test_PlacebleObject.test_PlacebleObjectSCO = test_PlacebleObjectSCO;
        test_PlacebleObject.worldPos = worldPos;
        test_PlacebleObject.dir = direction;
        test_PlacebleObject.grid = grid;
        test_PlacebleObject.isSquare = test_PlacebleObjectSCO.isSquare;
        test_PlacebleObject.gridIndex = gridIndex;

        return test_PlacebleObject;
    }

    float timer = 0;
    bool workOnce = false;
    private void Update()
    {
       Produce(IsBuildingActive());
    }

    bool IsBuildingActive()
    {
        if (true)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    void Produce(bool isBuildingActive)
    {
        if (isBuildingActive)
        {
            timer += Time.deltaTime;
            if (test_PlacebleObjectSCO.produced == test_PlacebleObjectSCO.Produced.MatCapacity && !workOnce)
            {
                test_PlacebleObjectSCO.Produce();
                workOnce = true;
            }
            else if (timer > test_PlacebleObjectSCO.productionTime)
            {
                timer = 0;
                test_PlacebleObjectSCO.Produce();
            }
        }
    }

    void ProduceBack()
    {
        test_PlacebleObjectSCO.ProduceBack();
        workOnce = true;
    }

    void CheckConnectionGrid()
    {

    }

    public void DestroySelf()
    {
        test_BaseGrid test_BaseGrid = grid.GetGridObject((int)worldPos.x, (int)worldPos.y, (int)worldPos.z);
        ProduceBack();
        Destroy(test_BaseGrid.ClearPlacedObjcet());
    }
}
