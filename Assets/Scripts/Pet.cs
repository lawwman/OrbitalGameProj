using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pet : MonoBehaviour {

    private Animator animator;
    private SpriteRenderer spriterenderer;
    private FoodManager foodmanager;
    private Player player;
    private int timer = 0, hunger = 5, timerAttack = 0;
    private bool charge = false;
    public bool attack = false;
    private Vector3 move = new Vector3(-0.5f, 0, 0);
    private void Start()
    {
        foodmanager = GameObject.Find("InventoryManager").GetComponent("FoodManager") as FoodManager;
        animator = GetComponent<Animator>();
        spriterenderer = GetComponent<SpriteRenderer>();
        player = GameObject.Find("Player").GetComponent("Player") as Player;
    }
    private void OnMouseDown()
    {
        if (hunger >= 1) Feed();
    }
    private void OnMouseEnter()
    {
        spriterenderer.color = new Color(1, 1, 1, 0.7f);
        player.Thinking("It needs food...(click on it)", 1);
    }
    private void OnMouseExit()
    {
        spriterenderer.color = new Color(1, 1, 1, 1);
        player.stopThinking();
    }
    private void Update()
    {
        if (hunger == 0)
        {
            if (GameObject.FindGameObjectsWithTag("Enemy").Length != 0 && charge)
            {
                transform.position = Vector2.MoveTowards(transform.position, GameObject.FindGameObjectsWithTag("Enemy")[0].transform.position, 1.5f * Time.deltaTime);
                animator.SetTrigger("Charge");
                attack = true;
                if (transform.position.x < GameObject.FindGameObjectsWithTag("Enemy")[0].transform.position.x) spriterenderer.flipX = false;
                else
                    spriterenderer.flipX = true;
            }
            else
            {
                IdleMove();
                RestartAttack();
            }
        }
    }
    private void IdleMove()
    {
        animator.SetTrigger("DogWalk");
        if (move.x > 0) spriterenderer.flipX = false;
        else spriterenderer.flipX = true;
        transform.Translate(move * Time.deltaTime);
        timer++;
        if (timer == 60)
        {
            int rand = Random.Range(0, 7);
            if (rand <= 2) move *= -1;
            timer = 0;
        }
    }
    private void RestartAttack()
    {
        timerAttack++;
        if (timerAttack == 100)
        {
            timerAttack = 0;
            charge = true;
        }
    }
    public void Feed()
    {
        //only feed raw food
        if (foodmanager.FeedFood() == 1)
            hunger -= 1;
        if (hunger == 0) animator.SetTrigger("DogWalk");
    }
    public void rest()
    {
        charge = false;
        attack = false;
        animator.SetTrigger("EndCharge");
    }
    public void EndAttackMode()
    {
        animator.SetTrigger("EndCharge");
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (hunger == 0)
        {
            if (collision.gameObject.tag == "tree")
            {
                move *= -1;
                if (move.x > 0) spriterenderer.flipX = false;
                else spriterenderer.flipX = true;
                transform.Translate(move * Time.deltaTime);
            }
        }
    }
}
