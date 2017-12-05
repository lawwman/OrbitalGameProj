using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class Player : MonoBehaviour
{

    public Inventory inventory;
    public UIManager uimanager;
    public GameManager gamemanager;
    public GameObject bubbleImg, bubbleCloseButton;
    public bool idle, knocked, choppingWood, choppingClay, choppingStone, choppingDam, attackMode, attack;
    public bool haveAxe, haveSpear;
    public Text bubblethoughts;
    public int AttDmg = 5;

    private int timer = 0, knockedTimer = 0;
    private float direction = 0;
    private Vector3 move;
    private Vector2 target;
    private Animator animator;
    private SpriteRenderer spriterenderer;
    private Build build;

    // Use this for initialization
    void Start()
    {
        bubbleCloseButton.SetActive(false);
        animator = GetComponent<Animator>();
        spriterenderer = GetComponent<SpriteRenderer>();
        build = GetComponent<Build>();
        move = new Vector3(0.4f,0,0);
        idle = true;
        choppingWood = choppingClay = choppingStone = choppingDam = knocked = attack = attackMode = false;
        haveAxe = haveSpear = false;
        target = transform.position;
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && !gamemanager.Paused)
        {
            idle = false;
            target = new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y);
        }
        if (!idle)
        {
        target.y = transform.position.y;
        transform.position = Vector2.MoveTowards(transform.position, target, 1.5f*Time.deltaTime);
            if (!attackMode)
            {
                animator.SetTrigger("walk left");
                if (transform.position.x < target.x) spriterenderer.flipX = true;
                else spriterenderer.flipX = false;
            }
            else if (attackMode)
            {
                animator.SetTrigger("SpearMode");
                if (transform.position.x < target.x) spriterenderer.flipX = true;
                else spriterenderer.flipX = false;
            }
        }

        if (transform.position.x == target.x) idle = true;
        build.buildStuff();
        if (idle && !knocked) IdleMove();
        if (knocked) KnockedBack();
        //if (!attackMode) animator.SetTrigger("EndSpearMode");
    }

    private void IdleMove()
    {
        if (!attackMode) animator.SetTrigger("walk left");
        else if (attackMode) animator.SetTrigger("SpearMode");
        if (move.x > 0) spriterenderer.flipX = true;
        else spriterenderer.flipX = false;
        transform.Translate(move * Time.deltaTime);
        timer++;
        if (timer == 60)
        {
            int rand = Random.Range(0, 7);
            if (rand <= 2) move *= -1;
            timer = 0;
        }
    }
    private void KnockedBack()
    {
        if (knockedTimer == 60)
        {
            knockedTimer = 0;
            knocked = false;
            idle = true;
        }
        knockedTimer++;
        if (direction < 0)
        {
            transform.Translate(new Vector3(1.5f, 0, 0) * Time.deltaTime);
            spriterenderer.flipX = false;
        }
        else
        {
            transform.Translate(new Vector3(-1.5f, 0, 0) * Time.deltaTime);
            spriterenderer.flipX = true;
        }
    }
    //Called by other scripts to trigger the player chop anim
    public void Chop(Vector2 objLocation)
    {
        if (haveAxe) animator.SetTrigger("chopAxe");
        else animator.SetTrigger("chop");
        direction = objLocation.x - transform.position.x;
        if (direction < 0)
        {
            spriterenderer.flipX = false;
        }
        else spriterenderer.flipX = true;
    } 
    //instantiate thinking bubble
    //function that calls this pauses game, need to unpause
    //bubble thoughts is a child of close button. Need to setActive close button first
    //if option == 0, thinking starts after few sec, if option == 1, speech bubble instant
    public void Thinking(string thought, int option)
    {
        if (option == 0)
            StartCoroutine(thinkProcess(thought));
        else
        {
            GameObject Bubble = Instantiate(bubbleImg, new Vector2(gameObject.transform.position.x, gameObject.transform.position.y + 3), Quaternion.identity);
            Bubble.transform.SetParent(gameObject.transform);
            bubblethoughts.text = thought;
            bubbleCloseButton.SetActive(true);
        }
    }
    IEnumerator thinkProcess(string thought)
    {
        yield return new WaitForSeconds(0.65f);
        GameObject Bubble = Instantiate(bubbleImg, new Vector2(gameObject.transform.position.x, gameObject.transform.position.y + 3), Quaternion.identity);
        Bubble.transform.SetParent(gameObject.transform);
        StartCoroutine(thinkProcess2(thought));
        gamemanager.PauseGame();

    }
    IEnumerator thinkProcess2(string thought)
    {
        yield return new WaitForSeconds(0.35f);
        bubblethoughts.text = thought;
        bubbleCloseButton.SetActive(true);
    }
    public void stopThinking()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).CompareTag("Bubble"))
            {
                Destroy(transform.GetChild(i).gameObject);
            }
        }
        bubbleCloseButton.SetActive(false);
        gamemanager.UnPauseGame();
    }
    //called by objects whenever player knocks into a collidable surface
    public void Knocked(Vector2 objLocation)
    {
        animator.SetTrigger("knock");
        knocked = true; 
        direction = objLocation.x - transform.position.x;
    }
    public void Attack(Vector2 objLocation)
    {
        animator.SetTrigger("SpearAttack");
        direction = objLocation.x - transform.position.x;
        if (direction < 0) spriterenderer.flipX = false;
        else spriterenderer.flipX = true;
        attack = false;
    }
    public void EndAttackMode()
    {
        attackMode = false;
        animator.SetTrigger("EndSpearMode");
        animator.SetTrigger("walk left");
        StartCoroutine(RealEnd());
    }
    IEnumerator RealEnd()
    {
        yield return new WaitForSeconds(2);
        animator.SetTrigger("EndSpearMode");
        animator.SetTrigger("walk left");
    }
    public void isChoppingWood()
    {
        choppingWood = true;
        StartCoroutine(UnselectWood());
    }
    public void isChoppingClay()
    {
        choppingClay = true;
        StartCoroutine(UnselectClay());
    }
    public void isChoppingStone()
    {
        choppingStone = true;
        StartCoroutine(UnselectStone());
    }
    public void isChoppingDam()
    {
        choppingDam = true;
        StartCoroutine(UnselectDam());
    }
    IEnumerator UnselectWood()
    {
        yield return new WaitForSeconds(3);
        choppingWood = false;
    }
    IEnumerator UnselectStone()
    {
        yield return new WaitForSeconds(3);
        choppingStone = false;
    }
    IEnumerator UnselectClay()
    {
        yield return new WaitForSeconds(3);
        choppingClay = false;
    }
    IEnumerator UnselectDam()
    {
        yield return new WaitForSeconds(3);
        choppingDam = false;
    }
    public void IdleIt()
    {
        idle = true;
    }
}
