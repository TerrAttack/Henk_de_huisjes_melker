using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchievementToggle : MonoBehaviour
{
   public void ToggleUI()
    {
        //if(this.transform.gameObject.activeSelf)
        //{
        //    this.transform.gameObject.SetActive(false);
        //}
        //else if(!this.transform.gameObject.activeSelf)
        //{
        //    this.transform.gameObject.SetActive(true);
        //}
        this.transform.gameObject.SetActive(!this.transform.gameObject.activeSelf);
    }
}
