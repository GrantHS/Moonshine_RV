using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderPreview : MonoBehaviour
{
    public int flavor; //stores the flavor of the order
    public int size; //stores the glass size of the order
    public int color; //stores the color of the order

    public GameObject GameManager;
    [SerializeField]
    private List<GameObject> Images;//list of all possible images

    private void Start()
    {
        GameManager = GameObject.Find("GameManager"); //finds the game manager to make the player lose reputation or gain it
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

    


    private void ReputationPenalty(int Rep)
    {
        GameManager.GetComponent<MenuManager>().LoseReputation(Rep);
    }

    private void ReputationReward(int Rep)
    {
        GameManager.GetComponent<MenuManager>().GainReputation(Rep);
    }

    public void OrderAccepted()
    {
        ReputationReward(5);
        GameManager.GetComponent<OrderMaker>().OrderUp(flavor, color, size);
        Destroy(this.gameObject);
    }

    public void OrderDenied()
    {
        ReputationPenalty(5);
        Destroy(this.gameObject);
    }
}
