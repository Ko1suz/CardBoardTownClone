using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI_Stats : MonoBehaviour
{
    GameManager gameManager = GameManager.GetGameManagerInstance;
    [SerializeField] TextMeshProUGUI lifeSupportMatText;  // yaþam destek
    [SerializeField] TextMeshProUGUI structureMatText;    // Yapý
    [SerializeField] TextMeshProUGUI energyMatText;       // enerji
    [SerializeField] TextMeshProUGUI conductiveMatText;   // iletken
    [SerializeField] TextMeshProUGUI habitability; 
    [SerializeField] TextMeshProUGUI researchPoint; 
    [SerializeField] TextMeshProUGUI matCapacity;


    private void Start()
    {
        gameManager = GameManager.GetGameManagerInstance;
    }


    private void FixedUpdate()
    {
        UpdateStats();
    }
    void UpdateStats()
    {
        lifeSupportMatText.text = gameManager.lifeSupportMat + "";
        structureMatText.text = gameManager.structureMat + "";
        energyMatText.text = gameManager.energyMat + "";
        conductiveMatText.text = gameManager.conductiveMat + "";
        habitability.text = gameManager.habitability + "/" + 4;
        researchPoint.text = gameManager.researchPoint + "";
        matCapacity.text = gameManager.matCapacity + "";
    }
}
