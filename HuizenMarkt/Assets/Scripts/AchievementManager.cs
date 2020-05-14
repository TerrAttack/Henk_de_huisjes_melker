using System.Collections;
using System.Collections.Generic;
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
    private static AchievementManager instance;

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
        CreateAchievement("General Area", "Press W", "Press W to achieve this!", 420, 0);
        CreateAchievement("General Area", "Press S", "Press S to achieve this!", 69, 0);

        AchievementsMenu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            EarnAchievement("Press W");
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            EarnAchievement("Press S");
        }
    }

    public void EarnAchievement(string title)
    {
        if (achievements[title].EarnAchievement())
        {

            GameObject notification = (GameObject)Instantiate(Notification);
            SetAchievementInfo("Notifications", notification, title);
            StartCoroutine(DestroyNotification(notification));
        }
    }

    IEnumerator DestroyNotification(GameObject notification)
    {
        yield return new WaitForSeconds(3);
        Destroy(notification);
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
