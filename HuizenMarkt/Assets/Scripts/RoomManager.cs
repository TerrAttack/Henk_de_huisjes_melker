using UnityEngine;

public class RoomManager : MonoBehaviour
{
	#region Variables
	private Room[] rooms;
    private Room selectedRoom;
    private bool roomIsSelected = false;
	#endregion

	private void Start()
    {
        rooms = new Room[transform.childCount];

        for(int i = 0; i < transform.childCount; i++)
        {
            GameObject t = transform.GetChild(i).gameObject;

            rooms[i] = t.GetComponent<Room>();
            rooms[i].Id = i;
            rooms[i].RoomManager = this;
        }
    }
    
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            UnSelect();
    }
    public void SetSelected(int id)
    {
        if(!roomIsSelected)
        {
            SelectRoom(id);
            roomIsSelected = true;
        }
        else
        {
            rooms[selectedRoom.Id].SpriteRenderer.color = Color.white;
            SelectRoom(id);
        }
    }

    private void SelectRoom(int id)
    {
        if(!rooms[id].Unlocked)
            rooms[id].Unlocked = true;
        rooms[id].SetColor();
        selectedRoom = rooms[id];
    }

    private void UnSelect()
    {
        roomIsSelected = false;
        if(selectedRoom != null) selectedRoom.SpriteRenderer.color = Color.white;
        selectedRoom = null;
    }

    public Room[] Rooms
    {
        get { return rooms; }
    }
    public Room SelectedRoom
    {
        get { return selectedRoom; }
    }
    public bool RoomIsSelected
    {
        get { return roomIsSelected; }
    }
}