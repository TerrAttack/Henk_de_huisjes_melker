using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DisplayStudentUICard : MonoBehaviour
{
    public Student student;

    public TextMeshProUGUI studentNameUI;
    public TextMeshProUGUI moneyUI;
    public TextMeshProUGUI incomeUI;

    public StudentCardHandler handler;

    void Start()
    {
        UpdateContent();
    }

    private void Update()
    {
        UpdateContent();
    }

    public void UpdateContent()
    {
        studentNameUI.text = student.studentName;
        moneyUI.text = student.currentMoney.ToString() + "$";
        incomeUI.text = student.income.ToString() + "$ p/m";
    }
}
