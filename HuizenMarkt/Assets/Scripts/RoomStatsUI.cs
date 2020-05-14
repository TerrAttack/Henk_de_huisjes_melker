using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RoomStatsUI : MonoBehaviour
{
    [Header("RoomStats UI")]
    [SerializeField] public GameObject RoomStats = null;

    [Header("Dependencies")]
    [SerializeField] public RoomManager roomManager = null;

    [Header("Room Info")]
    [SerializeField] public GameObject InfoOverlay = null;
    [SerializeField] public GameObject InfoOverlayBackground = null;
    [SerializeField] public TextMeshProUGUI Rent = null;

    private BoxCollider2D InfoOverlayCollider;

    

    public int RoomPrize { get; set; }

    private void Start()
    {
        InfoOverlayCollider = InfoOverlayBackground.GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        RoomSelected();
    }

    public void RoomSelected()
    {
        if (roomManager.SelectedRoom == null)
            RoomStats.SetActive(false);
        else
        {
            RoomStats.SetActive(true);
        }
    }

    public void UpgradeButtonClicked()
    {

    }

	#region Info Screen

	public void InfoButtonClicked()
    {
        if (roomManager.SelectedRoom == null)
            return;
        InfoOverlay.SetActive(!InfoOverlay.activeSelf);
        GetRoomInfo();
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
                Rent.text = _num.ToString();
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
                Rent.text = _text;
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

    public void GetRoomInfo()
    {
        SetRoomInfo("rent", roomManager.SelectedRoom.rent);
    }
	#endregion
}
