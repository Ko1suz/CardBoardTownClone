using CodeMonkey.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CM_GridBuildingSystem : MonoBehaviour
{
    [SerializeField] public List<CM_PlacedObjectTypeSO> cm_PlacedObjectTypeSOList;
    CM_PlacedObjectTypeSO cm_PlacedObjectTypeSO;

    CM_GridXZ<CM_GridObject> grid;
    private CM_PlacedObjectTypeSO.Dir dir = CM_PlacedObjectTypeSO.Dir.Down;

    private void Awake()
    {
        int gridWidth = 10;
        int gridHeight = 10;
        float cellSize = 10;

        grid = new CM_GridXZ<CM_GridObject> (gridWidth, gridHeight, cellSize, Vector3.zero, (CM_GridXZ<CM_GridObject> g, int x, int z) => new CM_GridObject(g,x,z));

        cm_PlacedObjectTypeSO = cm_PlacedObjectTypeSOList[0];
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("Amerikaya");
            grid.GetXZ(CM_Testing.GetMousePos3D(), out int x, out int z);

            List<Vector2Int> gridPositionList = cm_PlacedObjectTypeSO.GetGridPositionList(new Vector2Int(x,z), dir);

            //Test Can Build
            bool canBuild = true;
            foreach (Vector2Int gridPosition in gridPositionList)
            {
                if (!grid.GetGridObject(gridPosition.x, gridPosition.y).CanBuild()) 
                {
                    //Cannot build Here 
                    canBuild = false;
                    break;
                }
            }

            CM_GridObject gridObject =  grid.GetGridObject(x,z);
            if (canBuild) 
            {
                Vector2Int rotatinOffset = cm_PlacedObjectTypeSO.GetRotationOffset(dir);
                Vector3 placedObjectWorldPosition = grid.GetWorldPosition(x,z) + new Vector3(rotatinOffset.x, 0, rotatinOffset.y) * grid.GetCellSize();
                Transform buildTransform =
                    Instantiate(cm_PlacedObjectTypeSO.prefab,
                    placedObjectWorldPosition,
                    Quaternion.Euler(0, cm_PlacedObjectTypeSO.GetRotationAngle(dir),0)
                    ); ;

                foreach (Vector2Int gridPosition in gridPositionList)
                {
                    grid.GetGridObject(gridPosition.x, gridPosition.y).SetTransform(buildTransform);
                }
            }
            else 
            {
                UtilsClass.CreateWorldTextPopup("Cannot Build", grid.GetWorldPosition(x,z));
            }
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            dir = CM_PlacedObjectTypeSO.GetNexDir(dir);
            UtilsClass.CreateWorldTextPopup("" + dir, CM_Testing.GetMousePos3D(), 2);
        }

        if (Input.GetKeyDown(KeyCode.Alpha1)){ cm_PlacedObjectTypeSO = cm_PlacedObjectTypeSOList[0]; }
        if (Input.GetKeyDown(KeyCode.Alpha2)){ cm_PlacedObjectTypeSO = cm_PlacedObjectTypeSOList[1]; }
        if (Input.GetKeyDown(KeyCode.Alpha3)){ cm_PlacedObjectTypeSO = cm_PlacedObjectTypeSOList[2]; }
    }

    public class CM_GridObject
    {
        private CM_GridXZ<CM_GridObject> grid;
        private int x;
        private int z;
        Transform gridObjcetHolder;
        public Transform SetGridObjcetHolder { get => gridObjcetHolder; set => gridObjcetHolder = value; }

        public void SetTransform(Transform transform)
        {
            gridObjcetHolder = transform;
            grid.TriggerGridObjectChanged(x,z);
        }

        public void ClearTransform(Transform transform)
        {
            gridObjcetHolder = null;
        }

        public bool CanBuild()
        {
            return gridObjcetHolder == null;
        }
        public CM_GridObject(CM_GridXZ<CM_GridObject> grid, int x, int z)
        {
            this.grid = grid;
            this.x = x;
            this.z = z;
        }

        

        public override string ToString()
        {
            return x + "," + z + "\n" + gridObjcetHolder;
        }
    }

}


