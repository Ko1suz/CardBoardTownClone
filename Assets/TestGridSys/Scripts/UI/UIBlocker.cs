using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIBlocker : MonoBehaviour
{
    // Bu metod UI'ya t�klan�p t�klanmad���n� kontrol eder
    private bool IsPointerOverUIObject()
    {
        // �u anda �zerine t�klanan UI ��elerinin listesini al
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);

        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        return results.Count > 0;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (!IsPointerOverUIObject())
            {
                // UI'ya t�klanmad�ysa, sahnedeki t�klama i�lemlerini ger�ekle�tir
                OnSceneClick();
            }
        }
    }

    private void OnSceneClick()
    {
        // Burada sahnedeki t�klama i�lemlerini ger�ekle�tirin
        Debug.Log("Sahneye t�kland�!");
    }
}
