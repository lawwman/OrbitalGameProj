using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeFire : MonoBehaviour {
    private Upgrade upgrade;
    private Player player;
    private MouseOver mouseover;
    void Start () {
        upgrade = GameObject.Find("Player").GetComponent("Upgrade") as Upgrade;
        player = GameObject.Find("Player").GetComponent("Player") as Player;
        mouseover = GameObject.Find("UIManager").GetComponent("MouseOver") as MouseOver;
    }

    private void OnMouseDown()
    {
        upgrade.UpGradeFire();
        Destroy(GameObject.Find("UpgradeFireBtn(Clone)"));
        Destroy(GameObject.Find("UpgradeCircle(Clone)"));
    }
    private void OnMouseEnter()
    {
        mouseover.UpgradeFire();
    }
    private void OnMouseExit()
    {
        player.stopThinking();
    }
}
