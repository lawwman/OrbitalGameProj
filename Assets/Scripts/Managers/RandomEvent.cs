using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RandomEvent : MonoBehaviour {
    public Text randomText;
    public GameObject randomPage, yesbtn, nobtn, closebtn;

    private GameManager gamemanager;
    private Inventory inventory;
    private FoodManager foodmanager;
    private WaterManager watermanager;
    private Upgrade upgrade;
    private UIManager uimanager;
    private RandEvent[] FoodEvent = new RandEvent[9];
    private RandEvent[] WarmthEvent = new RandEvent[1];
    private RandEvent[] DestroyEvent = new RandEvent[2];
    private RandEvent[] QuestEvent = new RandEvent[2];
    private int eventChosen, typeOfEvent, consequence;

    private void Awake()
    {
        gamemanager = GameObject.Find("GameManager").GetComponent("GameManager") as GameManager;
        inventory = GameObject.Find("InventoryManager").GetComponent("Inventory") as Inventory;
        foodmanager = GameObject.Find("InventoryManager").GetComponent("FoodManager") as FoodManager;
        watermanager = GameObject.Find("InventoryManager").GetComponent("Watermanager") as WaterManager;
        upgrade = GameObject.Find("Player").GetComponent("Upgrade") as Upgrade;
        uimanager = GameObject.Find("UIManager").GetComponent("UIManager") as UIManager;
        randomPage.SetActive(false);
        eventChosen = 0;
    }
    void Start()
    {
        //chance to lose health or gain food
        FoodEvent[0] = new RandEvent("Found many of orange-yellow berries on a  \r\n woody vine. Should I eat?", true);
        FoodEvent[1] = new RandEvent("Found strawberry like berries.. Eat?", true);
        FoodEvent[2] = new RandEvent("Found a beehive. Attempt to Collect?", true);
        FoodEvent[3] = new RandEvent("Found a weird looking thing...do I put it \r\n in my mouth?", true);
        FoodEvent[4] = new RandEvent("Monkeys have stolen my food!", false); //remove all food
        FoodEvent[5] = new RandEvent("Found dead pig!", false); //add 3 meat
        FoodEvent[6] = new RandEvent("Found some maggots!", true);
        FoodEvent[7] = new RandEvent("Found crushed squirrel!", false); //add 1 meat
        FoodEvent[8] = new RandEvent("Found some button mushrooms!", true); //add satiety

        WarmthEvent[0] = new RandEvent("Caught a cold...", false);

        //lose vase and water, check if vase exists
        DestroyEvent[0] = new RandEvent("Vase got broken by falling branch!", false);
        //lose tent and some resources(make sure resources below limit and to add speech bubble)
        //doesn't activate when fire has been upgraded to level 2
        DestroyEvent[1] = new RandEvent("Fire spreaded to tent...",false);

        QuestEvent[0] = new RandEvent("Found a banana! Should I eat? If not \r\n I will keep it", true); //chance to tame monkey
        //50% to lose 3/4 health or permanently gain 1 action
        QuestEvent[1] = new RandEvent("Found a weird herb..Should I eat it? If not \r\n I will throw it away", true);
    }
    public void activateRandomEvent()
    {
        gamemanager.PauseGame();
        randomPage.SetActive(true);
        typeOfEvent = Random.Range(0, 10);
        switch(typeOfEvent)
        {
            case 0:
            case 4:
            case 5:
            case 6:
            case 7:
                ActivateFoodEvent();
                typeOfEvent = 0;
                break;
            case 1:
                ActivateWarmthEvent();
                typeOfEvent = 1;
                break;
            case 2:
            case 8:
            case 9:
                ActivateDestroyEvent();
                typeOfEvent = 2;
                break;
            case 3:
                ActivateQuestEvent();
                typeOfEvent = 3;
                break;
        }
    }
    private void ActivateFoodEvent()
    {
        eventChosen = Random.Range(0, FoodEvent.Length);
        randomText.text = FoodEvent[eventChosen].message;
        if (FoodEvent[eventChosen].YesNoQn) ActivateYesNoBtn();
        else ActivateCloseBtn();
        switch(eventChosen)
        {
            case 0:
            case 1:
            case 2:
            case 3:
            case 6:
            case 8:
                consequence = 10;
                break;
            case 4:
                foodmanager.LoseAllFood();
                break;
            case 5:
                foodmanager.addFood(3);
                break;
            case 7:
                foodmanager.addFood(1);
                break;
        }
    }
    private void ActivateWarmthEvent()
    {
        eventChosen = Random.Range(0, WarmthEvent.Length);
        randomText.text = WarmthEvent[eventChosen].message;
        if (WarmthEvent[eventChosen].YesNoQn) ActivateYesNoBtn();
        else ActivateCloseBtn();
        switch(eventChosen)
        {
            case 0:
                gamemanager.Decrement(0, 10, 0 ,0);
                break;
        }

    }
    private void ActivateDestroyEvent()
    {
        eventChosen = Random.Range(0, DestroyEvent.Length);
        randomText.text = DestroyEvent[eventChosen].message;
        if (DestroyEvent[eventChosen].YesNoQn) ActivateYesNoBtn();
        else ActivateCloseBtn();
        switch(eventChosen)
        {
            case 0:
                if (GameObject.Find("Vase 1(Clone)") && !GameObject.Find("Vase Shelter(Clone)"))
                {
                    GameObject toDestroy = GameObject.Find("Vase 1(Clone)");
                    Destroy(toDestroy);
                    watermanager.LoseAllWater();
                }
                else randomText.text = "A branch has fallen...";
                break;
            case 1:
                if (GameObject.Find("Tent(Clone)") && upgrade.FireLevel <= 1)
                    Destroy(GameObject.Find("Tent(Clone)"));
                else if (GameObject.Find("Tent2(Clone)") && upgrade.FireLevel <= 1)
                    Destroy(GameObject.Find("Tent2(Clone)"));
                else randomText.text = "Nothing happened...";
                break;
        }
    }
    private void ActivateQuestEvent()
    {
        eventChosen = Random.Range(0, QuestEvent.Length);
        randomText.text = QuestEvent[eventChosen].message;
        if (QuestEvent[eventChosen].YesNoQn) ActivateYesNoBtn();
        else ActivateCloseBtn();
        switch(eventChosen)
        {
        }
    }
    public void ifYes()
    {
        //FoodEvent
        if (typeOfEvent == 0)
        {
            int chance = Random.Range(0, 100);
            if(chance < 50)
            {
                gamemanager.Decrement(-consequence, 0, 0, 0);
                randomText.text = "You feel fuller";
            }
            else
            {
                gamemanager.Decrement(0, 0, consequence, 0);
                randomText.text = "Might not have been such a \r\n good idea..";
            }
        }
        //Quest Event
        if (typeOfEvent == 3)
        {
            if(eventChosen == 0)
            {
                //Player chose to eat the banana
                gamemanager.Decrement(-20, 0, 0, 0);
                randomText.text = "Damn that's good.";
            }
            else if(eventChosen == 1)
            {
                //player chose to eat the strange herb
                int chance = Random.Range(0 ,100);
                if (chance < 50)
                {
                    gamemanager.Decrement(0, 0, 40, 0);
                    randomText.text = "Should not put strange things in your mouth..";
                }
                else
                {
                    uimanager.IncAction();
                    randomText.text = "I feel so alive!";
                }
            }
        }
        ActivateCloseBtn();
    }
    public void ifNo()
    {
        randomText.text = "Nothing happened";
        if (typeOfEvent == 3)
        {
            if (eventChosen == 0)
            {
                //player chose to keep the banana
                inventory.banana = true;
                randomText.text = "Might come in handy later on.";
            }
        }
        ActivateCloseBtn();
    }

    public void closeRandomPage()
    {
        randomPage.SetActive(false);
        gamemanager.UnPauseGame();
    }
    private void ActivateYesNoBtn()
    {
        yesbtn.SetActive(true);
        nobtn.SetActive(true);
        closebtn.SetActive(false);
    }
    private void ActivateCloseBtn()
    {
        closebtn.SetActive(true);
        yesbtn.SetActive(false);
        nobtn.SetActive(false);
    }
    class RandEvent {
        public string message;
        public bool YesNoQn;

        public RandEvent(string message, bool YesNoQn)
        {
            this.message = message;
            this.YesNoQn = YesNoQn;
        }
    }
}
