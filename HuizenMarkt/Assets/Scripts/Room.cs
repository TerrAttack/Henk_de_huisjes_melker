using UnityEngine;

public class Room : MonoBehaviour
{
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

    public int id;
    public int level = 1;

    [SerializeField] public Sprite LockedSprite;
    [SerializeField] public Sprite HighLightSprite;

    [SerializeField] public int roomCost = 100;

    [SerializeField] public RoomType  roomType = RoomType.BedRoom;
    [SerializeField] public RoomState roomState = RoomState.Locked;

    [SerializeField] Sprite roomSprite;
    [SerializeField] GameObject overlay;

    SpriteRenderer overlaySpriteRenderer;
    SpriteRenderer spriteRenderer;
    BoxCollider2D boxCollider2D;

    public RoomManager roomManager;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        boxCollider2D = GetComponent<BoxCollider2D>();
        overlaySpriteRenderer = overlay.GetComponent<SpriteRenderer>();
        overlaySpriteRenderer.sprite = LockedSprite;

        SwitchState(roomState);
    }

    private void Update()
    {
        

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
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if (boxCollider2D.OverlapPoint(worldPosition))
                return true;
            return false;
        }
        return false;
    }
}