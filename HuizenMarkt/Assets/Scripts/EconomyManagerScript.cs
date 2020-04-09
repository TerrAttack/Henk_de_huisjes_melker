using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EconomyManagerScript : MonoBehaviour
{
    [SerializeField] public int money;
    [SerializeField] TextMeshProUGUI moneyText = null;

    private float timer;

    private void Update()
    {
        moneyText.text = money.ToString();
        //TODO every time timer is above certain threshold
        //Get room info and their student stats, add money accordingly
    }
}
