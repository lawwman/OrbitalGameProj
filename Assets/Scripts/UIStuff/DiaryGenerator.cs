using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class DiaryGenerator : MonoBehaviour {
    int rawExpired, cookedExpired;
    string[] hints = new string[10];
    string[] rambling = new string[10];

	void Start () {
        rawExpired = cookedExpired = 0;
        hints[0] = "Clay hardens over night. ";
        hints[1] = "The nights are cold without a good \r\ntent to sleep in. ";
        hints[2] = "Water is the source of life. ";
        hints[3] = "If I don't make a weapon by day 10... \r\nI doubt I would survive..";
        hints[4] = "Becareful what you put in your mouth...\r\n";
        hints[5] = "Somehow if I eat too much... my health improves. \r\n";
        hints[6] = "It is possible for fire to spread...some upgrades are/r/n needed.";
        /*
        hints[2] = "";
        hints[2] = "";
        hints[2] = "";
        hints[2] = "";
        hints[2] = "";
        hints[2] = "";
        hints[2] = "";
        */

        rambling[0] = "The jungle is a lawless environment...\r\n";
        rambling[1] = "If my life was a game...it would be a \r\npretty cool game. Then again what do \r\nI know?";
        rambling[2] = "Today is a nice day. Kinda. Not really...\r\n";
    }
    public string generateDiary()
    {
        string diaryEntry;
        diaryEntry = generateRambling();
        if (rawExpired != 0) diaryEntry += "\r\n" + rawExpired.ToString() + " units of food have expired.";
        if (cookedExpired != 0) diaryEntry += "\r\n" + cookedExpired.ToString() + " units of food have expired..";
        return diaryEntry;
    }
    private string generateRambling() { return rambling[Random.Range(0, 2)]; }
    public string generateHint() { return hints[Random.Range(0, 7)]; }
    public void UpdateFood(int raw, int cooked)
    {
        rawExpired = raw;
        cookedExpired = cooked;
    }

} 
