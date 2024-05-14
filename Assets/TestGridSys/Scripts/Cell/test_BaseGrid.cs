using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test_BaseGrid 
{
    protected int x, y, z;
    protected Transform placedObject;

    public test_BaseGrid(int x, int y, int z) 
    {
        this.x = x;
        this.y = y;
        this.z = z;
    }
}
