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
	[SerializeField] GameObject studentList = null;
	[SerializeField] TimeManager timeManager = null;
	public List<Student> students;
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

		students = new List<Student>();
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

			print("Profit: "+ currentProfit);
			if (currentProfit < (int)targetProfit)
			{
				//lose
				print("YOU LOSE");
				SceneManager.LoadScene(sceneName:"EndScreen");
			}
			targetProfit *= 1.25f;
		}
	}

	public void payCelery()
	{
		foreach (Student student in students)
		{
			student.currentMoney += student.income;
		}
	}

	public int CalculateExpectedProfit()
	{
		int expectedProfit = 0;
		foreach (Student student in students)
		{
			expectedProfit += student.appartment.rent;
		}
		foreach (Room room in roomManager.rooms)
		{

			if (room.roomState != Room.RoomState.Locked)
			{
				expectedProfit -= room.maintenanceCost;
			}
		}
		return expectedProfit;
	}

	public void getPaid()
    {
        int income = 0;
        foreach (Student student in students)
        {
            if (student.PayRent())
            {
                income += student.appartment.rent;
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
