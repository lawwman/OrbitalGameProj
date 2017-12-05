using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FoodManager : MonoBehaviour {

    public GameObject foodMenu, RoastPitWMeat, BoilBtn, DrinkBtn, CookBtn, EatBtn;
    public Text FoodCount, FoodButtonDisplay;

    private int rawFood, cookedFood, rawExpiryDay, cookedExpiryDay, foodLimit;
    private Queue<RawMeat> RawFood;
    private Queue<CookedMeat> CookedFood;
    private UIManager uimanager;
    private GameManager gamemanager;
    private DiaryGenerator diarygenerator;
    private Upgrade upgrade;

    private void Start()
    {
        rawFood = cookedFood = 0;
        rawExpiryDay = 2;
        cookedExpiryDay = 10;
        foodLimit = 5;
        foodMenu.SetActive(false);
        RawFood = new Queue<RawMeat>();
        CookedFood = new Queue<CookedMeat>();
        uimanager = GameObject.Find("UIManager").GetComponent("UIManager") as UIManager;
        gamemanager = GameObject.Find("GameManager").GetComponent("GameManager") as GameManager;
        upgrade = GameObject.Find("Player").GetComponent("Upgrade") as Upgrade;
        diarygenerator = GameObject.Find("UIManager").GetComponent("DiaryGenerator") as DiaryGenerator;
        FoodButtonDisplay.text = "Food: " + (rawFood + cookedFood) + " / " + foodLimit.ToString();
    }
    public void OpenMenu()
    {
        foodMenu.SetActive(true);
        BoilBtn.SetActive(false);
        DrinkBtn.SetActive(false);
        CookBtn.SetActive(true);
        EatBtn.SetActive(true);
        FoodCount.text = "Raw Meat: " + RawFood.Count.ToString() + " Cooked Meat: " + CookedFood.Count.ToString();
    }
    public void CloseMenu() {
        foodMenu.SetActive(false);
        gamemanager.UnPauseGame();
    }
    public void EatFood()
    {
        if (CookedFood.Count != 0)
        {
            CookedFood.Dequeue();
            cookedFood -= 1;
            gamemanager.Decrement(-20, 0, 0, 0);
            if (CookedFood.Count == 0 && GameObject.Find("Roast Pit with Meat(Clone)"))
                Destroy(GameObject.Find("Roast Pit with Meat(Clone)"));
            FoodCount.text = "Raw Meat: " + RawFood.Count.ToString() + " Cooked Meat: " + CookedFood.Count.ToString();
            FoodButtonDisplay.text = "Food: " + (rawFood + cookedFood) + " / " + foodLimit.ToString();
        }
    }
    public int FeedFood()
    {
        if (RawFood.Count != 0)
        {
            RawFood.Dequeue();
            rawFood -= 1;
            FoodButtonDisplay.text = "Food: " + (rawFood + cookedFood) + " / " + foodLimit.ToString();
            return 1;
        }
        else return 0;
    }
    public void CookFood()
    {
        if(GameObject.Find("Fire(Clone)") && RawFood.Count != 0)
        {
            int foodToCook = 0;
            if (upgrade.FireLevel == 0)
                foodToCook = 1;
            else if (upgrade.FireLevel == 1)
                foodToCook = 3;
            for (int i = 0; i < foodToCook; i++)
            {
                if (RawFood.Count == 0)
                    break;
                else
                {
                    RawFood.Dequeue();
                    rawFood -= 1;
                    CookedFood.Enqueue(new CookedMeat(uimanager.GetDay()));
                    cookedFood += 1;
                }
            }
            if (foodToCook != 0 && upgrade.FireLevel == 1)
                Instantiate(RoastPitWMeat, new Vector2(1, -1.49f), Quaternion.identity);
            if (uimanager.GetAction() == 1)
            {
                foodMenu.SetActive(false);
                gamemanager.UnPauseGame();
            }
            uimanager.TakeAction();
            FoodCount.text = "Raw Meat: " + RawFood.Count.ToString() + " Cooked Meat: " + CookedFood.Count.ToString();
        }

    }
    public void addFood(int count)
    {
        int diff = foodLimit - (RawFood.Count + CookedFood.Count);
        if (count > diff) count = diff;
        rawFood += count;
        //update gamemanager stat
        gamemanager.numFood += count;
        FoodButtonDisplay.text = "Food: " + (rawFood + cookedFood) + " / " + foodLimit.ToString();
        for (int i = 0; i < count; i++)
        {
            RawFood.Enqueue(new RawMeat(uimanager.GetDay()));
        }
    }
    public void CheckFood()
    {
        int currentDay = uimanager.GetDay();
        int foodDate;
        int rawExpired, cookedExpired;
        rawExpired = cookedExpired = 0;
        if (RawFood.Count != 0)
        {
            foodDate = RawFood.Peek().dayAdded;
            while (currentDay - foodDate >= rawExpiryDay)
            {
                RawFood.Dequeue();
                rawFood -= 1;
                rawExpired += 1;
                if (RawFood.Count == 0)
                    break;
                foodDate = RawFood.Peek().dayAdded;
            }
        }
        if (CookedFood.Count != 0)
        {
            foodDate = CookedFood.Peek().dayAdded;
            while (currentDay - foodDate >= cookedExpiryDay)
            {
                CookedFood.Dequeue();
                cookedFood -= 1;
                cookedExpired += 1;
                if (CookedFood.Count == 0)
                    break;
                foodDate = CookedFood.Peek().dayAdded;
            }
        }
        diarygenerator.UpdateFood(rawExpired, cookedExpired);
        FoodButtonDisplay.text = "Food: " + (rawFood + cookedFood) + " / " + foodLimit.ToString();
    }
    public void IncrementFoodLimit(int num)
    {
        foodLimit += num;
        FoodButtonDisplay.text = "Food: " + (rawFood + cookedFood) + " / " + foodLimit.ToString();
    }
    public void LoseAllFood()
    {
        rawFood = cookedFood = 0;
        while (RawFood.Count != 0) RawFood.Dequeue();
        while (CookedFood.Count != 0) CookedFood.Dequeue();
    }
    //return true if food has expired
    private class RawMeat
    {
        public int dayAdded; //the day the meat was added
        public RawMeat(int dayAdded) { this.dayAdded = dayAdded; }
        
    }
    //return true if food has expired
    private class CookedMeat
    {
        public int dayAdded;
        public CookedMeat(int dayAdded) { this.dayAdded = dayAdded; }
    }
}
