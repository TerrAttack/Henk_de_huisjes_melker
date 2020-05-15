using UnityEngine;
using TMPro;
using UnityEngine.UI;

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

    [Header("Student Info")]
    [SerializeField] public GameObject StudentWindow = null;
    [SerializeField] public GameObject StudentWindowBackground = null;
    [SerializeField] public GameObject StudentList = null;
    [SerializeField] public GameObject StudentPrefab = null;

    private BoxCollider2D InfoOverlayCollider;
    private BoxCollider2D UpgradeWindowCollider;
    private BoxCollider2D StudentWindowCollider;

    public int RoomPrize { get; set; }

    private void Start()
    {
        InfoOverlayCollider = InfoOverlayBackground.GetComponent<BoxCollider2D>();
        UpgradeWindowCollider = UpgradeWindowBackground.GetComponent<BoxCollider2D>();
        StudentWindowCollider = StudentWindowBackground.GetComponent<BoxCollider2D>();
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
        if (StudentWindow.activeSelf)
            if (!CheckForClickStudent())
                DeactivateStudentWindow();
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
        StudentWindow.SetActive(false);
        InfoOverlay.SetActive(false);
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

    #region Info Screen
    public void InfoButtonClicked()
    {
        if (roomManager.SelectedRoom == null)
            return;
        InfoOverlay.SetActive(!InfoOverlay.activeSelf);
        StudentWindow.SetActive(false);
        UpgradeWindow.SetActive(false);
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

    #region Student Window
    public void StudentButtonClicked()
    {
        if (roomManager.SelectedRoom == null)
            return;
        StudentWindow.SetActive(!StudentWindow.activeSelf);
        InfoOverlay.SetActive(false);
        UpgradeWindow.SetActive(false);
        GetRoomInfo();
    }
    public void DeactivateStudentWindow()
    {
        StudentWindow.SetActive(false);
    }
    public bool CheckForClickStudent()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if (StudentWindowCollider.OverlapPoint(worldPosition))
                return true;
            return false;
        }
        return false;
    }

    public void AddStudent()
    {
        int a = 0;
        for(int i = 0; i < StudentList.transform.childCount; i++)
        {
            var s = StudentList.transform.GetChild(i).gameObject;
            var studentScript = s.GetComponent<Student>();
            if (studentScript.appartment == roomManager.SelectedRoom)
                a++;
        }
        if(a < 2)
        {
            GameObject student = Instantiate(StudentPrefab, Vector3.zero, Quaternion.identity);
            var g = student.GetComponent<Student>();
            g.appartment = roomManager.SelectedRoom;
            student.transform.parent = StudentList.transform;
        }    
    }

    public void EvictStudent()
    {
        for(int i = 0; i < StudentList.transform.childCount; i++)
        {
            var s = StudentList.transform.GetChild(i).gameObject;
            var studentScript = s.GetComponent<Student>();
            if (studentScript.appartment == roomManager.SelectedRoom)
            {
                Destroy(s);
                return;
            }
                
        }
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
            case "student":
                //UpgradeCost.text = _num.ToString();
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
            case "student":
                //UpgradeCost.text = _text;
                break;

            default:
                return;
        }
    }
}
