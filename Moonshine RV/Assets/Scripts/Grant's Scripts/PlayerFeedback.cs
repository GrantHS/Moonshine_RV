using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Timeline;
using UnityEngine.UI;

//Attach this script to the feedback panel object
public class PlayerFeedback : MonoBehaviour
{
    public GameObject _feedbackPanel;

    private Text text;
    private float visibility;
    private float maxVis = 1f;
    private float minVis = 0f;
    private float visChange = .005f;
    private bool isFading = false;
    private bool cherBuy, appBuy, honBuy = false;
    private MenuManager menuManager;
    private int currency;
    private int cherTreePrice, appTreePrice, honTreePrice, shotGlassPrice, doubleGlassPrice, masonGlassPrice, decanterPrice;
    // Start is called before the first frame update


    public void Activate()
    {
        //Debug.Log("activating");
        visibility = maxVis;
        _feedbackPanel.gameObject.GetComponent<Image>().color = new Color(255f, 255f, 255f, visibility);
        text.GetComponent<Text>().color = new Color(50f, 50f, 50f, visibility);
        isFading = true;
    }

    private void OnEnable()
    { 
        menuManager = this.GetComponent<MenuManager>();
        cherTreePrice = menuManager.CherTreePrice;
        appTreePrice = menuManager.AppTreePrice;
        honTreePrice = menuManager.HonTreePrice;
        shotGlassPrice = menuManager.ShotGlassPrice;
        doubleGlassPrice = menuManager.DoubleGlassPrice;
        masonGlassPrice = menuManager.MasonGlassPrice;
        decanterPrice = menuManager.CanterGlassPrice;
        text = _feedbackPanel.gameObject.GetComponentInChildren<Text>();
        visibility = minVis;
        _feedbackPanel.gameObject.GetComponent<Image>().color = new Color(255f, 255f, 255f, visibility);
        text.GetComponent<Text>().color = new Color(0f, 0f, 0f, visibility);
    }

    private void FixedUpdate()
    {
        if (isFading)
        {
            visibility = visibility - visChange;
            _feedbackPanel.gameObject.GetComponent<Image>().color = new Color(255f, 255f, 255f, visibility);
            text.GetComponent<Text>().color = new Color(0f, 0f, 0f, visibility);
            if (visibility <= minVis)
            {
                isFading = false;
                Debug.Log("Faded out");
            }
        }
        
    }

    private bool EnoughMoney(int money, int price)
    {
        Debug.Log("Money: " + money + "; Price: " + price);
        if (money > price || money == price) return true;
        else 
        {
            text.text = "Not Enough Money";
            Activate();
            return false;
        }
    }

    public void LightingCollected()
    {
        text.text = "Lighting Flavor Harvested!";
        Activate();
    }
    public void ClearCollected()
    {
        text.text = "Clear Coloring Harvested!";
        Activate();
    }
    public void CherryCollected()
    {
        text.text = "Cherry Flavor Harvested!";
        Activate();
    }
    public void RedCollected()
    {
        text.text = "Red Coloring Harvested!";
        Activate();
    }
    public void AppleCollected()
    {
        text.text = "Apple Flavor Harvested!";
        Activate();
    }
    public void GreenCollected()
    {
        text.text = "Green Coloring Harvested!";
        Activate();
    }
    public void HoneyCollected()
    {
        text.text = "Honey Flavor Harvested!";
        Activate();
    }
    public void BrownCollected()
    {
        text.text = "Brown Coloring Harvested!";
        Activate();
    }
    public void OnShotGlassClick()
    {
        currency = menuManager.Currency;
        if(EnoughMoney(currency, shotGlassPrice))
        {
            text.text = "Shot Glass Purchased!";
            Activate();
        }
    }
    public void OnRocksGlassClick()
    {
        currency = menuManager.Currency;
        if (EnoughMoney(currency, doubleGlassPrice))
        {
            text.text = "Rocks Glass Purchased!";
            Activate();
        }
        
    }
    public void OnMasonJarClick()
    {
        currency = menuManager.Currency;
        if (EnoughMoney(currency, masonGlassPrice))
        {
            text.text = "Mason Jar Purchased!";
            Activate();
        }
        
    }
    public void OnDecanterClick()
    {
        currency = menuManager.Currency;
        if (EnoughMoney(currency, decanterPrice))
        {
            text.text = "Decanter Purchased!";
            Activate();
        }
        
    }

    public void OnLightingClick()
    {
        text.text = "Already Planted in Backyard!";
        Activate();
    }

    public void OnCherryTreeBuy()
    {
        currency = menuManager.Currency;
        if (EnoughMoney(currency, cherTreePrice))
        {
            text.text = "Cherry Tree Planted Outside!";
            Activate();
        }
        
    }
    public void OnAppleTreeBuy()
    {
        currency = menuManager.Currency;
        if (EnoughMoney(currency, appTreePrice))
        {
            text.text = "Apple Tree Planted Outside!";
            Activate();
        }
        
    }
    public void OnHoneyTreeBuy()
    {
        currency = menuManager.Currency;
        if (EnoughMoney(currency, honTreePrice))
        {
            text.text = "Honey Tree Planted Outside!";
            Activate();
        }
        
    }
    
}
