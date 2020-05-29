using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class TimeManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI dayText = null;
    [SerializeField] TextMeshProUGUI monthText = null;
    [SerializeField] AchievementManager achievementScript = null;
    [SerializeField] public int timeScale = 3;
    private float timer = 0;
    public int month = 1;
    public float day = 0;

    // Start is called before the first frame update
    void Start()
    {
        dayText.text = "Day: "+((int)day).ToString();
        monthText.text = "Month: " + month.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        dayText.text = "Day: " + ((int)day).ToString();
        monthText.text = "Month: " + month.ToString();
        timer += Time.deltaTime * timeScale;
        day += Time.deltaTime * timeScale;

        if (timer > 30)
        {
            month++;
            day = 1;
            timer = 0;
            if (month == 2)
            {
                achievementScript.EarnAchievement("Still learning");
            }
            else if (month == 6)
            {
                achievementScript.EarnAchievement("Getting better");
            }
            else if (month == 11)
            {
                achievementScript.EarnAchievement("YOU'RE STILL PLAYING?!?!?");
            }
        }
    }
}
