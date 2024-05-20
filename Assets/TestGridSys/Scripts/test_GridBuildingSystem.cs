#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;


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


    public GameObject[] buildings;
    public int index = 0;
    // Start is called before the first frame update
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

        if (Input.GetMouseButtonDown(0))
        {
            CM_Testing.GetMousePos3D();
            //Debug.Log("Mouse Pos" + CM_Testing.GetMousePos3D());
            test_GridXYZ.GetGridIndexAtWorldPosition(CM_Testing.GetMousePos3D(), out int x, out int y, out int z);
            //test_GridXYZ.GetGridXYZOctagon(CM_Testing.GetMousePos3D(), out int x, out int y, out int z);
            Debug.Log(string.Format("pozisyonun x y z deðerleri {0},{1},{2}", x, y, z));

            if (x >= 0)
            {
                Instantiate(buildings[index], test_GridXYZ.GetWorldPositionGrid(x, y, z), Quaternion.identity);
            }
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
