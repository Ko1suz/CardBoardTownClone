using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test_GridXYZ 
{
    int x;
    int y;
    int z;

    test_SquareGrid[,,] gridObjcets = new test_SquareGrid[55, 55, 55];


    public test_GridXYZ()
    {
        for (int x = 0; x < gridObjcets.GetLength(0); x++)
        {
            for (int y = 0; y < gridObjcets.GetLength(1); y++)
            {
                for (int z = 0; z < gridObjcets.GetLength(2); z++)
                {
                    if (y <= 0)
                    {
                        gridObjcets[x, y, z] = new test_SquareGrid(x * 2, y * 2, z * 2 , true);
                    }
                    else
                    {
                        gridObjcets[x, y, z] = new test_SquareGrid(x * 2, y * 2, z * 2, false);
                    }

                }
            }
        }
    }
}
