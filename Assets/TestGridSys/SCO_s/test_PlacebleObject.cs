using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class test_PlacebleObject : ScriptableObject
{
    public string nameString;
    public Transform prefab;
    public Transform visual;
    public int x_size;
    public int y_size;
    public int z_size;
    public static int[] directions = { 0, 45, 90, 135, 180, 225, 270, 315 };
    public static int GetNexDir(int counter)
    {
        int direction  = directions[0];
        counter++;
        if (counter > 7)
        {
            counter = 0;
        }
        return direction;
    }

   
    //public int[] directionsForSquares = { 0, 90, 180, 270};
}
