using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RoomStatsUI : MonoBehaviour
{
    [Header("RoomStats UI")]
    [SerializeField] public GameObject RoomStats;

    [Header("Room Info")]
    [SerializeField] public GameObject InfoOverlay;
    [SerializeField] public GameObject InfoOverlayBackground;
    [SerializeField] public TextMeshProUGUI RentText;


    private BoxCollider2D InfoOverlayCollider;

    public int RoomPrize { get; set; }

    private void Start()
    {
        InfoOverlayCollider = InfoOverlayBackground.GetComponent<BoxCollider2D>();
    }

    public void UpgradeButtonClicked()
    {

    }

    public void InfoButtonClicked()
    {
        InfoOverlay.SetActive(!InfoOverlay.activeSelf);
    }

    public void DeactivateInfoOverlay()
    {
        InfoOverlay.SetActive(false);
    }

    public void SetRoomInfo(string _type, int _num)
    {
        switch(_type)
        {
            case "rent":
                RentText.text = _num.ToString();
                break;

            default:
                return;
        }
    }

    public void SetRoomInfo(string _type, string _text)
    {
        switch (_type)
        {
            case "rent":
                RentText.text = _text;
                break;

            default:
                return;
        }
    }

    public void CheckInfoUIClick()
    {
        if (!CheckForClick())
            DeactivateInfoOverlay();
    }

    public bool CheckForClick()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if (InfoOverlayCollider.OverlapPoint(worldPosition))
                return true;
            return false;
        }
        return false;
    }
}
