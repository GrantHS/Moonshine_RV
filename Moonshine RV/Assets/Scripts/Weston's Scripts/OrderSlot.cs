using System;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class OrderSlot : MonoBehaviour
{
    public float TimeRemaining;
    [SerializeField]
    private TMP_Text TimeText;

    public int flavor; //stores the flavor of the order
    public int size; //stores the glass size of the order
    public int color; //stores the color of the order

    public GameObject GameManager;
    [SerializeField]
    private List<GameObject> Images;//list of all possible images

    private void Start()
    {
        GameManager = GameObject.Find("GameManager"); //finds the game manager to make the player lose reputation and gain money
    }

    void Update() //handles the countdown until the order is failed
    {
        if (TimeRemaining > 0)
        {
            TimeRemaining -= Time.deltaTime;
        }
        else
        {
            TimeRemaining = 0;
            ReputationPenalty(20); //Removes 20 reputation if you fail an order
            for (int i = 0; i > GameManager.GetComponent<OrderMaker>().Orders.Count+1; i++)
            {
               if(GameManager.GetComponent<OrderMaker>().Orders[i] == this.gameObject)
               {
                    GameManager.GetComponent<OrderMaker>().Orders.Remove(GameManager.GetComponent<OrderMaker>().Orders[i]);
                    i = GameManager.GetComponent<OrderMaker>().Orders.Count + 1;
               }
            }

            GameManager.GetComponent<OrderMaker>().DeList(this.gameObject);
        }
        DisplayTime(TimeRemaining);

    }

    private void DisplayTime(float TimetoDisplay) //displays the time properly
    {
        if (TimetoDisplay < 0)
        {
            TimetoDisplay = 0;
        }
        else if (TimetoDisplay > 0)
        {
            TimetoDisplay += 1;
        }

        float minutes = Mathf.FloorToInt(TimetoDisplay / 60); //converts the float to minutes
        float seconds = Mathf.FloorToInt(TimetoDisplay % 60); //converts the float to seconds

        TimeText.text = string.Format("Time: {0:00}:{1:00}", minutes, seconds); //formats the text for a time output
    }
    /*
     * Quick Guide for List of Icons
     * Colors    Flavors        Glass Size
     * 0-Clear   4-Lightning    8-ShotGlass
     * 1-Red     5-Cherry       9-Double Rock
     * 2-Green   6-Apple        10-Mason Jar
     * 3-Brown   7-Honey        11-Decanter
     */


    public void SetIcons() //Sets the correct icons for the order
    {

        switch (color)
        {
            case 0:
                Images[0].SetActive(true);
                break;
            case 1:
                Images[1].SetActive(true);
                break;
            case 2:
                Images[2].SetActive(true);
                break;
            case 3:
                Images[3].SetActive(true);
                break;
        }

        switch (flavor)
        {
            case 0:
                Images[4].SetActive(true);
                break;
            case 1:
                Images[5].SetActive(true);
                break;
            case 2:
                Images[6].SetActive(true);
                break;
            case 3:
                Images[7].SetActive(true);
                break;
        }

        switch (size)
        {
            case 0:
                Images[8].SetActive(true);
                break;
            case 1:
                Images[9].SetActive(true);
                break;
            case 2:
                Images[10].SetActive(true);
                break;
            case 3:
                Images[11].SetActive(true);
                break;
        }
        


    }

    private void AddTime(float AddedTime)
    {
        TimeRemaining += AddedTime;
    }


    private void ReputationPenalty(int Rep)
    {
        GameManager.GetComponent<MenuManager>().LoseReputation(Rep);
    }

    private void ReputationReward(int Rep)
    {
        GameManager.GetComponent<MenuManager>().GainReputation(Rep);
    }

    public void OrderCompleted()
    {

        ReputationReward(0);
        GameManager.GetComponent<OrderMaker>().DeList(this.gameObject);
    }

    public void OrderCancelled()
    {
        ReputationPenalty(10);
        GameManager.GetComponent<OrderMaker>().DeList(this.gameObject);
    }

}
