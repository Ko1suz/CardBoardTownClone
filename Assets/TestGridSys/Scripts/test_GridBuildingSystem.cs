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

    public test_PlacebleObjectSCO test_PlacebleObjectSO;
    public GameObject[] buildings;
    public int index = 0;

    private int[] directions = { 0, 45, 90, 135, 180, 225, 270, 315 };
    [Range(0,7)]
    public int directionindex;
    public int directionValue;
    // Start is called before the first frame update

    void Rotate()
    {
        if (Input.GetKeyDown(KeyCode.R))
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
    void Start()
    {
        test_GridXYZ = new test_GridXYZ(x, y, z, gridCellSize, gridPosition);
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Alpha1))
        {
            index = 0;
        }
        if (Input.GetKey(KeyCode.Alpha2))
        {
            index = 1;
        }
        if (Input.GetKey(KeyCode.Alpha3))
        {
            index = 2;
        }
        if (Input.GetKey(KeyCode.Alpha4))
        {
            index = 3;
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

        if (Input.GetMouseButtonDown(0))
        {
            test_GridXYZ.GetGridIndexAtWorldPosition(CM_Testing.GetMousePos3D(), out int x, out int y, out int z);
            //test_GridXYZ.GetGridXYZOctagon(CM_Testing.GetMousePos3D(), out int x, out int y, out int z);
            Debug.Log(string.Format("pozisyonun x y z deðerleri {0},{1},{2}", x, y, z));
            if (x >= 0)
            {
                test_BaseGrid test_BaseGrid = test_GridXYZ.GetGridObject(x, y, z);
                if (test_BaseGrid.CanBuild())
                {
                    GameObject cloneBuilding = Instantiate(buildings[index], test_GridXYZ.GetWorldPositionGrid(x, y, z), Quaternion.Euler(0,directionValue,0));
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

        Rotate();
    }

    float timer = 0;
    test_BaseGrid GetMousePosGrid()
    {
        timer += Time.deltaTime;
        test_BaseGrid test_BaseGrid = default;
        if (timer >= 0.1f)
        {
            test_GridXYZ.GetGridIndexAtWorldPosition(CM_Testing.GetMousePos3D(), out int x, out int y, out int z);
            test_BaseGrid = test_GridXYZ.GetGridObject(x, y, z);
            Debug.Log("Grid Index = " + x + " " + y + " " + z);
            timer = 0;
        }

        return test_BaseGrid;
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
