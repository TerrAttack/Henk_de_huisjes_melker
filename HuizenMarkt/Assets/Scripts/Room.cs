using UnityEngine;

public class Room : MonoBehaviour
{
    #region Variables
    private SpriteRenderer spriterRenderer;
    private BoxCollider2D boxCollider2D;
    private RoomManager roomManager;
    private int id;

    private bool isUnlocked = false;

    [SerializeField]
    public Color c;
    #endregion

    enum RoomState
    {
        Locked,
        Unlocked,
        Selected,
    };

    void Start()
    {
        spriterRenderer = GetComponent<SpriteRenderer>();
        boxCollider2D = GetComponent<BoxCollider2D>();
    }
    private void Update()
    {
        CheckForClick();
        if(!Unlocked)
        {
            spriterRenderer.color = Color.gray;
        }
    }

    private void CheckForClick()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if (boxCollider2D.OverlapPoint(worldPosition))
                RoomPressed();
        }
    }
    public void SetColor()
    {
        spriterRenderer.color = c;
    }
    public void RoomPressed()
    {
        roomManager.SetSelected(id);
    }

    public RoomManager RoomManager
    {
        get { return roomManager; }
        set { roomManager = value; }
    }
    public SpriteRenderer SpriteRenderer
    {
        get { return spriterRenderer; }
        set { spriterRenderer = value; }
    }
    public BoxCollider2D BoxCollider2D
    {
        get { return boxCollider2D; }
        set { boxCollider2D = value; }
    }
    public int Id
    {
        get { return id; }
        set { id = value; }
    }
    public bool Unlocked
    {
        get { return isUnlocked; }
        set { isUnlocked = value; }
    }
}