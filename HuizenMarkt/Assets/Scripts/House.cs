using UnityEngine;
using TMPro;

public class House : MonoBehaviour
{
	#region Enums
	public enum HouseState
    {
        Unlocked,
        Selected,
        Locked,
    };
	#endregion

	#region Variables
	[HideInInspector] public int id;
    [HideInInspector] public HouseManager houseManager;

    [Header("House Sprites")]
    [SerializeField] Sprite houseSprite;


    [Header("Costs")]
    [SerializeField] public int houseCost = 100;

    [Header("House")]
    [SerializeField] public HouseState houseState = HouseState.Locked;

    [Header("Overlay Objects")]
    [SerializeField] GameObject overlay;

    [Header("Overlay Sprites")]
    [SerializeField] public Sprite LockedSprite;
    [SerializeField] public Sprite HighLightSprite;

    SpriteRenderer overlaySpriteRenderer;
    SpriteRenderer spriteRenderer;

    BoxCollider2D boxCollider2D;
	#endregion

	#region Methodes
	private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        boxCollider2D = GetComponent<BoxCollider2D>();
        overlaySpriteRenderer = overlay.GetComponent<SpriteRenderer>();
        overlaySpriteRenderer.sprite = LockedSprite;
        SwitchState(houseState);
    }

    public void SwitchState(HouseState _HouseState)
    {
        switch (_HouseState)
        {
            case HouseState.Locked:
                overlay.SetActive(true);
                overlaySpriteRenderer.sprite = LockedSprite;
                spriteRenderer.color = new Color(255, 255, 255, 255);
                break;

            case HouseState.Unlocked:
                overlay.SetActive(false);
                spriteRenderer.color = new Color(255, 255, 255, 0);
                break;

            case HouseState.Selected:
                overlay.SetActive(true);
                overlaySpriteRenderer.sprite = HighLightSprite;
                break;

            default:
                break;
        }
        houseState = _HouseState;
    }

    public bool CheckForClick()
    {
        if (Input.GetMouseButtonDown(0) && houseState != 0)
        {
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if (boxCollider2D.OverlapPoint(worldPosition))
                return true;
            return false;
        }
        return false;
    }

	#endregion
}