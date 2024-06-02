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
                if (test_BaseGrid.CanBuild())
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
                UtilsClass.CreateWorldTextPopup("Buraya Bina koyamazsýn caným", CM_Testing.GetMousePos3D(), 5);
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
    void BuildingGhost()
    {
        if (buildMode)
        {
            test_BaseGrid test_BaseGridRef = GetMousePosGrid();
            if (visualClone == null)
            {
                visualClone = Instantiate(test_PlacebleObjectSOs[index].visual.gameObject, CM_Testing.GetMousePos3D(), Quaternion.Euler(0, directions[directionindex],0));
            }
            if (test_BaseGridRef.isSquareGrid && !test_PlacebleObjectSOs[index].isSquare)
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
                Color blueEmmisonColor = new Color(red/255f, green/255f, blue/255f);
                Renderer renderer = visualClone.GetComponent<Renderer>();
                Material material = renderer.material;
                material.SetColor("_EmissionColor", blueEmmisonColor * Mathf.LinearToGammaSpace(5.8f));
                SetBuilding(true);
            }
            visualClone.transform.position = Vector3.Lerp(visualClone.transform.position, test_BaseGridRef.GetWorldPosition(), Time.deltaTime * 10);
            visualClone.transform.rotation = Quaternion.Lerp(visualClone.transform.rotation, Quaternion.Euler(0, directions[directionindex],0), Time.deltaTime * 5);
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
