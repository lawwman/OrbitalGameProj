using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectWater : MonoBehaviour {

    private GameManager gamemanager;
    private UIManager uimanager;
    private WaterManager watermanager;
    private Achievement achievement;

    private void Start()
    {
        gamemanager = GameObject.Find("GameManager").GetComponent("GameManager") as GameManager;
        uimanager = GameObject.Find("UIManager").GetComponent("UIManager") as UIManager;
        watermanager = GameObject.Find("InventoryManager").GetComponent("WaterManager") as WaterManager;
        achievement = GameObject.Find("Achievement").GetComponent("Achievement") as Achievement;
    }
    private void OnMouseDown()
    {
        gamemanager.UnPauseGame();
        watermanager.addWater(3);
        uimanager.TakeAction();
        achievement.Achieve(0);
        Destroy(GameObject.Find("UpgradeCircle(Clone)"));
        Destroy(GameObject.Find("Icons_0(Clone)"));
        Destroy(GameObject.Find("Upgrade(Clone)"));
    }
}
