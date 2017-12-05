using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WoodManager : MonoBehaviour {

    public GameObject MenuPage, ChopBtn, PeelBtn;
    public int TotalWood, ChoppedWood, BarkWood, woodLimit;
    //total amount is NOT the display amount. Refers to the Total Amount in the Chop Menu
    //wood display is the amount shown on the bottom right of the game interface
    public Text title, WoodDisplay, totalAmount, firstAmount, secondAmount;

    private UIManager uimanager;
    private GameManager gamemanager;
    private Achievement achievement;
    private bool haveCutter = false;
    private void Start()
    {
        MenuPage.SetActive(false);
        woodLimit = 12;
        WoodDisplay.text = "Wood: " + TotalWood.ToString() + " / " + woodLimit.ToString();
        uimanager = GameObject.Find("UIManager").GetComponent("UIManager") as UIManager;
        gamemanager = GameObject.Find("GameManager").GetComponent("GameManager") as GameManager;
        achievement = GameObject.Find("Achievement").GetComponent("Achievement") as Achievement;
        TotalWood = ChoppedWood = BarkWood = 0;
    }
    //make sure calling script checks if there is enough to deduct
    public void AddWood(int count)
    {
        //update gamemanager stats
        gamemanager.numWood += count;
        int diff = woodLimit - TotalWood;
        if (count > diff) TotalWood += diff;
        else TotalWood += count;
        //update achievement
        achievement.Achieve(4);
        WoodDisplay.text = "Wood: " + TotalWood.ToString() + " / " + woodLimit.ToString();
    }
    public void LoseChoppedWood(int amount)
    {
        ChoppedWood -= amount;
        TotalWood -= amount;
        WoodDisplay.text = "Wood: " + TotalWood.ToString() + " / " + woodLimit.ToString();
    }
    public void LoseBarkWood(int amount)
    {
        BarkWood -= amount;
        TotalWood -= amount;
        WoodDisplay.text = "Wood: " + TotalWood.ToString() + " / " + woodLimit.ToString();
    }
    public void openMenuPage()
    {
        MenuPage.SetActive(true);
        ChopBtn.SetActive(true);
        PeelBtn.SetActive(true);
        title.text = "Wood Cutting Page";
        totalAmount.text = "Amount of Unprocessed wood: " + (TotalWood - ChoppedWood - BarkWood).ToString();
        firstAmount.text = "Amount of ChoppedWood: " + ChoppedWood.ToString();
        secondAmount.text = "Amount of BarkWood: " + BarkWood.ToString();
    }
    public void ChopWood()
    {
        int woodLeft = TotalWood - ChoppedWood - BarkWood;
        if (woodLeft == 0)
            return;
        if (!haveCutter)
        {
            ChoppedWood += 1;
        }
        else
        {
            ChoppedWood += 3;
            TotalWood += 2;
            if (TotalWood > woodLimit) TotalWood = woodLeft;
            WoodDisplay.text = "Wood: " + TotalWood.ToString() + " / " + woodLimit.ToString();
        }
        firstAmount.text = "Amount of ChoppedWood: " + ChoppedWood.ToString();
        totalAmount.text = "Amount of Unprocessed wood: " + (TotalWood - ChoppedWood - BarkWood).ToString();
        if (uimanager.GetAction() == 1)
        {
            MenuPage.SetActive(false);
            gamemanager.UnPauseGame();
        }
        uimanager.TakeAction();
    }
    public void PeelWood()
    {
        int woodLeft = TotalWood - ChoppedWood - BarkWood;
        if (woodLeft == 0)
            return;
        if (!haveCutter)
        {
            BarkWood += 1;
        }
        else
        {
            BarkWood += 3;
            TotalWood += 2;
            if (TotalWood > woodLimit) TotalWood = woodLeft;
            WoodDisplay.text = "Wood: " + TotalWood.ToString() + " / " + woodLimit.ToString();
        }
        secondAmount.text = "Amount of BarkWood: " + BarkWood.ToString();
        totalAmount.text = "Amount of Unprocessed wood: " + (TotalWood - ChoppedWood - BarkWood).ToString();
        if (uimanager.GetAction() == 1)
        {
            MenuPage.SetActive(false);
            gamemanager.UnPauseGame();
        }
        uimanager.TakeAction();
    }
    public void closeMenuPage() { MenuPage.SetActive(false); }
    public void UpdateWoodLimit(int count)
    {
        woodLimit += count;
        WoodDisplay.text = "Wood: " + TotalWood.ToString() + " / " + woodLimit.ToString();
    }
    public void updateCutter()
    {
        haveCutter = true;
    }
}
