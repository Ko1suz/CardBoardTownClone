#if UNITY_EDITOR
using CodeMonkey.Utils;
using UnityEditor;
#endif
using UnityEngine;
using static CM_PlacedObjectTypeSO;


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

    public bool buildMode = false;
    public test_PlacebleObjectSCO[] test_PlacebleObjectSOs;
    GameObject visualClone;
    public int index = 0;

    private int[] directions = { 0, 45, 90, 135, 180, 225, 270, 315 };
    [Range(0,7)]
    public int directionindex;
    public int directionValue;
    // Start is called before the first frame update

    void Start()
    {
        test_GridXYZ = new test_GridXYZ(x, y, z, gridCellSize, gridPosition);
    }

    private void Update()
    {
        SetBuildMode();
        BuildingGhost();
        if (Input.GetKey(KeyCode.Alpha1))
        {
            index = 0;
            Destroy(visualClone);
            visualClone = null;
        }
        if (Input.GetKey(KeyCode.Alpha2))
        {
            index = 1;
            Destroy(visualClone);
            visualClone = null;
        }
        if (Input.GetKey(KeyCode.Alpha3))
        {
            index = 2;
            Destroy(visualClone);
            visualClone = null;
        }
        if (Input.GetKey(KeyCode.Alpha4))
        {
            index = 3;
            Destroy(visualClone);
            visualClone = null;
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            test_GridXYZ.GetGridIndexAtWorldPosition(CM_Testing.GetMousePos3D(), out int x, out int y, out int z);
            test_BaseGrid test_BaseGrid = test_GridXYZ.GetGridObject(x, y - 1, z);
            if(!test_BaseGrid.CanBuild())
            {
                Destroy(test_BaseGrid.ClearPlacedObjcet());
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
            Debug.Log(string.Format("pozisyonun x y z deðerleri {0},{1},{2}", x, y, z));
            if (x >= 0 && canIPlace)
            {
                test_BaseGrid test_BaseGrid = test_GridXYZ.GetGridObject(x, y, z);
                if (CheclAllConditions(x,y,z, true))
                {
                    GameObject cloneBuilding = Instantiate(test_PlacebleObjectSOs[index].prefab.gameObject, test_GridXYZ.GetWorldPositionGrid(x, y, z), Quaternion.Euler(0, directionValue, 0));
                    test_BaseGrid.SetPlacedObject(cloneBuilding.transform);
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
        test_BaseGrid test_BaseGrid = default;

        test_GridXYZ.GetGridIndexAtWorldPosition(CM_Testing.GetMousePos3D(), out int x, out int y, out int z);
        test_BaseGrid = test_GridXYZ.GetGridObject(x, y, z);
        Debug.Log("Grid Index = " + x + " " + y + " " + z);

        return test_BaseGrid;
    }


    bool CheclAllConditions(int xIndex, int yIndex, int zIndex, bool setResources = false)
    {
        if (CheckGridAndBuildingType(xIndex, yIndex, zIndex) &&
        CheckBuildingBorders(yIndex) &&
        CheckGridItSelf(xIndex, yIndex, zIndex) &&
        CheckSideGrids(xIndex, yIndex, zIndex) &&
        GameManager.GetGameManagerInstance.CheckBuildingCost(test_PlacebleObjectSOs[index], setResources))
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
        if (gridRef.isSquareGrid && !test_PlacebleObjectSOs[index].isSquare)
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
        if (yIndex >= test_PlacebleObjectSOs[index].minPlaceIndexs.y &&  yIndex <= test_PlacebleObjectSOs[index].maxPlaceIndexs.y)
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
            Debug.LogWarning("Kontrol edilen x =" + x);
            if (gridRef == null)
            {
                isPlaceble++;
            }
            else if (gridRef.CheckPlacedObject() || test_PlacebleObjectSOs[index].isSquare)
            {
                isPlaceble++;
            }
        }
        for (int z = -1 + zIndex; z <= 1 + zIndex; z += 2)
        {
            gridRef = test_GridXYZ.GetGridObject(xIndex, yIndex, z);
            Debug.LogWarning("Kontrol edilen z =" + z);
            if (gridRef == null)
            {
                isPlaceble++;
            }
            else if (gridRef.CheckPlacedObject() || test_PlacebleObjectSOs[index].isSquare)
            {
                isPlaceble++;
            }
        }
        Debug.LogWarning("Boþ alan = " + isPlaceble);
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
                    visualClone = Instantiate(test_PlacebleObjectSOs[index].visual.gameObject, CM_Testing.GetMousePos3D(), Quaternion.Euler(0, directions[directionindex], 0));
                }
              
                if (!CheclAllConditions(gridRef.GetXIndex(), gridRef.GetYIndex(), gridRef.GetZIndex()))
                {
                    Renderer renderer = visualClone.GetComponent<Renderer>();
                    Material material = renderer.material;
                    material.SetColor("_EmissionColor", Color.red * Mathf.LinearToGammaSpace(5.8f));
                    SetBuilding(false);
                }
                else
                {
                    float green = 191;
                    float red = 0;
                    float blue = 190;
                    Color blueEmmisonColor = new Color(red / 255f, green / 255f, blue / 255f);
                    Renderer renderer = visualClone.GetComponent<Renderer>();
                    Material material = renderer.material;
                    material.SetColor("_EmissionColor", blueEmmisonColor * Mathf.LinearToGammaSpace(5.8f));
                    SetBuilding(true);
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
