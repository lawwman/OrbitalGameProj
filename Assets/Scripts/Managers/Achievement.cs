using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Achievement : MonoBehaviour {

    public Text message;
    public GameObject achievePage;
    private int water = 0, tent = 0, trap = 0, wood = 0;
    private string achievementList;

	void Start () {
        achievePage.SetActive(false);
	}
    public void Achieve(int x)
    {

        if (x == 0)
        {
            water += 1;
            if (water == 1)
            {
                achievePage.SetActive(true);
                message.text = "Achievement Unlocked\r\n";
                message.text += "At least it ain’t pee - Find water source.";
                achievementList += "At least it ain’t pee - Find water source.\r\n";
            }
        }
        if (x == 1)
        {
            achievePage.SetActive(true);
            message.text = "Achievement Unlocked\r\n";
            message.text += "Men Vs Wild - Survived 3 days.";
            achievementList += "Men Vs Wild - Survived 3 days.\r\n";
        }
        if (x == 2)
        {
            tent += 1;
            if (tent == 1)
            {
                achievePage.SetActive(true);
                message.text = "Achievement Unlocked\r\n";
                message.text = "House Warming - Build a tent";
                achievementList += "House Warming - Build a tent\r\n";
            }
        }
        if (x == 3)
        {
            trap += 1;
            if (trap == 1)
            {
                achievePage.SetActive(true);
                message.text = "Achievement Unlocked\r\n";
                message.text = "Lunch is served - Used trap.";
                achievementList += "Lunch is served - Used trap.\r\n";
            }
        }
        if (x == 4)
        {
            wood += 3;
            if (wood == 30)
            {
                achievePage.SetActive(true);
                message.text = "Achievement Unlocked\r\n";
                message.text = "Deforestator - Cut 30 wood.";
                achievementList += "Deforestator - Cut 30 wood.\r\n";
            }
        }
    }
    public string generateAchievement()
    {
        return achievementList;
    }
    public void closePage()
    {
        achievePage.SetActive(false);
    }
}
