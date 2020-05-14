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

    [Header("Upgrade Info")]
    [SerializeField] public GameObject UpgradeWindow = null;
    [SerializeField] public GameObject UpgradeWindowBackground = null;
    [SerializeField] public TextMeshProUGUI UpgradeCost = null;

    private BoxCollider2D InfoOverlayCollider;
    private BoxCollider2D UpgradeWindowCollider;

    public int RoomPrize { get; set; }

    private void Start()
    {
        InfoOverlayCollider = InfoOverlayBackground.GetComponent<BoxCollider2D>();
        UpgradeWindowCollider = UpgradeWindowBackground.GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        RoomSelected();
    }

    public void CheckUIClick()
    {
        if(InfoOverlay.activeSelf)
            if (!CheckForClickInfo())
                DeactivateInfoOverlay();
        if (UpgradeWindow.activeSelf)
            if (!CheckForClickUpgrade())
                DeactivateUpgradeOverlay();
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

	#region Upgrade Window
	public void UpgradeButtonClicked()
    {
        if (roomManager.SelectedRoom == null)
            return;
        UpgradeWindow.SetActive(!UpgradeWindow.activeSelf);
    }

    public void DeactivateUpgradeOverlay()
    {
        UpgradeWindow.SetActive(false);
    }

    public bool CheckForClickUpgrade()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if (UpgradeWindowCollider.OverlapPoint(worldPosition))
                return true;
            return false;
        }
        return false;
    }
	#endregion

	public void SetRoomInfo(string _type, int _num)
    {
        switch (_type)
        {
            case "rent":
                Rent.text = _num.ToString();
                break;
            case "upgrade":
                UpgradeCost.text = _num.ToString();
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
            case "upgrade":
                UpgradeCost.text = _text.ToString();
                break;

            default:
                return;
        }
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

    public bool CheckForClickInfo()
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
