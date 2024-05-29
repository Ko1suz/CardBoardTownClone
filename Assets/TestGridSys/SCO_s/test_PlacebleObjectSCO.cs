using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class test_PlacebleObjectSCO : ScriptableObject
{
    public string nameString;
    public Transform prefab;
    public Transform visual;
    public int x_size;
    public int y_size;
    public int z_size;
    //public int[] directions = { 0, 45, 90, 135, 180, 225, 270, 315 };
    
}
