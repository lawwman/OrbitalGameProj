using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nap : MonoBehaviour {

    private GameManager gamemanager;
    private UIManager uimanager;
    private Player player;

    private void Start()
    {
        gamemanager = GameObject.Find("GameManager").GetComponent("GameManager") as GameManager;
        uimanager = GameObject.Find("UIManager").GetComponent("UIManager") as UIManager;
        player = GameObject.Find("Player").GetComponent("Player") as Player;
    }
    private void OnMouseDown()
    {
        gamemanager.Decrement(0, -10, -10, 0);
        player.Thinking("That was a good nap", 1);
        uimanager.TakeAction();
        Destroy(GameObject.Find("UpgradeCircle(Clone)"));
        Destroy(GameObject.Find("Icons_1(Clone)"));
        Destroy(GameObject.Find("UpgradeTentBtn(Clone)"));
    }
}
