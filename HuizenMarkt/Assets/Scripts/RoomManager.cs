using UnityEngine;

public class RoomManager : MonoBehaviour
{
	public Room[] rooms;
	private Room selectedRoom;
	private bool roomGotSelected = false;

	[SerializeField] EconomyManagerScript moneyScript;

	private void Start()
	{
		rooms = new Room[transform.childCount];

		for (int i = 0; i < transform.childCount; i++)
		{
			GameObject t = transform.GetChild(i).gameObject;

			rooms[i] = t.GetComponent<Room>();
			rooms[i].id = i;
			rooms[i].roomManager = this;
		}
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
		if (selectedRoom != null && moneyScript.money >= selectedRoom.upgradeCost)
		{
			moneyScript.money -= selectedRoom.upgradeCost;
			selectedRoom.Upgrade();
		}
	}

	private void SelectRoom(Room _room)
	{
		if (selectedRoom != null)
			selectedRoom.SwitchState(Room.RoomState.Unlocked);
		_room.SwitchState(Room.RoomState.Selected);
		selectedRoom = _room;
	}

	public void UnSelect()
	{
		if (selectedRoom != null)
		{
			selectedRoom.SwitchState(Room.RoomState.Unlocked);
			selectedRoom = null;
		}
	}

	public Room SelectedRoom
	{
		get { return selectedRoom; }
	}
}