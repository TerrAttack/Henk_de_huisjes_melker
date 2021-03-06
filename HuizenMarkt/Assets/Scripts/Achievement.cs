﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Achievement
{
    private string title;
    private string description;
    public bool unlocked;
    private int points;
    private int spriteIndex;
    private GameObject achievementRef;

    public string Title { get => title; set => title = value; }
    public string Description { get => description; set => description = value; }
    public int Points { get => points; set => points = value; }
    public int SpriteIndex { get => spriteIndex; set => spriteIndex = value; }
    public GameObject AchievementRef { get => achievementRef; set => achievementRef = value; }

    public Achievement(string title, string description, int points, int spriteIndex, GameObject achievementRef)
    {
        this.Title = title;
        this.Description = description;
        this.unlocked = false;
        this.Points = points;
        this.SpriteIndex = spriteIndex;
        this.AchievementRef = achievementRef;

        LoadAchievement();
    }
    public bool EarnAchievement()
    {

        if (!unlocked)
        {
            SaveAchievement(true);
            achievementRef.GetComponent<Image>().sprite = AchievementManager.Instance.UnlockedSprite;
            AchievementManager.Instance.moneyScript.money += points;
            AchievementManager.Instance.moneyScript.totalEarnedMoney += points;
            return true;
        }
        return false;
    }

    public void SaveAchievement(bool val)
    {
        unlocked = val;
        PlayerPrefs.SetInt(title, val ? 1 : 0);
        PlayerPrefs.Save();
    }

    public void LoadAchievement()
    {
        unlocked = PlayerPrefs.GetInt(title) == 1 ? true : false;

        if (unlocked)
        {
            achievementRef.GetComponent<Image>().sprite = AchievementManager.Instance.UnlockedSprite;
        }
    }
}
