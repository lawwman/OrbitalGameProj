using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public int stoneCount, clayCount, leatherCount;
    public int stoneLimit, clayLimit, leatherLimit;

    //crafting page
    public GameObject BuildPage;

    public Text stoneDisplay, clayDisplay, leatherDisplay;
    public bool banana = false;

    private void Awake()
    {
        BuildPage.SetActive(false);
    }
    private void Start()
    {
        stoneCount = clayCount = leatherCount = 0;
        stoneLimit = 3;
        clayLimit = 2;
        leatherLimit = 5;
        stoneDisplay.text = "Stone: " + stoneCount.ToString() + " / " + stoneLimit.ToString();
        clayDisplay.text = "Clay: " + clayCount.ToString() + " / " + clayLimit.ToString();
        leatherDisplay.text = "Leather: " + leatherCount.ToString() + " / " + leatherLimit.ToString();
    }
    //do something when limit has been reached!!!!
    public void AddStone(int count)
    {
        stoneCount += count;
        if (stoneCount >= stoneLimit)
            stoneCount = stoneLimit;
        stoneDisplay.text = "Stone: " + stoneCount.ToString() + " / " + stoneLimit.ToString();
    }
    public void AddClay(int count)
    {
        clayCount += count;
        if (clayCount >= clayLimit)
            clayCount = clayLimit;
        clayDisplay.text = "Clay: " + clayCount.ToString() + " / " + clayLimit.ToString();
    }
    public void AddLeather(int count)
    {
        leatherCount += count;
        if (leatherCount >= leatherLimit)
            leatherCount = leatherLimit;
        leatherDisplay.text = "Leather: " + leatherCount.ToString() + " / " + leatherLimit.ToString();
    }
    public void UpdateStoneLimit(int count)
    {
        stoneLimit += count;
        stoneDisplay.text = "Stone: " + stoneCount.ToString() + " / " + stoneLimit.ToString();
    }
    public void UpdateClayLimit(int count)
    {
        clayLimit += count;
        clayDisplay.text = "Clay: " + clayCount.ToString() + " / " + clayLimit.ToString();
    }
    public void OpenBuildPage()
    {
        BuildPage.SetActive(true);
    }
    public void CloseBuildPage()
    {
        BuildPage.SetActive(false);
    }
    IEnumerator Deselect(bool item)
    {
        yield return new WaitForSecondsRealtime(3);
        Debug.Log("unselected");
    }
}