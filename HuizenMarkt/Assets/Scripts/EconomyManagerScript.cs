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
    public int totalMoney;
    public int month = 1;
    public int lastMonth = 1;


    void Start()
    {
        month = timeManager.month;
        lastMonth = timeManager.month;
        moneyText.text = money.ToString();
        totalMoney = money;

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
            getPaid();
            payBills();
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
        totalMoney += income;
    }
    public void payBills()
    {
        int costs = 0;
        foreach (Room room in roomManager.rooms)
        {
            
            if (room.roomState == 0)
            {
                costs += 20;
            }
        }
        money -= costs;
    }
}
