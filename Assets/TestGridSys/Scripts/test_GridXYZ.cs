using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test_GridXYZ 
{
    int x;
    int y;
    int z;

    test_BaseGrid[,,] gridObjcets = new test_BaseGrid[11, 11, 11];


    public test_GridXYZ()
    {
        int counter = 0;
        if (x % 2 != 0)
        {
            counter++;
        }

        for (int y = 0; y < gridObjcets.GetLength(1); y++)
        {
            for (int z = 0; z < gridObjcets.GetLength(2); z++)
            {
                for (int x = 0; x < gridObjcets.GetLength(0); x++)
                {
                    if (counter % 2 == 0)
                    {
                        if (y <= 0)
                        {
                            gridObjcets[x, y, z] = new test_OctagonGrid(x, y, (z*2), true);
                        }
                        else
                        {
                            gridObjcets[x, y, z] = new test_OctagonGrid(x, y, z, false);
                        }
                    }
                    else
                    {
                        if (y <= 0)
                        {
                            gridObjcets[x, y, z] = new test_SquareGrid(x, y, (z*2)+1, true);
                        }
                        else
                        {
                            gridObjcets[x, y, z] = new test_SquareGrid(x, y, z + 1, false);
                        }
                    }
                   
                    counter++;
                }
            }
        }
    }
}
