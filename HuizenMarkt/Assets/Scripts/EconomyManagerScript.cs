using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Data.SqlTypes;

public class EconomyManagerScript : MonoBehaviour
{
    [SerializeField] public int money;
    [SerializeField] TextMeshProUGUI moneyText = null;
    [SerializeField] RoomManager roomManager = null;
    [SerializeField] GameObject studentList = null;
    [SerializeField] TimeManager timeManager = null;
    public Student[] students;
    public IList<Student> list2;
    public int totalEarnedMoney;
    public int month = 1;
    public int lastMonth = 1;
    public int targetProfit = 10;
    public int currentProfit = 0;


    void Start()
    {
        month = timeManager.month;
        lastMonth = timeManager.month;
        moneyText.text = money.ToString();
        totalEarnedMoney = money;

        students = new Student[studentList.transform.childCount];

        for (int i = 0; i < studentList.transform.childCount; i++)
        {
            GameObject t = studentList.transform.GetChild(i).gameObject;

            students[i] = t.GetComponent<Student>();
        }
    }

    private void Update()
    {
        moneyText.text = money.ToString();

        lastMonth = month;
        month = timeManager.month;
        if (month > lastMonth)
        {
            currentProfit = 0;
            getPaid();
            payBills();

            if(currentProfit < targetProfit)
            {
                //lose
            }
        }
    }

    public void getPaid()
    {
        int income = 0;
        foreach (Student student in students)
        {
            if (student.appartment.roomState != Room.RoomState.Locked)
            {
                if (student.PayRent())
                {
                    income += student.appartment.rent;
                }
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
}
