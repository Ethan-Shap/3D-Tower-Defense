using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class UiOverlayTest : MonoBehaviour {

    private void Update()
    {
        if (Input.touchCount == 1)
            IsPointerOverUIObject();
    }

    private bool IsPointerOverUIObject()
    {
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = Input.mousePosition;
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);

        foreach (RaycastResult result in results)
        {
            Debug.Log(result.gameObject.name);
        }

        return results.Count > 0;
    }
}