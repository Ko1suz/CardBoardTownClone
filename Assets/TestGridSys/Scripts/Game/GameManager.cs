using CodeMonkey.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public float lifeSupportMat;
    public float structureMat;
    public float energyMat;
    public float conductiveMat;
    public float matCapacity;

    public float researchPoint;

    public float settlersMorale;
    public float habitability;

    static GameManager instance;
    public static GameManager GetGameManagerInstance { get => instance; }
    
    [SerializeField] test_GridBuildingSystem GridBuildingSystemRef;
    public test_GridBuildingSystem GetGridBuildingSystem { get => GridBuildingSystemRef; }


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    private void Start()
    {
        GridBuildingSystemRef = FindObjectOfType<test_GridBuildingSystem>();
    }


    public bool CheckBuildingCost(test_PlacebleObjectSCO buildingRef, bool setResources)
    {
        int boolValue = 0;
        boolValue += (buildingRef.lifeSupportMat <= this.lifeSupportMat) ? 1 : 0;
        boolValue += (buildingRef.structureMat <= this.structureMat) ? 1 : 0;
        boolValue += (buildingRef.energyMat <= this.energyMat) ? 1 : 0;
        boolValue += (buildingRef.conductiveMat <= this.conductiveMat) ? 1 : 0;

        if (boolValue >= 4)
        {
            SetResources(buildingRef, setResources);
            return true;
        }
        else
        {
            // nedendir bilinmez debug true gelince çalýþmýyor fakat false deðeri gelirse çalýþýyor :D
            //if (setResources) { UtilsClass.CreateWorldTextPopup("Yeterli Kaynak Yok", CM_Testing.GetMousePos3D(), 5); } 
            return false;
        }
    }

    void SetResources(test_PlacebleObjectSCO buildingRef , bool setResources)
    {
        if (setResources)
        {
            lifeSupportMat -= buildingRef.lifeSupportMat;
            structureMat -= buildingRef.structureMat;
            energyMat -= buildingRef.energyMat;
            conductiveMat -= buildingRef.conductiveMat;
        }
    }

}
