﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class AchievementManager : MonoBehaviour
{
    public GameObject Achievement;
    public GameObject Notification;
    public Sprite[] Sprites;
    public GameObject AchievementsMenu;
    Dictionary<string, Achievement> achievements = new Dictionary<string, Achievement>();
    public Sprite UnlockedSprite;
    public EconomyManagerScript moneyScript;
    public TimeManager timeManager;
    private static AchievementManager instance;
    private int fadeTime = 2;

    public static AchievementManager Instance {
        get
        {
            if(instance == null)
            {
                instance = GameObject.FindObjectOfType<AchievementManager>();
            }
            return instance;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();
        CreateAchievement("General Area", "First step", "Buy a house to achieve this!", 5, 0);
        CreateAchievement("General Area", "Second step", "Buy a room to achieve this!", 10, 0);
        CreateAchievement("General Area", "Third step", "Have a student to achieve this!", 20, 0);
        CreateAchievement("General Area", "Still learning", "Survive 1 month to achieve this!", 50, 0);
        CreateAchievement("General Area", "Getting better", "Survive 5 months to achieve this!", 100, 0);
        CreateAchievement("General Area", "YOU'RE STILL PLAYING?!?!?", "Survive 10 months to achieve this!", 200, 0);
        CreateAchievement("General Area", "Let's get this bread", "Earn 10000 points to achieve this!", 69, 0);
        CreateAchievement("General Area", "That's a lot of bread", "Earn 100000 points to achieve this!", 420, 0);

        AchievementsMenu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (moneyScript.totalEarnedMoney >= 10000)
        {
            EarnAchievement("Let's get this bread");
        }
        if (moneyScript.totalEarnedMoney >= 100000)
        {
            EarnAchievement("That's a lot of bread");
        }
        if (moneyScript.students.Count() > 0)
        {
            EarnAchievement("Third step");
        }
    }

    public void EarnAchievement(string title)
    {
        if (achievements[title].EarnAchievement())
        {
            GameObject notification = (GameObject)Instantiate(Notification);
            SetAchievementInfo("Notifications", notification, title);
            StartCoroutine(FadeAchievement(notification));
        }
    }

    //IEnumerator DestroyNotification(GameObject notification)
    //{
    //    yield return new WaitForSeconds(3);
    //    Destroy(notification);
    //}

    IEnumerator FadeAchievement(GameObject obj)
    {
        CanvasGroup group = obj.GetComponent<CanvasGroup>();
        float rate = 1.0f / fadeTime;
        int startAlpha = 0;
        int endAlpha = 1;
        
        for (int i = 0; i < 2; i++)
        {
            float progress = 0.0f;
            while (progress < 1.0)
            {
                group.alpha = Mathf.Lerp(startAlpha, endAlpha, progress);
                progress += rate * Time.deltaTime;
                yield return null;
            }

            yield return new WaitForSeconds(fadeTime);
            startAlpha = 1;
            endAlpha = 0;
        }

        Destroy(obj);
    }


    public void CreateAchievement(string category, string title, string description, int points, int spriteIndex)
    {
        GameObject achievement = (GameObject)Instantiate(Achievement);
        Achievement achievementObj = new Achievement(title, description, points, spriteIndex, achievement);
        achievements.Add(title, achievementObj);
        SetAchievementInfo(category, achievement, title);
    }
    public void SetAchievementInfo(string category, GameObject achievement, string title)
    {
        achievement.transform.SetParent(GameObject.Find(category).transform);
        achievement.transform.localScale = Vector3.one;
        achievement.transform.GetChild(0).GetComponent<Image>().sprite = Sprites[achievements[title].SpriteIndex];
        achievement.transform.GetChild(1).GetChild(0).GetComponent<Text>().text = achievements[title].Points.ToString();
        achievement.transform.GetChild(2).GetComponent<Text>().text = title;
        achievement.transform.GetChild(3).GetComponent<Text>().text = achievements[title].Description;
    }
}
