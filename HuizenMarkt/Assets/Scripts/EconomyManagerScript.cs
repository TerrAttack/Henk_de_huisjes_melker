using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.Data.SqlTypes;

public class EconomyManagerScript : MonoBehaviour
{
	[SerializeField] public int money;
	[SerializeField] TextMeshProUGUI moneyText = null;
	[SerializeField] TextMeshProUGUI profitText = null;
	[SerializeField] TextMeshProUGUI expectedProfitText = null;
	[SerializeField] RoomManager roomManager = null;
	[SerializeField] HouseManager houseManager = null;
	[SerializeField] public GameObject studentList = null;
	[SerializeField] TimeManager timeManager = null;
	public int totalEarnedMoney;
	public int month = 1;
	public int lastMonth = 1;
    public float targetProfit;
    public int currentProfit = 0;
	void Start()
	{
		month = timeManager.month;
		lastMonth = timeManager.month;
		moneyText.text = money.ToString();
		profitText.text = "Target profit: " + ((int)targetProfit).ToString();
		totalEarnedMoney = money;
	}

	private void Update()
	{
		moneyText.text = money.ToString();
		profitText.text = "Target profit: " + ((int)targetProfit).ToString();
		expectedProfitText.text = "Expected profit: " + CalculateExpectedProfit().ToString(); 
		ChangeOverlay();
		lastMonth = month;
		month = timeManager.month;
		if (month > lastMonth)
		{
			currentProfit = 0;
			payCelery();
			getPaid();
			payBills();

			if (currentProfit < (int)targetProfit || money < 0)
			{
				SceneManager.LoadScene(sceneName:"EndScreen");
			}
			targetProfit *= 1.25f;
		}
	}

	public void payCelery()
	{
		foreach (Transform t in studentList.transform)
		{
			t.GetComponent<Student>().PayRent();
		}
	}

	public int CalculateExpectedProfit()
	{
		int _expectedProfit = 0;
		
		foreach (Transform t in studentList.transform)
		{
			_expectedProfit += t.GetComponent<Student>().appartment.rent;
		}
		foreach (Room room in roomManager.rooms)
		{

			if (room.roomState != Room.RoomState.Locked)
			{
				_expectedProfit -= room.maintenanceCost;
			}
		}
		
		return _expectedProfit;
	}

	public void getPaid()
    {
		foreach (Transform t in studentList.transform)
		{
			t.GetComponent<Student>().currentMoney += t.GetComponent<Student>().income;
		}
		int income = 0;
        foreach (Transform t in studentList.transform)
        {
			var g = t.GetComponent<Student>();
			if (g.currentMoney - g.appartment.rent >= 0)
            {
                income += g.appartment.rent;
            }
        }
        money += income;
        totalEarnedMoney += income;
        currentProfit += income;
    }

    public void payBills()
    {
        int costs = 0;
        foreach (Room room in roomManager.rooms)
        {
            
            if (room.roomState != Room.RoomState.Locked)
            {
                costs += room.maintenanceCost;
            }
        }
        money -= costs;
        currentProfit -= costs;
    }

	public void ChangeOverlay()
	{
		foreach(Room room in roomManager.rooms)
		{
			if(room.roomCost > money && room.roomState == Room.RoomState.Locked)
			{
				room.overlaySpriteRenderer.color = Color.red;
			}
			else if(room.roomCost < money && room.roomState == Room.RoomState.Locked)
			{
				room.overlaySpriteRenderer.color = Color.white;
			}
		}

		foreach (House house in houseManager.houses)
		{
			if (house.houseCost > money && house.houseState == House.HouseState.Locked)
			{
				house.overlaySpriteRenderer.color = Color.red;
			}
			else if (house.houseCost < money && house.houseState == House.HouseState.Locked)
			{
				house.overlaySpriteRenderer.color = Color.white;
			}
		}
	}
}
