using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaterManager : MonoBehaviour {

    public GameObject waterMenu, CookBtn, EatBtn, BoilBtn, DrinkBtn;
    public Text WaterCount, WaterButtonDisplay;

    private int UncleanWater, BoiledWater, waterLimit;
    private UIManager uimanager;
    private GameManager gamemanager;
    private Upgrade upgrade;

    private void Start()
    {
        UncleanWater = BoiledWater = 0;
        waterLimit = 3;
        uimanager = GameObject.Find("UIManager").GetComponent("UIManager") as UIManager;
        gamemanager = GameObject.Find("GameManager").GetComponent("GameManager") as GameManager;
        upgrade = GameObject.Find("Player").GetComponent("Upgrade") as Upgrade;
        waterMenu.SetActive(false);
        WaterButtonDisplay.text = "Water: " + (UncleanWater + BoiledWater).ToString() + " / " + waterLimit.ToString();
    }
    public void OpenMenu() {
        waterMenu.SetActive(true);
        BoilBtn.SetActive(true);
        DrinkBtn.SetActive(true);
        CookBtn.SetActive(false);
        EatBtn.SetActive(false);
        gamemanager.PauseGame();
        WaterCount.text = "Unclean Water: " + UncleanWater.ToString() + "  " + "Boiled Water: " + BoiledWater.ToString();
    }
    public void closeMenu() {
        waterMenu.SetActive(false);
        gamemanager.UnPauseGame();
    }
    public void Drink()
    {
        if (BoiledWater != 0)
        {
            BoiledWater -= 1;
            gamemanager.hydration += 15;
            WaterButtonDisplay.text = "Water: " + (UncleanWater + BoiledWater).ToString() + " / " + waterLimit.ToString();
            WaterCount.text = "Unclean Water: " + UncleanWater.ToString() + "  " + "Boiled Water: " + BoiledWater.ToString();
        }
    }
    public void BoilWater()
    {
        if (UncleanWater != 0 && GameObject.Find("Fire(Clone)") && GameObject.Find("Vase 1(Clone)"))
        {
            if (uimanager.GetAction() == 1)
            {
                waterMenu.SetActive(false);
                gamemanager.UnPauseGame();
            }
            uimanager.TakeAction();
            int waterToBoil = 0;
            if (upgrade.VaseLevel == 0)
                waterToBoil = 1;
            else if (upgrade.VaseLevel == 1)
                waterToBoil = 3;
            for (int i = 0; i < waterToBoil; i++)
            {
                if (UncleanWater == 0)
                    break;
                UncleanWater -= 1;
                BoiledWater += 1;
            }
            WaterButtonDisplay.text = "Water: " + (UncleanWater + BoiledWater).ToString() + " / " + waterLimit.ToString();
            WaterCount.text = "Unclean Water: " + UncleanWater.ToString() + "  " + "Boiled Water: " + BoiledWater.ToString();
        }
    }
    public void addWater(int count)
    {
        //update gamemanager stats
        gamemanager.numWater += count;
        int diff = waterLimit - (UncleanWater + BoiledWater);
        if (count > diff) count = diff;
        UncleanWater += count;
        WaterButtonDisplay.text = "Water: " + (UncleanWater + BoiledWater).ToString() + " / " + waterLimit.ToString();
    }
    public void LoseAllWater()
    {
        UncleanWater = BoiledWater = 0;
        WaterButtonDisplay.text = "Water: " + (UncleanWater + BoiledWater).ToString() + " / " + waterLimit.ToString();
    }
}
