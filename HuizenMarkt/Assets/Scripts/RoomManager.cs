using UnityEngine;

public class RoomManager : MonoBehaviour
{
	public Room[] rooms;
	private bool roomGotSelected = false;

	[SerializeField] EconomyManagerScript moneyScript = null;
	[SerializeField] GameObject gameHandler = null;
	[SerializeField] AchievementManager achievementScript = null;
	private RoomStatsUI r;

	

	private void Start()
	{
		r = gameHandler.GetComponent<RoomStatsUI>();



		rooms = new Room[transform.childCount];

		for (int i = 0; i < transform.childCount; i++)
		{
			GameObject t = transform.GetChild(i).gameObject;

			rooms[i] = t.GetComponent<Room>();
			rooms[i].id = i;
			rooms[i].roomManager = this;
		}
	}

	private void Update()
	{
		if (SelectedRoom != null)
			r.SetRoomInfo("upgrade", SelectedRoom.upgradeCost);
	}

	public void ClickCheck()
	{
		roomGotSelected = false;

		foreach (Room room in rooms)
		{
			if (room.CheckForClick())
			{

				switch (room.roomState)
				{
					case Room.RoomState.Unlocked:
						SelectRoom(room);
						roomGotSelected = true;
						break;

					case Room.RoomState.Locked:
						if (moneyScript.money >= room.roomCost)
						{
							moneyScript.money -= room.roomCost;
							achievementScript.EarnAchievement("Buy a room");
							SelectRoom(room);
							roomGotSelected = true;
						}
						break;

					default:
						break;
				}
			}
		}
		if (!roomGotSelected)
			UnSelect();
	}

	public void UpgradeSelectedRoom()
	{
		if (SelectedRoom != null && moneyScript.money >= SelectedRoom.upgradeCost)
		{
			moneyScript.money -= SelectedRoom.upgradeCost;
			SelectedRoom.Upgrade();
		}
	}

	private void SelectRoom(Room _room)
	{
		if (SelectedRoom != null)
			SelectedRoom.SwitchState(Room.RoomState.Unlocked);
		_room.SwitchState(Room.RoomState.Selected);
		SelectedRoom = _room;
	}

	public void UnSelect()
	{
		if (SelectedRoom != null)
		{
			SelectedRoom.SwitchState(Room.RoomState.Unlocked);
			SelectedRoom = null;
		}
	}

	public Room SelectedRoom { get; private set; }
}