using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UpgradeUI : MonoBehaviour
{
    [SerializeField] public RoomManager roomManager     = null;
    [SerializeField] public GameObject  upgradeWindow   = null;
    [SerializeField] public TextMeshProUGUI price;
    [SerializeField] public Vector2 offset = Vector2.zero;

    void Update()
    {
        if(roomManager.SelectedRoom != null)
        {
            upgradeWindow.SetActive(true);
            price.text = roomManager.SelectedRoom.upgradeCost.ToString();
            upgradeWindow.transform.position = roomManager.SelectedRoom.transform.position+ new Vector3(offset.x, offset.y, 0);
        }
        else
        {
            upgradeWindow.SetActive(false);
        }
    }
}
