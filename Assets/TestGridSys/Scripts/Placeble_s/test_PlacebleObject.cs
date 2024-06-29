using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
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
    public bool isBuilding = false;
    public bool cannotBeBuiltOnTop = false;

    [SerializeField] bool thisIsBase = false;
    [SerializeField] bool isConnectionActive = false;

    Vector3[] connectionGridsRefs;
    test_PlacebleObject[] connections;
    GameObject[] connectionColliders;
    [HideInInspector] public Vector3[] GetConnectionGridRefs() { return connectionGridsRefs; }
    public bool IsThisBase { get => thisIsBase;}

    test_GridXYZ grid;


    public Sprite DisconnectedSprite;
    GameObject DisconnectedSpriteRef;

    private void Start()
    {
        if (thisIsBase) { isConnectionActive = true; }
        CreateDisconnectedSprite();
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
        test_PlacebleObject.isBuilding = test_PlacebleObjectSCO.isBuilding;
        test_PlacebleObject.cannotBeBuiltOnTop = test_PlacebleObjectSCO.cannotBeBuiltOnTop;
        test_PlacebleObject.thisIsBase = test_PlacebleObjectSCO.thisIsBase;
        test_PlacebleObject.gridIndex = gridIndex;
        test_PlacebleObject.gridIndex_s = new Vector3[test_PlacebleObjectSCO.x_size * test_PlacebleObjectSCO.y_size * test_PlacebleObjectSCO.z_size];
        test_PlacebleObject.connections = new test_PlacebleObject[test_PlacebleObjectSCO.connectionPoints.Length];
        test_PlacebleObject.CheckConnectionGrid();
        test_PlacebleObject.CreateConnetionSideCollider();

        return test_PlacebleObject;
    }

    float timer = 0;
    bool workOnce = false;

    private void Update()
    {
       Produce(IsBuildingActive());
       DisconnectedSpriteRef.SetActive(!IsBuildingActive());
       if (DisconnectedSpriteRef.activeSelf) { DisconnectedSpriteRef.transform.LookAt(Camera.main.transform); }
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
            else if (timer > test_PlacebleObjectSCO.productionTime && !workOnce)
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

                    for (int j = 0; j < placebleObj.GetConnectionGridRefs().Length; j++)
                    {
                        // objenin baðlantý noktalarýndan birisi bu objeye bakýyor ise

                        for (int g = 0; g < test_PlacebleObjectSCO.connectionPoints.Length; g++)
                        {
                            Vector3 currentGridIndex = gridIndex + test_PlacebleObjectSCO.connectionPoints[g].connectionPointsPosition[0];
                            if (placebleObj.GetConnectionGridRefs()[j] == currentGridIndex)
                            {
                                placebleObj.connections[j] = this;
                                connections[i] = placebleObj;
                                // ve o obje aktif ise 
                                if (placebleObj.IsBuildingActive())
                                {
                                    // baðlantýyý aktif hale getir
                                    isConnectionActive = true;
                                }
                            }
                        }
                       
                    }
                }
            }
        }
        if (thisIsBase)
        {
            isConnectionActive = true;
        }

        if (isConnectionActive) { ActivateConnections(); }
    }

    public void ActivateConnections()
    {
        for (int i = 0; i < connections.Length; i++)
        {
            if (connections[i] != null && !connections[i].isConnectionActive)
            {
                connections[i].isConnectionActive = true;
                connections[i].ActivateConnections();
            }
        }
    }

    public void DestroySelf()
    {
        test_BaseGrid test_BaseGrid;
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
        DisableAllBuildings();
        FindBaseAndActivateBuildings();
    }

    public void DisableAllBuildings()
    {
        foreach (var building in GameManager.GetGameManagerInstance.GetGridBuildingSystem.test_PlacebleObjects)
        {
            if (!building.IsThisBase)
            {
                building.isConnectionActive = false;
            }
        }
    }


    public void FindBaseAndActivateBuildings()
    {
        test_PlacebleObject baseBuilding = null;
        foreach (var building in GameManager.GetGameManagerInstance.GetGridBuildingSystem.test_PlacebleObjects)
        {
            if (building.IsThisBase)
            {
                baseBuilding = building;
            }
        }

        if (baseBuilding != null)
        {
            baseBuilding.ActivateConnections();
        }
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

    public void CreateConnetionSideCollider()
    {
        for (int i = 0; i < test_PlacebleObjectSCO.connectionPoints.Length; i++)
        {
            GameObject colliderObj = new GameObject();
            colliderObj.AddComponent<BoxCollider>();
            colliderObj.transform.localScale = new Vector3(0.85f, 0.85f, 0.85f);
            colliderObj.transform.SetParent(transform);
            colliderObj.transform.localPosition = Vector3.zero;
            colliderObj.layer = 6;

            float yPos = 0.5f + test_PlacebleObjectSCO.connectionPoints[i].connectionPointsPosition[0].y;
            switch (test_PlacebleObjectSCO.connectionPoints[i].connectionPointsRotation[0])
            {
                case 0:
                    colliderObj.transform.localPosition = new Vector3(0, yPos, -1.3f);
                    break;
                case 45:
                    colliderObj.transform.localPosition = new Vector3(-0.85f, yPos, -0.85f);
                    colliderObj.transform.rotation = Quaternion.Euler(0,45,0); 
                    break;
                case 90:
                    colliderObj.transform.localPosition = new Vector3(-1.3f, yPos, 0);
                    break;
                case 135:
                    if (isSquare) { colliderObj.transform.localPosition = new Vector3(-0.85f/2, yPos, 0.85f/2); }
                    else { colliderObj.transform.localPosition = new Vector3(-0.85f, yPos, 0.85f); }
                    colliderObj.transform.rotation = Quaternion.Euler(0, 45, 0);
                    break;
                case 180:
                    colliderObj.transform.localPosition = new Vector3(0, yPos, 1.3f);
                    break;
                case 225:
                    colliderObj.transform.localPosition = new Vector3(0.85f, yPos, 0.85f);
                    colliderObj.transform.rotation = Quaternion.Euler(0, 45, 0);
                    break;
                case 270:
                    colliderObj.transform.localPosition = new Vector3(1.3f, yPos, 0);
                    break;
                case 315:
                    if (isSquare) { colliderObj.transform.localPosition = new Vector3(0.85f / 2, yPos, -0.85f / 2); }
                    else { colliderObj.transform.localPosition = new Vector3(0.85f, yPos, -0.85f); }

                    colliderObj.transform.rotation = Quaternion.Euler(0, 45, 0);
                    break;
                default:
                    colliderObj.transform.localPosition = new Vector3(0, yPos, -1.3f);
                    break;
            }
        }
       
    }


    public void CreateDisconnectedSprite()
    {
        DisconnectedSpriteRef = new GameObject();
        SpriteRenderer objRenderer = DisconnectedSpriteRef.AddComponent<SpriteRenderer>();
        objRenderer.sprite = DisconnectedSprite;
        objRenderer.color = Color.red;
        objRenderer.sortingLayerName = "UILayer";
        DisconnectedSpriteRef.transform.parent = this.gameObject.transform;
        DisconnectedSpriteRef.transform.localPosition = Vector3.zero + new Vector3(0, test_PlacebleObjectSCO.y_size + 0.5f, 0);
        DisconnectedSpriteRef.SetActive(false);
    }

}
