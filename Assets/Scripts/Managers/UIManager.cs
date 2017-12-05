using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Text countDay;
    public Text diary;
    public Text countActions;
    public GameObject diaryPage, sprungTrap, firewood, boar;
    public Player PlayerScript;
    public bool passiveWater = false;

    private DiaryGenerator diaryGenerator;
    private RandomEvent randomEvent;
    private GameManager gamemanager;
    private FoodManager foodmanager;
    private WaterManager watermanager;
    public Achievement achievement;

    private int count, actions, actionsLimit, pageNum;

    private string diarystuff;

    private void Awake()
    {
        HideDiary();
        randomEvent = GetComponent<RandomEvent>();
        diaryGenerator = GetComponent<DiaryGenerator>();
        gamemanager = GameObject.Find("GameManager").GetComponent("GameManager") as GameManager;
        foodmanager = GameObject.Find("InventoryManager").GetComponent("FoodManager") as FoodManager;
        watermanager = GameObject.Find("InventoryManager").GetComponent("WaterManager") as WaterManager;
    }
    private void Start()
    {
        actions = actionsLimit = 8;
        count = 1;
        countActions.text = "Actions left: " + actions.ToString();
        countDay.text = "Day " + count.ToString();
    }
    public void NextDay ()
    {
        gamemanager.PauseGame();
        //check for rotten food
        foodmanager.CheckFood();
        //update day
        count += 1;
        gamemanager.numDays = count;
        if (count == 3) achievement.Achieve(1);
        pageNum = 0;
        actions = actionsLimit;
        //add water if dam has been unlocked and last pipe laid
        if (passiveWater)
            watermanager.addWater(1);
        //if vase exists, call function Harden() in vase script
        if (GameObject.Find("Vase 1(Clone)"))
        {
            Vase vase = GameObject.Find("Vase 1(Clone)").GetComponent("Vase") as Vase;
            vase.Harden();
        }
        //if fire exists, extinguish it
        if (GameObject.Find("Fire(Clone)"))
        {
            //if fire exists, increase warmth
            gamemanager.Decrement(0, -5, 0, 0);
            Destroy(GameObject.Find("Fire(Clone)"));
            //to prevent multiple instantiates of firewood
            if (GameObject.Find("firewood(Clone)") == null)
                Instantiate(firewood, new Vector2(0.5f, -2f), Quaternion.identity);
        }
        //else reduce warmth by 15
        else gamemanager.Decrement(0, 15, 0, 0);
        //if trap exists, calc probability of catching rabit
        if (GameObject.Find("Trap(Clone)"))
        {
            int rand = Random.Range(0,100);
            //currently set to 60% trap activation
            if (rand <= 60)
            {
                Destroy(GameObject.Find("Trap(Clone)"));
                Instantiate(sprungTrap, new Vector2(-3, -2.2f), Quaternion.identity);
                achievement.Achieve(3);
            }
        }
        UpdatePlayerandDiary(count);
        gamemanager.CheckIfDead();
    }
    public void UpdatePlayerandDiary(int count)
    {
        countDay.text = "Day " + count.ToString();
        countActions.text = "Actions left: " + actions.ToString();
        diaryPage.SetActive(true);
        gamemanager.Decrement(10, 0, 0, 15);
        diarystuff = "Dear diary, today is day " + count + ". \r\n"  + diaryGenerator.generateDiary();
        diary.text = diarystuff;
    }
    //direction == +1 if going to nextpage. direction == -1 if going to prev page
    //0 is first page.
    public void NextPage(int direction)
    {
        pageNum += direction;
        if (pageNum == 0) diary.text = diarystuff;
        else if (pageNum < 0)
        {
            pageNum = 0;
            return;
        }
        else if (pageNum == 1) diary.text = "Tip of the day! \r\n \r\n" + diaryGenerator.generateHint();
        else if (pageNum == 2) diary.text = "Achievements \r\n \r\n \r\n" + achievement.generateAchievement();
        else if (pageNum > 2) pageNum = 2;
    }
    private void Update()
    {
        if (actions == 0)
            NextDay();
        gamemanager.CheckIfDead();
    }
    public void TakeAction()
    {
        int chance = 0;
        actions -= 1;
        countActions.text = "Actions left: " + actions.ToString();
        chance = Random.Range(0, 100);
        if (chance <= 20 && actions > 1) // 20% chance of random event being generated
        {
            randomEvent.activateRandomEvent();
        }
        chance = Random.Range(0, 100);
        if (chance <= 8 && count >= 10) // 8% chance to spawn pig after day 10
        {
            PlayerScript.Thinking("I smell bacon...", 0);
            Instantiate(boar, new Vector2(-2, -2.1f), Quaternion.identity);
        }
    }
    public int GetDay()
    {
        return count;
    }
    public int GetAction()
    {
        return actions;
    }
    public void IncAction()
    {
        actionsLimit += 1;
    }
    public void HideDiary()
    {
        diaryPage.SetActive(false);
    }
}
