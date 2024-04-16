using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class CM_PlacedObjectTypeSO : ScriptableObject
{
    public static Dir GetNexDir(Dir dir)
    {
        return Dir.Down;
    }

    public enum Dir { Down, Left, Up, Right }

    public string nameString;
    public Transform prefab;
    public Transform visual;
    public int width;
    public int height;

    public int GetRotationAngle(Dir dir)
    {
        return 0;
    }

    public Vector2Int GetRotationOffset(Dir dir) 
    {
        return Vector2Int.zero;
    }

    public List<Vector2Int> GetGridPositionList(Vector2Int offset, Dir dir) 
    {
        List<Vector2Int> gridPositionList = new List<Vector2Int>();
        switch (dir)
        {
            case Dir.Down:
            case Dir.Up:
                for (int x = 0; x < width; x++)
                {
                    for (int y = 0; y < height; y++)
                    {
                        gridPositionList.Add(offset +  new Vector2Int(x,y));
                    }
                }
                break;
            case Dir.Left:
            case Dir.Right:
                for (int x = 0; x < height; x++)
                {
                    for (int y = 0; y < width; y++)
                    {
                        gridPositionList.Add(offset + new Vector2Int(x, y));
                    }
                }
                break;
            default:
                break;
        }

        return gridPositionList;
    }
}
