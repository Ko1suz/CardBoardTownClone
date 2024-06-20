using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test_PlacebleObject : MonoBehaviour
{
    test_PlacebleObjectSCO test_PlacebleObjectSCO;
    Vector3 worldPos;
    Vector3 gridIndex;
    public Vector3 GetGridIndex { get => gridIndex;}
    Vector3[] gridIndex_s;
    int dir;
    
    public bool isSquare = false;

    [SerializeField] bool thisIsBase = false;
    [SerializeField] bool isConnectionActive = false;

    Vector3[] connectionGridsRefs;
    ConnectionPoint[] connections;
    [HideInInspector] public Vector3[] GetConnectionGridRefs() { return connectionGridsRefs; }

    test_GridXYZ grid;

    int energyLevel = 0;

    private void Start()
    {
        if (thisIsBase) { isConnectionActive = true; }
    }

    public static test_PlacebleObject Create(Vector3 worldPos, Vector3 gridIndex, int direction, test_PlacebleObjectSCO test_PlacebleObjectSCO, test_GridXYZ grid, bool thisIsBase = false)
    {
        Transform placedObjectTransform = Instantiate(test_PlacebleObjectSCO.prefab, worldPos, Quaternion.Euler(0,direction,0));
        test_PlacebleObject test_PlacebleObject = placedObjectTransform.GetComponent<test_PlacebleObject>();

        test_PlacebleObject.test_PlacebleObjectSCO = test_PlacebleObjectSCO;
        test_PlacebleObject.worldPos = worldPos;
        test_PlacebleObject.dir = direction;
        test_PlacebleObject.grid = grid;
        test_PlacebleObject.isSquare = test_PlacebleObjectSCO.isSquare;
        test_PlacebleObject.thisIsBase = test_PlacebleObjectSCO.thisIsBase;
        test_PlacebleObject.gridIndex = gridIndex;
        test_PlacebleObject.gridIndex_s = new Vector3[test_PlacebleObjectSCO.x_size * test_PlacebleObjectSCO.y_size * test_PlacebleObjectSCO.z_size];
        test_PlacebleObject.CheckConnectionGrid();
        test_PlacebleObject.connections = new ConnectionPoint[test_PlacebleObjectSCO.connectionPoints.Length];
        return test_PlacebleObject;
    }

    float timer = 0;
    bool workOnce = false;

    private void Update()
    {
       Produce(IsBuildingActive());
    }

    public bool IsBuildingActive()
    {
        if (isConnectionActive)
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

    public void CheckConnectionGrid()
    {
        test_BaseGrid cell = grid.GetGridObject((int)gridIndex.x, (int)gridIndex.y, (int)gridIndex.z);
        connectionGridsRefs = new Vector3[test_PlacebleObjectSCO.connectionPoints.Length];

        int emptyConnections = 0;

        for (int i = 0; i < test_PlacebleObjectSCO.connectionPoints.Length; i++)
        {
            Vector3 Index = gridIndex + test_PlacebleObjectSCO.connectionPoints[i].connectionPointsPosition[0] +
                GetRotationSideIndex(test_PlacebleObjectSCO.connectionPoints[i].connectionPointsRotation[0], cell.isSquareGrid);

            connectionGridsRefs[i] = Index;
            //Debug.LogError("Kontrol edilen baðlantý noktasý " + Index);

            // Baðlantý noktalarýnýn olduðu yönde ki Indexler hesaplanýr
            test_BaseGrid gridObj = grid.GetGridObject((int)Index.x, (int)Index.y, (int)Index.z);
            if (gridObj != null)
            {
                Transform placebleObjTransform = gridObj.GetPlacedObjet();
                if (placebleObjTransform != null)
                {
                    test_PlacebleObject placebleObj = placebleObjTransform.GetComponent<test_PlacebleObject>();
                    // Bina çok katlý vb ise asýl grid indexini referans alarak tekrar hesaplar
                    gridObj = grid.GetGridObject((int)placebleObj.GetGridIndex.x, (int)placebleObj.GetGridIndex.y, (int)placebleObj.GetGridIndex.z);
                    placebleObjTransform = gridObj.GetPlacedObjet();
                    placebleObj = placebleObjTransform.GetComponent<test_PlacebleObject>();

                    Debug.Log(placebleObj.GetConnectionGridRefs().Length);

                    for (int j = 0; j < placebleObj.GetConnectionGridRefs().Length; j++)
                    {
                        // objenin baðlantý noktalarýndan birisi bu objeye bakýyor ise
                        if (placebleObj.GetConnectionGridRefs()[j] == gridIndex)
                        {
                            connections[i].connectionPointObj = placebleObj;
                            // ve o obje aktif ise 
                            if (placebleObj.IsBuildingActive())
                            {
                                // baðlantýyý aktif hale getir
                                isConnectionActive = true;
                            }
                        }
                    }
                }
                else if (placebleObjTransform == null)
                {
                    emptyConnections++;
                    if (emptyConnections == test_PlacebleObjectSCO.connectionPoints.Length)
                    {
                        isConnectionActive = false;
                    }
                }
            }
            else
            {
                emptyConnections++;
                if (emptyConnections == test_PlacebleObjectSCO.connectionPoints.Length)
                {
                    isConnectionActive = false;
                }
            }
        }
        if (thisIsBase)
        {
            isConnectionActive = true;
        }
    }

    public void TriggerConnectionPointObjectsCheckSystem()
    {
        for (int i = 0; i < test_PlacebleObjectSCO.connectionPoints.Length; i++)
        {
            test_BaseGrid gridObj = grid.GetGridObject((int)connectionGridsRefs[i].x, (int)connectionGridsRefs[i].y, (int)connectionGridsRefs[i].z);
            if (gridObj != null)
            {
                Transform placebleObjTransform = gridObj.GetPlacedObjet();
                if (placebleObjTransform != null)
                {
                    test_PlacebleObject placedObj = placebleObjTransform.GetComponent<test_PlacebleObject>();
                    if (isConnectionActive && !thisIsBase) { placedObj.CheckConnectionGrid(); placedObj.TriggerConnectionPointObjectsCheckSystem(); }

                }
            }
        }
    }

    public void DestroySelf()
    {
        test_BaseGrid test_BaseGrid = grid.GetGridObject((int)gridIndex.x, (int)gridIndex.y, (int)gridIndex.z);
        ProduceBack();
        //Destroy(test_BaseGrid.ClearPlacedObjcet());
        for (int ySize = 0; ySize < test_PlacebleObjectSCO.y_size; ySize++)
        {
            for (int zSize = 0; zSize < test_PlacebleObjectSCO.z_size; zSize++)
            {
                for (int xSize = 0; xSize < test_PlacebleObjectSCO.x_size; xSize++)
                {
                    test_BaseGrid = grid.GetGridObject((int)gridIndex.x + (xSize), (int)gridIndex.y + (ySize), (int)gridIndex.z + (zSize));
                    Destroy(test_BaseGrid.ClearPlacedObjcet());
                }
            }
        }

        TriggerConnectionPointObjectsCheckSystem();
    }

    public Vector3 GetRotationSideIndex(int connectPointDirection, bool isSquareGrid = false)
    {
        int connectPointDir = dir + connectPointDirection;
        if (connectPointDir >= 360)
        {
            connectPointDir -= 360;
        }
        //Debug.LogError("Baðlantý noktasýnýn baktýðý rotasyon" + connectPointDir);
        if (isSquareGrid)
        {
            switch (connectPointDir)
            {
                case 45:
                    return new Vector3(-1, 0, 0);
                case 135:
                    return new Vector3(-1, 0, 1);
                case 225:
                    return new Vector3(1, 0, 1);
                case 315:
                    return new Vector3(1, 0, 0);
                default:
                    return new Vector3(0, 0, 0);
            }
        }
        else
        {
            switch (connectPointDir)
            {
                case 0:
                    return new Vector3(0, 0, -1);
                case 45:
                    return new Vector3(-1, 0, -1);
                case 90:
                    return new Vector3(-2, 0, 0);
                case 135:
                    return new Vector3(-1, 0, 0);
                case 180:
                    return new Vector3(0, 0, 1);
                case 225:
                    return new Vector3(1, 0, 0);
                case 270:
                    return new Vector3(2, 0, 0);
                case 315:
                    return new Vector3(1, 0, -1);
                default:
                    return new Vector3(0, 0, 0);
            }
        }  
    }

    public void CheckConnectionsAndGetEnergy()
    {

    }

    public void CheckConnectionsAndGiveEnergy()
    {

    }

    public void SetEnergy(int value)
    {
        if (!thisIsBase)
        {
            energyLevel += value;
        }
    }
}

public struct ConnectionPoint
{
    public test_PlacebleObject connectionPointObj;
    public int receivedEnergy;
    public int givenEnergy;
}
