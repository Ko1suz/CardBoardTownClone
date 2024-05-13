using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test_GridObjcet : test_GlobalVisualRender
{
    private int x;
    private int y;
    private int z;

    Transform placedObject;


    public test_GridObjcet(int x, int y, int z) 
    {
        this.x = x;
        this.y = y;
        this.z = z;
    }
}
