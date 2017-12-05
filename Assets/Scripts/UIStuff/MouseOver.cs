using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseOver : MonoBehaviour {

    private Player player;
    private string FireBtn, VaseBtn, TrapBtn, TentBtn, PipeBtn, BasketBtn, AxeBtn, SpearBtn, CutterBtn, upgradeVase, upgradeFire;
    private string upgradeVase2;
    private void Start()
    {
        player = GameObject.Find("Player").GetComponent("Player") as Player;
        FireBtn = "Camp Fire.\r\nRequires 2 Chopped Wood.";
        VaseBtn = "Clay Vase. \r\nCan be used to store water.\r\nRequires 2 Clay.";
        TrapBtn = "Trap. I can use this to catch \r\nanimals.\r\nRequires 2 Chopped Wood.";
        TentBtn = "Tent.\r\nRequires 8 Chopped Wood \r\nand 2 Bark Wood.";
        PipeBtn = "Pipe. I can use this to \r\ntransport water.\r\nRequires 2 Chopped Wood.";
        BasketBtn = "Basket. I can store more \r\nthings with this.\r\nRequires 2 Bark Wood.";
        AxeBtn = "Axe.\r\nRequires 2 Stone and 1 \r\nChopped Wood.";
        SpearBtn = "Spear.\r\nRequires 1 Stone and 2 \r\nChopped Wood.";
        CutterBtn = "Wood WorkShop. I can process\r\nmore wood. Requires 5 \r\nChoppedWood, upgraded Tent\r\nand an axe.";
        upgradeVase = "Upgrade vase. I need 3\r\n clay.";
        upgradeVase2 = "Upgrade vase. I need 5\r\n Chopped Wood.";
        upgradeFire = "Upgrade Fire. I need 5\r\n Chopped Wood.";
    }
    public void FireBtnThink() { player.Thinking(FireBtn, 1); }
    public void VaseBtnThink() { player.Thinking(VaseBtn, 1); }
    public void TrapBtnThink() { player.Thinking(TrapBtn, 1); }
    public void TentBtnThink() { player.Thinking(TentBtn, 1); }
    public void PipeBtnThink() { player.Thinking(PipeBtn, 1); }
    public void BasketBtnThink() { player.Thinking(BasketBtn, 1); }
    public void CutterBtnThink() { player.Thinking(CutterBtn, 1); }
    public void AxeBtnThink() { player.Thinking(AxeBtn, 1); }
    public void SpearBtnThink() { player.Thinking(SpearBtn, 1); }
    public void UpgradeVase()
    {
        if (GameObject.Find("Kettle(Clone)"))
        {
            player.Thinking(upgradeVase2, 1);
        }
        else player.Thinking(upgradeVase, 1);
    }
    public void UpgradeFire()
    {
        player.Thinking(upgradeFire, 1);
    }
}
