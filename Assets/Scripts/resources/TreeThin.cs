using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeThin : MonoBehaviour {

    private WoodManager woodmanager;
    private float hp;
    private Player player;
    private UIManager uimanager;
    private GameManager gamemanager;
    private TerrainGenerator TG;
    private Animator animator;
    private SpriteRenderer spriterenderer;

    void Start () {
        //remember to change these
        hp = 8;
        player = GameObject.Find("Player").GetComponent("Player") as Player;
        woodmanager = GameObject.Find("InventoryManager").GetComponent("WoodManager") as WoodManager;
        uimanager = GameObject.Find("UIManager").GetComponent("UIManager") as UIManager;
        gamemanager = GameObject.Find("GameManager").GetComponent("GameManager") as GameManager;
        TG = GameObject.Find("TerrainGenerator").GetComponent("TerrainGenerator") as TerrainGenerator;
        animator = GetComponent<Animator>();
        spriterenderer = GetComponent<SpriteRenderer>();
    }
    void OnMouseDown()
    {
        if (!gamemanager.Paused)
            player.isChoppingWood();
    }
    private void OnMouseEnter() { spriterenderer.color = new Color(1, 1, 1, 0.7f); }
    private void OnMouseExit() { spriterenderer.color = new Color(1, 1, 1, 1); }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (!player.choppingWood)
                player.Knocked(transform.position);
        }
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !gamemanager.Paused)
        {
            if (player.choppingWood)
            {
                if (player.haveAxe) hp -= 3;
                else hp -= 1;
                uimanager.TakeAction();
                player.Chop(transform.position);
                player.choppingWood = false;
            }
        }
        if (hp == 5)
        {
            animator.SetTrigger("TreeChop");
            woodmanager.AddWood(3);
            //Have to constantly - 1 hp at every point. This is to avoid multiple calls to if statement 
            //as the function is collision on STAY.
            hp -= 1;
        }
        if (hp == 1)
        {
            animator.SetTrigger("TreeFall");
            StartCoroutine(DestroyTree());
        }
    }
    IEnumerator DestroyTree()
    {
        yield return new WaitForSeconds(1f);
        gameObject.SetActive(false);
        woodmanager.AddWood(3);
        if (transform.position.x < 0) TG.LeftSideTree.Dequeue();
        else TG.RightSideTree.Dequeue();
    }
}
