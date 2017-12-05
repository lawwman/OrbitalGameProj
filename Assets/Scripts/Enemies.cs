using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Enemies : MonoBehaviour {

    public float health, AttDmg;
    //public Weapons WeaponsScript;

    private int timer = 0;
    private Vector3 move;
    private float Closeness;
    private GameManager gamemanager;
    private FoodManager foodmanager;
    private Animator animator;
    private SpriteRenderer spriterenderer;
    private Player PlayerScript;
    private Pet pet;
    private bool calm = false;
    private bool charge = false;

    // Use this for initialization
    void Start() {

        health = 10;
        AttDmg = 10;
        move = new Vector3(-0.5f, 0, 0);
        gamemanager = GameObject.Find("GameManager").GetComponent("GameManager") as GameManager;
        PlayerScript = GameObject.Find("Player").GetComponent("Player") as Player;
        pet = GameObject.Find("Dog").GetComponent("Pet") as Pet;
        foodmanager = GameObject.Find("InventoryManager").GetComponent("FoodManager") as FoodManager;
        animator = GetComponent<Animator>();
        spriterenderer = GetComponent<SpriteRenderer>();

    }
    private void OnMouseDown()
    {
        if (PlayerScript.haveSpear)
            PlayerScript.attack = PlayerScript.attackMode = true;
    }

    // Update is called once per frame
    void Update() {
        Closeness = Mathf.Abs(PlayerScript.transform.position.x - transform.position.x);
        if (Closeness <= 2 && !calm)
        {
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(PlayerScript.transform.position.x, transform.position.y), 1.5f*Time.deltaTime);
            animator.SetTrigger("Charge");
            charge = true;
            if (transform.position.x < PlayerScript.transform.position.x) spriterenderer.flipX = false;
            else
                spriterenderer.flipX = true;
        }
        else
            IdleMove();
    }

    private void IdleMove()
    {
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
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player" && PlayerScript.attack)
        {
            health -= PlayerScript.AttDmg;
            PlayerScript.Attack(transform.position);
            StartCoroutine(DeadEnemy());
        }
        else if (collision.gameObject.tag == "Pet" && pet.attack)
        {
            Debug.Log(health);
            health -= 0.5f;
            pet.rest();
            StartCoroutine(DeadEnemy());
        }
        else if (collision.gameObject.tag == "Player" && !calm && !PlayerScript.attack && charge)
        {
            gamemanager.health -= 5;
            PlayerScript.Knocked(transform.position);
            calm = true;
            charge = false;
            move *= -1;
            if (move.x > 0) spriterenderer.flipX = false;
            else spriterenderer.flipX = true;
            StartCoroutine(UnCalm());
        }
    }
    IEnumerator DeadEnemy()
    {
        yield return new WaitForSeconds(0.8f);
        if (health <= 0)
        {
            Destroy(this.gameObject);
            gamemanager.pigsKilled += 1;
            PlayerScript.EndAttackMode();
            foodmanager.addFood(3);
            pet.EndAttackMode();
        }
    }
    IEnumerator UnCalm()
    {
        yield return new WaitForSeconds(2f);
        calm = false;
    }
}
