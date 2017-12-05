using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clay : MonoBehaviour {

    private Inventory inventory;
    private Player player;
    private UIManager uimanager;
    private GameManager gamemanager;
    private SpriteRenderer spriterenderer;

    private void Start()
    {
        player = GameObject.Find("Player").GetComponent("Player") as Player;
        inventory = GameObject.Find("InventoryManager").GetComponent("Inventory") as Inventory;
        uimanager = GameObject.Find("UIManager").GetComponent("UIManager") as UIManager;
        gamemanager = GameObject.Find("GameManager").GetComponent("GameManager") as GameManager;
        spriterenderer = GetComponent<SpriteRenderer>();
    }

    void OnMouseDown()
    {
        if (!gamemanager.Paused)
            player.isChoppingClay();
    }
    private void OnMouseEnter() { spriterenderer.color = new Color(1, 1, 1, 0.7f); }
    private void OnMouseExit() { spriterenderer.color = new Color(1, 1, 1, 1); }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (player.choppingClay)
            {
                gameObject.SetActive(false);
                inventory.AddClay(3);
                uimanager.TakeAction();
                player.choppingClay = false;
            }
        }
    }
}
