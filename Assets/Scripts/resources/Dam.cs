using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dam : MonoBehaviour {

    public bool broken = false;
    private GameManager gamemanager;
    private Animator animator;
    private UIManager uimanager;
    private Player player;
    private int hp;

    void Start () {
        animator = GetComponent<Animator>();
        gamemanager = GameObject.Find("GameManager").GetComponent("GameManager") as GameManager;
        player = GameObject.Find("Player").GetComponent("Player") as Player;
        uimanager = GameObject.Find("UIManager").GetComponent("UIManager") as UIManager;
        hp = 15;
    }
	
    void OnMouseDown()
    {
        if (!gamemanager.Paused)
            player.isChoppingDam();
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player") && !gamemanager.Paused)
        {
            if (player.choppingDam)
            {
                hp -= 1;
                uimanager.TakeAction();
                player.Chop(transform.position);
                player.choppingDam = false;
            }
            if (hp == 0)
            {
                animator.SetTrigger("DamBroken");
                Destroy(GetComponent<Collider2D>());
                broken = true;
            }
        }
    }
}
