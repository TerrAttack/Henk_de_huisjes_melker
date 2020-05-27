using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DisplayStudentUICard : MonoBehaviour
{
    public StudentUICard studentUICard;

    public TextMeshProUGUI studentNameUI;
    public TextMeshProUGUI moneyUI;
    public TextMeshProUGUI incomeUI;

    void Start()
    {
        UpdateContent();
    }

    void UpdateContent()
    {
        studentNameUI.text = studentUICard.studentName;
        moneyUI.text = studentUICard.money.ToString() + "$";
        incomeUI.text = studentUICard.income.ToString() + "$ p/m";
    }
}
