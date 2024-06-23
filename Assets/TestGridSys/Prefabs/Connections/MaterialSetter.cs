using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialSetter : MonoBehaviour
{
    public Material objMat;


    public void SetObjMatBlue()
    {
        float green = 59;
        float red = 0;
        float blue = 65;
        Color blueEmmisonColor = new Color(red / 255f, green / 255f, blue / 255f);
        objMat.SetColor("_EmissionColor", blueEmmisonColor * Mathf.LinearToGammaSpace(5.8f));
    }

    public void SetObjMatRed()
    {
        objMat.SetColor("_EmissionColor", Color.red * Mathf.LinearToGammaSpace(5.8f));
    }
}
