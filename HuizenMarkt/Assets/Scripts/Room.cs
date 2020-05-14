using UnityEngine;
using TMPro;

public class Room : MonoBehaviour
{
	#region Enums
	public enum RoomState
    {
        Unlocked,
        Selected,
        Locked,
    };

    public enum RoomType
    {
        BedRoom,
        LivingRoom,
        Kitchen,
        BathRoom,
        Enterence,
        Stairs,
    };
    #endregion

    #region Variables
	[HideInInspector] public int id;
    [HideInInspector] public int level = 1;
    [HideInInspector] public RoomManager roomManager;

    [Header("Room Sprites")]
    [SerializeField] Sprite roomSprite;


    [Header("Costs")]
    [SerializeField] public int roomCost = 100;
    [SerializeField] public int upgradeCost = 100;

    [Header("Room")]
    [SerializeField] public RoomType  roomType = RoomType.BedRoom;
    [SerializeField] public RoomState roomState = RoomState.Locked;
    [SerializeField] public House inHouse;

    [Header("Overlay Objects")]
    [SerializeField] GameObject overlay = null;

    [Header("Overlay Sprites")]
    [SerializeField] public Sprite LockedSprite;
    [SerializeField] public Sprite HighLightSprite;

    SpriteRenderer overlaySpriteRenderer;
    SpriteRenderer spriteRenderer;

    BoxCollider2D boxCollider2D;

    [Header("Student")]
    [SerializeField] public int rent = 100;

    #endregion

    #region Methodes
    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        boxCollider2D = GetComponent<BoxCollider2D>();
        overlaySpriteRenderer = overlay.GetComponent<SpriteRenderer>();
        overlaySpriteRenderer.sprite = LockedSprite;
        SetType(roomType);
        SwitchState(roomState);
    }

    public void SetType(RoomType _roomType)
    {
        switch(_roomType)
        {
            case RoomType.BathRoom:
                spriteRenderer.color = new Color(255, 0, 0, 255);
                break;
            case RoomType.BedRoom:
                spriteRenderer.color = new Color(255, 255, 255, 255);
                break;
            case RoomType.Enterence:
                spriteRenderer.color = new Color(255, 255, 0, 255);
                break;
            case RoomType.Kitchen:
                spriteRenderer.color = new Color(0, 255, 0, 255);
                break;
            case RoomType.LivingRoom:
                spriteRenderer.color = new Color(0, 255, 255, 255);
                break;
            case RoomType.Stairs:
                spriteRenderer.color = new Color(0, 0, 255, 255);
                break;
        }
    }

    public void SwitchState(RoomState _roomState)
    {
        switch (_roomState)
        {
            case RoomState.Locked:
                overlay.SetActive(true);
                overlaySpriteRenderer.sprite = LockedSprite;
                break;

            case RoomState.Unlocked:
                overlay.SetActive(false);
                break;

            case RoomState.Selected:
                overlay.SetActive(true);
                overlaySpriteRenderer.sprite = HighLightSprite;
                break;

            default:
                break;
        }
        roomState = _roomState;
    }

    public bool CheckForClick()
    {
        if (Input.GetMouseButtonDown(0) && inHouse.houseState == 0)
        {
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if (boxCollider2D.OverlapPoint(worldPosition))
                return true;
            return false;
        }
        return false;
    }

    public void Upgrade()
    {
        upgradeCost += upgradeCost / 2;
        level++;
    }
	#endregion

}