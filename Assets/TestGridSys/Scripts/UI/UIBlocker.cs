using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIBlocker : MonoBehaviour
{
    // Bu metod UI'ya týklanýp týklanmadýðýný kontrol eder
    private bool IsPointerOverUIObject()
    {
        // Þu anda üzerine týklanan UI öðelerinin listesini al
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
                // UI'ya týklanmadýysa, sahnedeki týklama iþlemlerini gerçekleþtir
                OnSceneClick();
            }
        }
    }

    private void OnSceneClick()
    {
        // Burada sahnedeki týklama iþlemlerini gerçekleþtirin
        Debug.Log("Sahneye týklandý!");
    }
}
