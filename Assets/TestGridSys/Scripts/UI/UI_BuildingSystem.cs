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
        CreateBuildingSlots();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public test_PlacebleObjectSCO ReturnBuildingType()
    {
        return null;
    }

    public void CreateBuildingSlots()
    {
        for (int i = 0; i < Buildings.Length; i++)
        {
            GameObject SlotClone = Instantiate(SlotPrefab);
            SlotClone.transform.SetParent(BuildingUI_GameObjcet.transform);
            SlotClone.transform.localScale = Vector3.one;
            SlotClone.GetComponent<Slot>().sco = Buildings[i];
            SlotClone.GetComponent<Slot>().SlotIcon = Buildings[i].uiImage;
        }
    }
}
