using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CM_PlacedObject : MonoBehaviour
{
    CM_PlacedObjectTypeSO placedObjectTypeSO;
    Vector2Int origin;
    CM_PlacedObjectTypeSO.Dir dir;


    public static CM_PlacedObject Create(Vector3 worldPosition, Vector2Int origin, CM_PlacedObjectTypeSO.Dir dir, CM_PlacedObjectTypeSO placedObjectTypeSO)
    {
        Transform placedObjcetTransform = Instantiate(placedObjectTypeSO.prefab, worldPosition, Quaternion.Euler(0, placedObjectTypeSO.GetRotationAngle(dir),0));
        
        CM_PlacedObject placedObject = placedObjcetTransform.GetComponent<CM_PlacedObject>();

        placedObject.placedObjectTypeSO = placedObjectTypeSO;
        placedObject.origin = origin;
        placedObject.dir = dir;

        return placedObject;
    }
    public List<Vector2Int> GetGridPositionList()
    {
        return placedObjectTypeSO.GetGridPositionList(origin,dir);
    }
    public void DestroySelf()
    {
        Destroy(gameObject);
    }
}
