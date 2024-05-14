using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test_BaseGrid 
{
    protected int x, y, z;
    protected Transform placedObject;
    protected bool showDebug;

    public test_BaseGrid(int x, int y, int z, bool showDebug = false) 
    {
        this.x = x;
        this.y = y;
        this.z = z;
        this.showDebug = showDebug;
    }
}
