using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Build : MonoBehaviour {
    public Inventory inventory;
    public FoodManager foodmanager;
    public WoodManager woodmanager;
    public UIManager uimanager;
    public Achievement achievement;
    public TerrainGenerator TG;
    public GameObject fire, vase, trap, tent, pipe1, pipe2, basket, cutter;

    private bool makeFire, makeVase, makeTrap, makeTent, makeWoodenSpear, makePipe, makeBasket;
    private bool makeAxe, makeCutter;
    private float pipeLocation = 4, basketLocation = -6;
    private Dam dam;

    private Player player;
    private Upgrade upgrade;

    private void Start()
    {
        player = GetComponent<Player>();
        upgrade = GetComponent<Upgrade>();
        makeFire = makeVase = makeTrap = makeTent = makePipe = makeBasket = makeAxe = makeCutter = makeWoodenSpear = false;
        dam = GameObject.Find("Dam").GetComponent("Dam") as Dam;
    }
    public void MakeFire() { makeFire = true; }
    public void MakeVase() { makeVase = true;}
    public void MakeTrap() { makeTrap = true; }
    public void MakeTent() { makeTent = true; }
    public void MakePipe() { makePipe = true; }
    public void MakeBasket() { makeBasket = true; }
    public void MakeAxe() { makeAxe = true; }
    public void MakeCutter() { makeCutter = true; }
    public void MakeWoodedSpear() { makeWoodenSpear = true; }

    public void buildStuff()
    {
        if (makeFire)
        {
            if (woodmanager.ChoppedWood <= 1 || GameObject.Find("Fire(Clone)")) //create speech bubble, I already have a fire
            {
                player.idle = true;
                makeFire = false;
                return;
            }
            Instantiate(fire, new Vector2(0.5f, -2f), Quaternion.identity);
            woodmanager.LoseChoppedWood(2);
            makeFire = false;
            uimanager.TakeAction();
        }
        if (makeVase)
        {
            if (inventory.clayCount <= 1 || GameObject.Find("Vase 1(Clone)")) //create speech bubble
            {
                player.Thinking("Can't build this..", 0);
                player.idle = true;
                makeVase = false;
                return;
            }
            Instantiate(vase, new Vector2(2.5f, -2.05f), Quaternion.identity);
            inventory.AddClay(-2);
            makeVase = false;
            player.Thinking("This can be used to \r\n collect water!", 0);
            uimanager.TakeAction();
        }
        if (makeTrap)
        {
            if (woodmanager.ChoppedWood <= 1 || GameObject.Find("Trap(Clone)")) //create speech bubble
            {
                player.Thinking("Can't build this..", 0);
                player.idle = true;
                makeTrap = false;
                return;
            }
            Instantiate(trap, new Vector2(-3f, -1.53f), Quaternion.identity);
            woodmanager.LoseChoppedWood(2);
            makeTrap = false;
            uimanager.TakeAction();
        }
        if (makeTent)
        {
            if (woodmanager.ChoppedWood < 8 || woodmanager.BarkWood < 2 || GameObject.Find("Tent(Clone)"))
            {
                player.Thinking("Can't build this..", 0);
                player.idle = true;
                makeTent = false;
                return;
            }
            Instantiate(tent, new Vector2(-5, -1f), Quaternion.identity);
            achievement.Achieve(2);
            woodmanager.LoseChoppedWood(8);
            woodmanager.LoseBarkWood(2);
            foodmanager.IncrementFoodLimit(3);
            inventory.UpdateClayLimit(3);
            inventory.UpdateStoneLimit(2);
            player.Thinking("I can store more things now!", 0);
            makeTent = false;
            woodmanager.UpdateWoodLimit(4);
            uimanager.TakeAction();
        }
        if (makeBasket)
        {
            if (woodmanager.BarkWood <2 || (TG.LeftSideTree.Peek()) > basketLocation)
            {
                player.Thinking("Can't build this..", 0);
                player.idle = true;
                makeBasket = false;
                return;
            }
            else
            {
                Instantiate(basket, new Vector2(basketLocation, -2), Quaternion.identity);
                basketLocation -= 0.6f;
                woodmanager.LoseBarkWood(2);
                inventory.UpdateClayLimit(2);
                inventory.UpdateStoneLimit(2);
                player.Thinking("I can store more things now!", 0);
                makeBasket = false;
                uimanager.TakeAction();
            } 
        }
        if (makePipe && pipeLocation < 20)
        {
            if (woodmanager.ChoppedWood < 1 || TG.RightSideTree.Peek() < pipeLocation)
            {
                player.Thinking("Can't build this..", 0);
                player.idle = true;
                makePipe = false;
                return;
            }
            //only build last pipe when dam is broken and pipes long enough
            if (pipeLocation == 18f && dam.broken)
            {
                Instantiate(pipe1, new Vector2(pipeLocation, -1), Quaternion.identity);
                pipeLocation += 2.1f;
                woodmanager.LoseChoppedWood(2);
                uimanager.TakeAction();
                makePipe = false;
                player.Thinking("I should gain water everyday!", 0);
                uimanager.passiveWater = true;
            }
            else if (pipeLocation < 18)
            {
                Instantiate(pipe2, new Vector2(pipeLocation, -1), Quaternion.identity);
                if (pipeLocation == 15.6f)
                    pipeLocation += 2.4f;
                else pipeLocation += 2.9f;
                woodmanager.LoseChoppedWood(2);
                player.Thinking("Pipes can transfer water.", 0);
                makePipe = false;
                uimanager.TakeAction();
            }
            else makePipe = false;
            player.idle = true;
        }
        if (makeWoodenSpear)
        {
            
            if (inventory.stoneCount < 1 || woodmanager.ChoppedWood <= 1 || player.haveSpear)
            {
                player.idle = true;
                makeWoodenSpear = false;
                return;
            }
            woodmanager.LoseChoppedWood(2);
            inventory.AddStone(-1);
            uimanager.TakeAction();
            player.haveSpear = true;
            makeWoodenSpear = false;
            player.Thinking("Here piggy piggy....", 0);
        }
        if (makeAxe)
        {
            if (inventory.stoneCount <= 1 || woodmanager.ChoppedWood < 1 || player.haveAxe)
            {
                player.idle = true;
                makeAxe = false;
                return;
            }
            woodmanager.LoseChoppedWood(1);
            inventory.AddStone(-2);
            uimanager.TakeAction();
            makeAxe = false;
            player.haveAxe = true;
            player.Thinking("Chopping trees is easier now!", 0);
        }
        if (makeCutter)
        {
            if (woodmanager.ChoppedWood <= 4 || GameObject.Find("WoodCutter(Clone)") || !player.haveAxe || upgrade.tentLevel == 0)
            {
                player.idle = true;
                makeCutter = false;
                return;
            }
            Instantiate(cutter, new Vector2(-2, -1f), Quaternion.identity);
            woodmanager.LoseChoppedWood(5);
            woodmanager.UpdateWoodLimit(10);
            uimanager.TakeAction();
            makeCutter = false;
            player.Thinking("Wood chopping and peeling more efficient. \r\n Less wastage.", 0);
        }
    }
}
