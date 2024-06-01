using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu()]
public class test_PlacebleObjectSCO : ScriptableObject
{
    public string nameString;
    public Image uiImage;
    public Transform prefab;
    public Transform visual;
    public int x_size;
    public int y_size;
    public int z_size;

    public bool isSquare = false;
    public Vector3 minPlaceIndexs;
    public Vector3 maxPlaceIndexs = new Vector3(99,99,99);
    //public int[] directions = { 0, 45, 90, 135, 180, 225, 270, 315 };
    
}
