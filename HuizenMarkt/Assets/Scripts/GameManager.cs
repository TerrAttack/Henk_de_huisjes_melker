using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GameManager : MonoBehaviour
{
    [SerializeField] RoomManager roomManager = null;
    [SerializeField] HouseManager houseManager = null;
    [SerializeField] RoomStatsUI roomStatsUI = null;


    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !IsMouseOverUIWithIgnores())
        {
            roomManager.ClickCheck();
            houseManager.ClickCheck();
            roomStatsUI.CheckInfoUIClick();
        }
    }

    private bool IsMouseOverUI()
    {
        return EventSystem.current.IsPointerOverGameObject();
    }

    private bool IsMouseOverUIWithIgnores()
    {
        PointerEventData pointerEventData = new PointerEventData(EventSystem.current);
        pointerEventData.position = Input.mousePosition;

        List<RaycastResult> raycastResultsList = new List<RaycastResult>();
        EventSystem.current.RaycastAll(pointerEventData, raycastResultsList);
        for (int i = 0; i < raycastResultsList.Count; i++)
        {
            if(raycastResultsList[i].gameObject.GetComponent<MouseUIClickthrough>() != null)
            {
                raycastResultsList.RemoveAt(i);
                i--;
            }
        }

        return raycastResultsList.Count > 0;
    }
}
