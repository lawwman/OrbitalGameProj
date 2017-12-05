using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeVase : MonoBehaviour {

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
        upgrade.upGradeVase();
        Destroy(GameObject.Find("UpgradeCircle(Clone)"));
        Destroy(GameObject.Find("Icons_0(Clone)"));
        Destroy(GameObject.Find("Upgrade(Clone)"));
    }
    private void OnMouseEnter()
    {
        mouseover.UpgradeVase();
    }
    private void OnMouseExit()
    {
        player.stopThinking();
    }
}
