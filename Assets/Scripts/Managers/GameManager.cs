using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    public GameObject deadPage, boar;
    public Text deadText;
    public bool Paused;
    public int health, satiety, hydration, warmth;
    public int numDays, numWood, numWater, numFood, pigsKilled;

    public Image HP, Hydra, Sat, Warm;
    private int MaxHealth,MaxSatiety,MaxWarmth,MaxHydration;

    void Start () {
        deadPage.SetActive(false);
        Paused = false;
        health = warmth = satiety = MaxHealth = MaxSatiety = MaxWarmth = 50;
        numDays = numWood = numWater = numFood = pigsKilled = 0;
        hydration = 31;
        MaxHydration = 65;
    }
    private void Update()
    {
        HandleBar();
    }
    private void HandleBar()
    {
        HP.fillAmount = Map(health, MaxHealth);
        Hydra.fillAmount = Map(hydration, MaxHydration);
        Sat.fillAmount = Map(satiety, MaxSatiety);
        Warm.fillAmount = Map(warmth, MaxWarmth);
    }
    public void PauseGame()
    {
        Paused = true;
    }
    public void UnPauseGame()
    {
        Paused = false;
    }
    //methods to access player's stats
    public void Decrement(int i, int j, int k, int l)
    {
        satiety -= i;
        warmth -= j;
        health -= k;
        hydration -= l;
        // to prevent stats from going beyond max value
        if (hydration > MaxHydration) hydration = MaxHydration;
        if (satiety > MaxSatiety)
        {
            int diff = satiety - MaxSatiety;
            satiety = MaxSatiety;
            health += diff;
        }
        if (health > MaxHealth) health = MaxHealth;
        if (warmth > MaxWarmth) warmth = MaxWarmth;
    }
    public void CheckIfDead()
    {
        if (hydration <= 0 || warmth <= 0 || health <= 0 || satiety <= 0)
        {
            deadPage.SetActive(true);
            if (hydration <= 0) deadText.text = "You take your last breathe as your throat finally dries up...\r\nYou died from thirst.\r\n\r\n";
            else if (warmth <= 0) deadText.text = "Your cold body remains motionless...\r\nYou died from hypothermia.\r\n\r\n";
            else if (health <= 0) deadText.text = "Your wounded corpse lies within the harsh lands of the jungle....\r\nYou died from severe damage to your health.\r\n\r\n";
            else deadText.text = "The last few bits of strength you can muster has left you...\r\n You died from starvation.\r\n\r\n";
            deadText.text += "Number of days survived: " + numDays.ToString() + "\r\n\r\n";
            deadText.text += "Number of wood chopped: " + numWood.ToString() + "\r\n\r\n";
            deadText.text += "Amount of water collected: " + numWater.ToString() + "\r\n\r\n";
            deadText.text += "Amount of Food collected: " + numFood.ToString() + "\r\n\r\n";
            deadText.text += "Amount of Pigs killed: " + pigsKilled.ToString() + "\r\n\r\n";
        }
    }

    private float Map(float value, float max) { return value / max; }
}
