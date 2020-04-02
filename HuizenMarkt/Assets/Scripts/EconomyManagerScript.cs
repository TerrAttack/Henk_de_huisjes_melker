﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EconomyManagerScript : MonoBehaviour
{
    [SerializeField] public int money;
    [SerializeField] TextMeshProUGUI moneyText = null;

    private void Update()
    {
        moneyText.text = money.ToString();
        if (Input.GetMouseButtonDown(0)) money++;
    }
}
