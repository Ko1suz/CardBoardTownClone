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


                CM_PlacedObject placedObject = CM_PlacedObject.Create(placedObjectWorldPosition, new Vector2Int(x,z),dir, cm_PlacedObjectTypeSO);
                

                foreach (Vector2Int gridPosition in gridPositionList)
                {
                    grid.GetGridObject(gridPosition.x, gridPosition.y).SetPlacedObject(placedObject);
                }
            }
            else 
            {
                UtilsClass.CreateWorldTextPopup("Cannot Build", grid.GetWorldPosition(x,z));
            }
        }

        if (Input.GetMouseButtonDown(1))
        {
            CM_GridObject gridObject =  grid.GetGridObject(CM_Testing.GetMousePos3D());
            CM_PlacedObject placedObject = gridObject.GetPlacedObjet();
            if (placedObject != null)
            {
                placedObject.DestroySelf();
                List<Vector2Int> gridPositionList = placedObject.GetGridPositionList();
                foreach (Vector2Int gridPosition in gridPositionList)
                {
                    grid.GetGridObject(gridPosition.x, gridPosition.y).ClearPlacedObjcet();
                }

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
        CM_PlacedObject placedObject;

        public void SetPlacedObject(CM_PlacedObject placedObject)
        {
            this.placedObject = placedObject;
            grid.TriggerGridObjectChanged(x,z);
        }

        public CM_PlacedObject GetPlacedObjet()
        {
            return placedObject;
        }

        public void ClearPlacedObjcet()
        {
            this.placedObject = null;
            grid.TriggerGridObjectChanged(x, z);
        }

        public bool CanBuild()
        {
            return placedObject == null;
        }
        public CM_GridObject(CM_GridXZ<CM_GridObject> grid, int x, int z)
        {
            this.grid = grid;
            this.x = x;
            this.z = z;
        }

        

        public override string ToString()
        {
            return x + "," + z + "\n" + placedObject;
        }
    }

    //public class Test_GridObject_2
    //{
    //    private CM_GridXZ<Test_GridObject_2> grid;
    //    private int x;
    //    private int z;
    //    CM_PlacedObject placedObject;

    //    public void SetPlacedObject(CM_PlacedObject placedObject)
    //    {
    //        this.placedObject = placedObject;
    //        grid.TriggerGridObjectChanged(x, z);
    //    }

    //    public CM_PlacedObject GetPlacedObjet()
    //    {
    //        return placedObject;
    //    }

    //    public void ClearPlacedObjcet()
    //    {
    //        this.placedObject = null;
    //        grid.TriggerGridObjectChanged(x, z);
    //    }

    //    public bool CanBuild()
    //    {
    //        return placedObject == null;
    //    }
    //    public Test_GridObject_2(CM_GridXZ<Test_GridObject_2> grid, int x, int z)
    //    {
    //        this.grid = grid;
    //        this.x = x;
    //        this.z = z;
    //    }



    //    public override string ToString()
    //    {
    //        return x + "," + z + "\n" + placedObject;
    //    }
    //}

}


