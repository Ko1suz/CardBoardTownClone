using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    public test_PlacebleObjectSCO sco;
    RectTransform rectTransform;
    public GameObject InfoSlot;
    [SerializeField] bool buttonHower = false;

    public Sprite SlotIcon;
    public Image SlotImage;

    [HideInInspector]   public TextMeshProUGUI lifeSupportMatText;  // ya�am destek
    [HideInInspector]   public TextMeshProUGUI structureMatText;    // Yap�
    [HideInInspector]   public TextMeshProUGUI energyMatText;       // enerji
    [HideInInspector]   public TextMeshProUGUI conductiveMatText;   // iletken

    public GameObject lifeSupportMatObj;  // ya�am destek
    public GameObject structureMatObj;    // Yap�
    public GameObject energyMatObj;       // enerji
    public GameObject conductiveMatObj;   // iletken

    float timer = 0;
    private void Start()
    {
        rectTransform = InfoSlot.GetComponent<RectTransform>();  

        lifeSupportMatText = lifeSupportMatObj.GetComponentInChildren<TextMeshProUGUI>();
        structureMatText = structureMatObj.GetComponentInChildren<TextMeshProUGUI>();
        energyMatText = energyMatObj.GetComponentInChildren<TextMeshProUGUI>();
        conductiveMatText = conductiveMatObj.GetComponentInChildren<TextMeshProUGUI>();

        WriteCosts();
    }
    public void OnButtonHover()
    {
        buttonHower = true; 
        // Metodunuzun i�eri�ini buraya ekleyin
    }
    public void OnButtonHoverExit()
    {
        buttonHower = false;
        // Fare butonun �zerinden �ekildi�inde yap�lacaklar
    }

    private void Update()
    {
        LerpSlot();
    }

    public void WriteCosts()
    {
        if(sco.lifeSupportMat > 0) { lifeSupportMatObj.SetActive(true); }
        if(sco.structureMat > 0) { structureMatObj.SetActive(true); }
        if(sco.energyMat > 0) { energyMatObj.SetActive(true); }
        if(sco.conductiveMat > 0) { conductiveMatObj.SetActive(true); }

        lifeSupportMatText.text = sco.lifeSupportMat.ToString();
        structureMatText.text = sco.structureMat.ToString();
        energyMatText.text = sco.energyMat.ToString();
        conductiveMatText.text = sco.conductiveMat.ToString();

        SlotImage.sprite = SlotIcon;
    }

    public void ReturnSco()
    {
        GameManager.GetGameManagerInstance.GetGridBuildingSystem.selectedSco = sco;
        Destroy(GameManager.GetGameManagerInstance.GetGridBuildingSystem.visualClone);
        GameManager.GetGameManagerInstance.GetGridBuildingSystem.visualClone = null;
        GameManager.GetGameManagerInstance.GetGridBuildingSystem.CheckSelectedGridChange(true);
    }

    void LerpSlot()
    {
        if (buttonHower)
        {
            rectTransform.anchoredPosition = Vector2.Lerp(rectTransform.anchoredPosition, new Vector2(0, 200), 0.1f);
        }
        else
        {
            rectTransform.anchoredPosition = Vector2.Lerp(rectTransform.anchoredPosition, new Vector2(0, 0), 0.1f);
        }
    }
}
