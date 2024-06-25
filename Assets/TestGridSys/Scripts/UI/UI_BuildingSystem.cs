using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_BuildingSystem : MonoBehaviour
{
    GameManager gameManager;
    public GameObject SlotPrefab;
    public test_PlacebleObjectSCO[] Buildings;
    public test_PlacebleObjectSCO[] Connections;

    public GameObject BuildingUI_GameObjcet;
    public GameObject ConnectionUI_GameObjcet;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameManager.GetGameManagerInstance;
        CreatePlacebleSlots(Buildings);
        CreatePlacebleSlots(Connections);
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public test_PlacebleObjectSCO ReturnBuildingType()
    {
        return null;
    }

    public void CreatePlacebleSlots(test_PlacebleObjectSCO[] buildingType)
    {
        for (int i = 0; i < buildingType.Length; i++)
        {
            GameObject SlotClone = Instantiate(SlotPrefab);

            if (buildingType == Buildings){ SlotClone.transform.SetParent(BuildingUI_GameObjcet.transform); }
            if (buildingType == Connections){ SlotClone.transform.SetParent(ConnectionUI_GameObjcet.transform); }
            
            SlotClone.transform.localScale = Vector3.one;
            SlotClone.GetComponent<Slot>().sco = buildingType[i];
            SlotClone.GetComponent<Slot>().SlotIcon = buildingType[i].uiImage;
        }
    }

    public void CloseAllUIs()
    {
        BuildingUI_GameObjcet.SetActive(false);
        ConnectionUI_GameObjcet.SetActive(false);
    }

    public void OpenBuildingSlots()
    {
        ConnectionUI_GameObjcet.SetActive(false);
        if (BuildingUI_GameObjcet.activeSelf) { BuildingUI_GameObjcet.SetActive(false); }
        else { BuildingUI_GameObjcet.SetActive(true); }
    }

    public void OpenConnectionSlots()
    {
        BuildingUI_GameObjcet.SetActive(false);
        if(ConnectionUI_GameObjcet.activeSelf) { ConnectionUI_GameObjcet.SetActive(false); }
        else { ConnectionUI_GameObjcet.SetActive(true); }
    }


}
