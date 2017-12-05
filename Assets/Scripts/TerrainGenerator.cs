using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class TerrainGenerator : MonoBehaviour {

    public GameObject LoadingScreen, helpscreen, treeThick, treeThick2, treeThin, clay, stone;
    public GameObject flower, flower2, dirt, dirt2, bush, bush2, grass, grass2, grass3;
    public Text LoadingText;
    public Queue<float> LeftSideTree, RightSideTree; //store location of trees in queue

    private float leftside = -6, rightside = 6f;
    private GameManager gamemanager;
    private GameObject TreeParent, ClayParent, StoneParent;

    private void Start()
    {
        gamemanager = GameObject.Find("GameManager").GetComponent("GameManager") as GameManager;
        gamemanager.PauseGame();
        TreeParent = GameObject.Find("TreeParent");
        ClayParent = GameObject.Find("ClayParent");
        StoneParent = GameObject.Find("StoneParent");
        LeftSideTree = new Queue<float>();
        RightSideTree = new Queue<float>();
        InitialiseLeft();
        InitialiseRight();
        LoadingScreen.SetActive(false);
        gamemanager.UnPauseGame();
    }
    private void InitialiseLeft()
    {
        float leftLimit = -42;
        int random;
        //instantiate tree object AND set TeeParent in hierachy as parent 
        for (float i = leftside; i >= leftLimit;)
        {
            if (i > -8 || i < -9.5)
            {
                random = Random.Range(0, 10);
                if (random <= 4)
                {
                    random = Random.Range(0, 10);
                    GameObject treechild;
                    if (random < 2) treechild = Instantiate(treeThick, new Vector2(i, 1.5f), Quaternion.identity);
                    else if (random < 7) treechild = Instantiate(treeThin, new Vector2(i, 1.5f), Quaternion.identity);
                    else treechild = Instantiate(treeThick2, new Vector2(i, 1.5f), Quaternion.identity);
                    //Queue stores location of trees. First element is the next tree to be cut down.
                    LeftSideTree.Enqueue(i);
                    treechild.transform.parent = TreeParent.transform;
                }
            }
            i -= 0.5f;

        }
        //instantiate clay objects AND set ClayParent in hierachy as parent
        for (float j = leftside; j >= leftLimit;)
        {
            random = Random.Range(0, 10);
            if (random <= 2)
            {
                GameObject claychild = Instantiate(clay, new Vector2(j, -2), Quaternion.identity);
                claychild.transform.parent = ClayParent.transform;
            }
            j -= 3;
        }
        //instantiate stone objects AND set StoneParent in hierachy as parent
        for (float k = leftside; k >= leftLimit;)
        {
            random = Random.Range(0, 10);
            if (random <= 2)
            {
                GameObject stonechild = Instantiate(stone, new Vector2(k, -2.17f), Quaternion.identity);
                stonechild.transform.parent = StoneParent.transform;
            }
            k -= 3;
        }
        //instantiate flowers and random stuff
        for (float l = leftside; l >= leftLimit;)
        {
            random = Random.Range(0, 10);
            if (random <= 7)
            {
                random = Random.Range(0, 19);
                if (random == 0) Instantiate(flower, new Vector2(l, -2.24f), Quaternion.identity);
                if (random == 1) Instantiate(flower2, new Vector2(l, -2.24f), Quaternion.identity);
                if (random == 2) Instantiate(bush, new Vector2(l, -1.956f), Quaternion.identity);
                if (random == 3) Instantiate(bush2, new Vector2(l, -2.17f), Quaternion.identity);
                if (random == 4) Instantiate(dirt, new Vector2(l, -2.366f), Quaternion.identity);
                if (random == 5) Instantiate(dirt2, new Vector2(l, -2.43f), Quaternion.identity);
                if (random == 6) Instantiate(grass, new Vector2(l, -2.28f), Quaternion.identity);
                if (random == 7) Instantiate(grass2, new Vector2(l, -2.24f), Quaternion.identity);
                if (random == 8) Instantiate(grass3, new Vector2(l, -2.32f), Quaternion.identity);
            }
            l -= 1;
        }
    }

    private void InitialiseRight()
    {
        float rightLimit = 50;
        int random;
        //instantiate trees
        for (float i = rightside; i <= rightLimit;)
        {
            random = Random.Range(0, 10);
            if (random <= 4)
            {
                random = Random.Range(0, 10);
                GameObject treechild;
                if (random < 2) treechild = Instantiate(treeThick, new Vector2(i, 1.5f), Quaternion.identity);
                else if (random < 7) treechild = Instantiate(treeThin, new Vector2(i, 1.5f), Quaternion.identity);
                else treechild = Instantiate(treeThick2, new Vector2(i, 1.5f), Quaternion.identity);
                RightSideTree.Enqueue(i);
                treechild.transform.parent = TreeParent.transform;
            }
            i += 0.5f;
        }
        //instantiate clay
        for (float j = rightside; j <= rightLimit;)
        {
            random = Random.Range(0, 10);
            if (random <= 2)
            {
                GameObject claychild = Instantiate(clay, new Vector2(j, -2), Quaternion.identity);
                claychild.transform.parent = ClayParent.transform;
            }
            j += 3;
        }
        //instantiate stone
        for (float k = rightside; k <= rightLimit;)
        {
            random = Random.Range(0, 10);
            if (random <= 2)
            {
                GameObject stonechild = Instantiate(stone, new Vector2(k, -2.17f), Quaternion.identity);
                stonechild.transform.parent = StoneParent.transform;
            }
            k += 3;
        }
        for (float l = rightside; l <= rightLimit;)
        {
            random = Random.Range(0, 10);
            if (random <= 7)
            {
                random = Random.Range(0, 9);
                if (random == 0) Instantiate(flower, new Vector2(l, -2.24f), Quaternion.identity);
                if (random == 1) Instantiate(flower2, new Vector2(l, -2.24f), Quaternion.identity);
                if (random == 2) Instantiate(bush, new Vector2(l, -1.956f), Quaternion.identity);
                if (random == 3) Instantiate(bush2, new Vector2(l, -2.17f), Quaternion.identity);
                if (random == 4) Instantiate(dirt, new Vector2(l, -2.366f), Quaternion.identity);
                if (random == 5) Instantiate(dirt2, new Vector2(l, -2.43f), Quaternion.identity);
                if (random == 6) Instantiate(grass, new Vector2(l, -2.28f), Quaternion.identity);
                if (random == 7) Instantiate(grass2, new Vector2(l, -2.24f), Quaternion.identity);
                if (random == 8) Instantiate(grass3, new Vector2(l, -2.32f), Quaternion.identity);
            }
            l += 1;
        }
    }
    public void closeHelpScreen()
    {
        helpscreen.SetActive(false);
    }
    public void openHelpScreen()
    {
        helpscreen.SetActive(true);
    }
}
