using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sprung : MonoBehaviour {

    private FoodManager foodmanager;
    private GameManager gamemanager;

    private void Start()
    {
        foodmanager = GameObject.Find("InventoryManager").GetComponent("FoodManager") as FoodManager;
        gamemanager = GameObject.Find("GameManager").GetComponent("GameManager") as GameManager;
    }
    public void OnMouseDown()
    {
        if (!gamemanager.Paused)
        {
            Destroy(gameObject);
            foodmanager.addFood(3);
        }
    }
}
