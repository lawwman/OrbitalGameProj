using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Upgrade : MonoBehaviour {

    public Inventory inven;
    public UIManager uimana;
    public WoodManager woodmanager;
    public GameObject RoastPit, Kettle, tentLvl1, VaseShelter;
    public int FireLevel = 0, VaseLevel = 0, tentLevel = 0;

    private GameManager gamemanager;
    private Player player;

    void Start () {
        gamemanager = GameObject.Find("GameManager").GetComponent("GameManager") as GameManager;
        player = GetComponent<Player>();
    }
    public void UpGradeFire()
    {
        if (FireLevel == 0 && GameObject.Find("Fire(Clone)") && woodmanager.ChoppedWood >= 5)
        {
            Instantiate(RoastPit, new Vector2(0.5f, -1.49f), Quaternion.identity);
            FireLevel += 1;
            uimana.TakeAction();
            woodmanager.LoseChoppedWood(5);
            player.Thinking("I can cook more meat now.", 0);
            gamemanager.UnPauseGame();
        }
        else
        {
            gamemanager.UnPauseGame();
        }
    }
    public void upGradeVase()
    {
        if (VaseLevel == 0 && inven.clayCount >= 3)
        {
            Instantiate(Kettle, new Vector2(2f, -1.95f), Quaternion.identity);
            VaseLevel += 1;
            uimana.TakeAction();
            inven.AddClay(-3);
            player.Thinking("I can boil more water  \r\n at the same time.", 0);
            gamemanager.UnPauseGame();
        }
        else if (VaseLevel == 1 && woodmanager.ChoppedWood >= 5)
        {
            Instantiate(VaseShelter, new Vector2(2.5f, -1), Quaternion.identity);
            VaseLevel += 1;
            uimana.TakeAction();
            woodmanager.LoseChoppedWood(5);
            player.Thinking("Beware falling branches.", 0);
            gamemanager.UnPauseGame();
        }
        else
        {
            gamemanager.UnPauseGame();
        }
    }
    public void upGradeTent()
    {
        if (tentLevel == 0 && woodmanager.ChoppedWood >= 12)
        {
            Instantiate(tentLvl1, new Vector2(-5, -0.5f), Quaternion.identity);
            Destroy(GameObject.Find("Tent(Clone)"));
            woodmanager.LoseChoppedWood(12);
            tentLevel += 1;
            uimana.TakeAction();
            inven.UpdateClayLimit(2);
            inven.UpdateStoneLimit(2);
            player.Thinking("I can take better naps now!", 0);
            gamemanager.UnPauseGame();
        }
        else
        {
            gamemanager.UnPauseGame();
        }
    }
}
