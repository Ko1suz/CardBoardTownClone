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

    // Start is called before the first frame update
    void Start()
    {
        test_GridXYZ = new test_GridXYZ(x, y, z, gridCellSize, gridPosition);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //test_GridXYZ.GetGridXYZOctagon(CM_Testing.GetMousePos3D(), out int x, out int y, out int z);
            //Debug.Log(string.Format("pozisyonun x y z deðerleri {0},{1},{2}", x, y, z));
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
