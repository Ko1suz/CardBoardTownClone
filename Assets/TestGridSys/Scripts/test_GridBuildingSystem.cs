#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using System.Collections.Generic;
using System;
using CodeMonkey.Utils;

public class test_GridBuildingSystem : MonoBehaviour
{
#if UNITY_EDITOR
    void OnValidate()
    {
        totalGridCount = x * y * z;
    }
#endif
    [HideInInspector]public int totalGridCount;
    [Range(1, 50)]
    public int x;
    [Range(1, 50)]
    public int y;
    [Range(1, 50)]
    public int z;
    [Range(1, 10)]
    public int gridCellSize;

    public Vector3 gridPosition;
    test_GridXYZ test_GridXYZ;
    public test_GridXYZ GetGridXYZ { get => test_GridXYZ; }

    public bool buildMode = false;
    public test_PlacebleObjectSCO selectedSco;
    GameObject visualClone;
    public int index = 0;

    private int[] directions = { 0, 45, 90, 135, 180, 225, 270, 315 };
    [Range(0,7)]
    public int directionindex;
    public int directionValue;
    // Start is called before the first frame update

    private int selectedX = 0;
    private int selectedY = 0;
    private int selectedZ = 0;
    private bool canIplace = false;

    public List<test_PlacebleObject> test_PlacebleObjects;

    MaterialSetter materialSetter;

    public event EventHandler OnSelectingGridChange;

    void CheckSelectedGridChange()
    {
        test_GridXYZ.GetGridIndexAtWorldPosition(CM_Testing.GetMousePos3D(), out int x, out int y, out int z);
        if (selectedX != x || selectedY != y || selectedZ != z)
        {
            OnSelectingGridChange?.Invoke(this, EventArgs.Empty);
            selectedX = x;  
            selectedY = y;
            selectedZ = z;
        }
    }
    void CheckSelectedGridChange(bool eventFire = false)
    {
        if (eventFire)
        {
            OnSelectingGridChange?.Invoke(this, EventArgs.Empty);
        }
    }

    void Start()
    {
        test_GridXYZ = new test_GridXYZ(x, y, z, gridCellSize, gridPosition);
        OnSelectingGridChange += CheckConditons;
        materialSetter = GetComponent<MaterialSetter>();
    }

    private void Update()
    {
        CheckSelectedGridChange();
        SetBuildMode();
        BuildingGhost();
        SetBuilding(canIplace);
        if (Input.GetKey(KeyCode.Alpha1))
        {
            index = 0;
            Destroy(visualClone);
            visualClone = null;
            CheckSelectedGridChange(true);
        }
        if (Input.GetKey(KeyCode.Alpha2))
        {
            index = 1;
            Destroy(visualClone);
            visualClone = null;
            CheckSelectedGridChange(true);
        }
        if (Input.GetKey(KeyCode.Alpha3))
        {
            index = 2;
            Destroy(visualClone);
            visualClone = null;
            CheckSelectedGridChange(true);
        }
        if (Input.GetKey(KeyCode.Alpha4))
        {
            index = 3;
            Destroy(visualClone);
            visualClone = null;
            CheckSelectedGridChange(true);
        }
        if (Input.GetKey(KeyCode.Alpha5))
        {
            index = 4;
            Destroy(visualClone);
            visualClone = null;
            CheckSelectedGridChange(true);
        }
        if (Input.GetKey(KeyCode.Alpha6))
        {
            index = 5;
            Destroy(visualClone);
            visualClone = null;
            CheckSelectedGridChange(true);
        }
        if (Input.GetKey(KeyCode.Alpha7))
        {
            index = 6;
            Destroy(visualClone);
            visualClone = null;
            CheckSelectedGridChange(true);
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            test_GridXYZ.GetGridIndexAtWorldPosition(CM_Testing.GetMousePos3D(), out int x, out int y, out int z);
            test_BaseGrid test_BaseGrid = test_GridXYZ.GetGridObject(x, y - 1, z);
            if(!test_BaseGrid.CanBuild())
            {
                //Destroy(test_BaseGrid.ClearPlacedObjcet());
                test_PlacebleObject objRef = test_BaseGrid.GetPlacedObjet().GetComponent<test_PlacebleObject>();
                test_PlacebleObjects.Remove(objRef);
                objRef.DestroySelf();
            }
            else
            {
                UtilsClass.CreateWorldTextPopup("Burada kurulu bir þey yok :D?", CM_Testing.GetMousePos3D(), 5);
            }

        }

        Rotate();
    }

    void SetBuilding(bool canIPlace)
    {
        if (Input.GetMouseButtonDown(0))
        {
            test_GridXYZ.GetGridIndexAtWorldPosition(CM_Testing.GetMousePos3D(), out int x, out int y, out int z);
            //test_GridXYZ.GetGridXYZOctagon(CM_Testing.GetMousePos3D(), out int x, out int y, out int z);

            if (x >= 0 && canIPlace && buildMode)
            {
                test_BaseGrid test_BaseGrid = test_GridXYZ.GetGridObject(x, y, z);
                if (CheckAllConditions(x,y,z, true))
                {
                    int xIndex = test_BaseGrid.GetXIndex();
                    int yIndex = test_BaseGrid.GetYIndex();
                    int zIndex = test_BaseGrid.GetZIndex();
                    //GameObject cloneBuilding = Instantiate(test_PlacebleObjectSOs[index].prefab.gameObject, test_GridXYZ.GetWorldPositionGrid(x, y, z), Quaternion.Euler(0, directionValue, 0));
                    test_PlacebleObject cloneBuilding = test_PlacebleObject.Create(test_GridXYZ.GetWorldPositionGrid(x, y, z), new Vector3(xIndex,yIndex,zIndex), directionValue, selectedSco, test_GridXYZ);

                    for (int ySize = 0; ySize < selectedSco.y_size; ySize++)
                    {
                        for (int zSize = 0; zSize < selectedSco.z_size; zSize++)
                        {
                            for (int xSize = 0; xSize < selectedSco.x_size; xSize++)
                            {
                                test_BaseGrid = test_GridXYZ.GetGridObject(x + (xSize), y + (ySize), z + (zSize));
                                test_BaseGrid.SetPlacedObject(cloneBuilding.transform);
                                Debug.Log(string.Format("pozisyonun x y z deðerleri {0},{1},{2}", x + (xSize), y + (ySize), z + (zSize)));
                                test_PlacebleObjects.Add(cloneBuilding);
                            }
                        }
                    }
                }
                else
                {
                    UtilsClass.CreateWorldTextPopup("Burada zaten bina var caným", CM_Testing.GetMousePos3D(), 5);
                }
            }
            else
            {
                //UtilsClass.CreateWorldTextPopup("Buraya Bina koyamazsýn caným", CM_Testing.GetMousePos3D(), 5);
            }
        }
    }

    test_BaseGrid GetMousePosGrid()
    {
        test_BaseGrid test_BaseGrid = null;

        test_GridXYZ.GetGridIndexAtWorldPosition(CM_Testing.GetMousePos3D(), out int x, out int y, out int z);
        if (x>=0)
        {
            test_BaseGrid = test_GridXYZ.GetGridObject(x, y, z);
        }

        return test_BaseGrid;
    }


    bool CheckAllConditions(int xIndex, int yIndex, int zIndex, bool setResources = false)
    {
        if (CheckGridAndBuildingType(xIndex, yIndex, zIndex) &&
        CheckBuildingBorders(yIndex) &&
        CheckGridItSelf(xIndex, yIndex, zIndex) &&
        CheckSideGrids(xIndex, yIndex, zIndex) &&
        GameManager.GetGameManagerInstance.CheckBuildingCost(selectedSco, setResources))
        {
            return true;
        }
        else { return false; }
    }

    // Bina ve Grid Tipini kontrol ediyor
    bool CheckGridAndBuildingType(int xIndex, int yIndex, int zIndex)
    {
        test_BaseGrid gridRef;
        gridRef = test_GridXYZ.GetGridObject(xIndex, yIndex, zIndex);
        if (gridRef.isSquareGrid && !selectedSco.isSquare)
        {
            return false;
        }
        else
        {
            return true;
        }
    }
    // Bina Index Yuksekliðini Kontrol ediyor
    bool CheckBuildingBorders(int yIndex)
    {
        if (yIndex >= selectedSco.minPlaceIndexs.y &&  yIndex <= selectedSco.maxPlaceIndexs.y)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    // Seçili Gridin Boþ olup olmadýðýný kontrol ediyor
    bool CheckGridItSelf(int xIndex, int yIndex, int zIndex)
    {
        test_BaseGrid gridRef;
        gridRef = test_GridXYZ.GetGridObject(xIndex, yIndex, zIndex);
        if (gridRef.CheckPlacedObject())
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    // Gridin 4 farklý yönünde ki diðer gridleri kontrol ediyor eðer seçili Bina tipi Sekizgen ise 
    // Ve orda etrafýnda baþka sekizgen bina varsa oraya bina koyulmuyor
    bool CheckSideGrids(int xIndex, int yIndex, int zIndex)
    {
        int isPlaceble = 0;
        test_BaseGrid gridRef;
        gridRef = test_GridXYZ.GetGridObject(xIndex, yIndex, zIndex);

        for (int x = -2 + xIndex; x <= 2 + xIndex; x += 4)
        {
            gridRef = test_GridXYZ.GetGridObject(x, yIndex, zIndex);
            //Debug.LogWarning("Kontrol edilen x =" + x);
            if (gridRef == null)
            {
                isPlaceble++;
            }
            else if (gridRef.CheckPlacedObject() || selectedSco.isSquare)
            {
                isPlaceble++;
            }
            else if (gridRef.GetPlacedObjet().GetComponent<test_PlacebleObject>().isSquare)
            {
                isPlaceble++;
            }
        }
        for (int z = -1 + zIndex; z <= 1 + zIndex; z += 2)
        {
            gridRef = test_GridXYZ.GetGridObject(xIndex, yIndex, z);
            //Debug.LogWarning("Kontrol edilen z =" + z);
            if (gridRef == null)
            {
                isPlaceble++;
            }
            else if (gridRef.CheckPlacedObject() || selectedSco.isSquare)
            {
                isPlaceble++;
            }
            else if (gridRef.GetPlacedObjet().GetComponent<test_PlacebleObject>().isSquare)
            {
                isPlaceble++;
            }
        }
        //Debug.LogWarning("Boþ alan = " + isPlaceble);
        if (isPlaceble >=4){ return true; }
        else { return false; }
    }

    void SetBuildMode()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            if (!buildMode)
            {
                buildMode = true;
            }
            else
            {
                buildMode = false;
                Destroy(visualClone);
                visualClone = null;
            }
        }
    }
    Vector3 last_GridRefPos = Vector3.zero;

    void BuildingGhost()
    {
        if (buildMode)
        {
            test_BaseGrid gridRef = GetMousePosGrid();


            if (gridRef != null)
            {
                last_GridRefPos = gridRef.GetWorldPosition();
                if (visualClone == null)
                {
                    visualClone = Instantiate(selectedSco.visual.gameObject, CM_Testing.GetMousePos3D(), Quaternion.Euler(0, directions[directionindex], 0));
                }
                visualClone.transform.position = Vector3.Lerp(visualClone.transform.position, gridRef.GetWorldPosition(), Time.deltaTime * 10);
                visualClone.transform.rotation = Quaternion.Lerp(visualClone.transform.rotation, Quaternion.Euler(0, directions[directionindex], 0), Time.deltaTime * 5);
            }
            else
            {
                if (visualClone != null)
                {
                    visualClone.transform.position = Vector3.Lerp(visualClone.transform.position, last_GridRefPos, Time.deltaTime * 10);
                }
            }

            if (visualClone != null)
            {
                if (canIplace)
                {
                    materialSetter.SetObjMatBlue();
                    //SetBuilding(true);
                }
                else
                {
                    materialSetter.SetObjMatRed();
                    //SetBuilding(false);
                }
            }
          
        }
    }
    public void CheckConditons(object sender, EventArgs e)
    {
        test_BaseGrid gridRef = GetMousePosGrid();
        if (gridRef == null)
        {
            canIplace = false;
        }
        else if (!CheckAllConditions(gridRef.GetXIndex(), gridRef.GetYIndex(), gridRef.GetZIndex()))
        {
            canIplace = false;
        }
        else
        {
            canIplace = true;
        }
    }

    void Rotate()
    {
        if (Input.GetKeyDown(KeyCode.R) && buildMode)
        {
            test_GridXYZ.GetGridIndexAtWorldPosition(CM_Testing.GetMousePos3D(), out int x, out int y, out int z);
            test_BaseGrid gridRef = test_GridXYZ.GetGridObject(x, y, z);
            if (!gridRef.isSquareGrid)
            {
                if (directionindex < 7)
                {
                    directionindex++;
                }
                else
                {
                    directionindex = 0;
                }
            }
            else
            {
                if (directionindex % 2 != 0 && directionindex >= 0)
                {
                    directionindex--;
                }
                else if (directionindex < 6)
                {
                    directionindex += 2;
                }
                else
                {
                    directionindex = 0;
                }
            }
            directionValue = directions[directionindex];
            UtilsClass.CreateWorldTextPopup("Direction ->" + directions[directionindex], CM_Testing.GetMousePos3D(), 12);
        }
    }
}

#if UNITY_EDITOR
[CustomEditor(typeof(test_GridBuildingSystem))]
public class test_GridBuildingSystemEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        test_GridBuildingSystem myScript = (test_GridBuildingSystem)target;
        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Toplam Grid Sayýsý: " + myScript.totalGridCount.ToString());
    }
}
#endif
