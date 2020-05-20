using System;
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
        CreateAchievement("General Area", "Earn a total of 10000 points", "Earn 10000 points to achieve this!", 69, 0);
        CreateAchievement("General Area", "Earn a total of 100000 points", "Earn 100000 points to achieve this!", 420, 0);
        CreateAchievement("General Area", "Buy a house", "Buy a house to achieve this!", 20, 0);
        CreateAchievement("General Area", "Buy a room", "Buy a room to achieve this!", 20, 0);
        CreateAchievement("General Area", "Put a student in a room", "Have a student to achieve this!", 20, 0);
        CreateAchievement("General Area", "Survive 1 month", "Survive 1 monthto achieve this!", 50, 0);
        CreateAchievement("General Area", "Survive 5 months", "Survive 5 months to achieve this!", 100, 0);
        CreateAchievement("General Area", "Survive 10 months", "Survive 10 months to achieve this!", 200, 0);

        AchievementsMenu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (moneyScript.totalEarnedMoney >= 10000)
        {
            EarnAchievement("Earn a total of 10000 points");
        }
        if (moneyScript.totalEarnedMoney >= 100000)
        {
            EarnAchievement("Earn a total of 100000 points");
        }
        if (moneyScript.students.Count() > 0)
        {
            EarnAchievement("Put a student in a room");
        }
    }

    public void EarnAchievement(string title)
    {
        print("Earning: " + title);
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
