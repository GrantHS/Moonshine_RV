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
    private Text text;
    private float visibility;
    private float maxVis = 1f;
    private float minVis = 0f;
    private float visChange = .005f;
    private bool isFading = false;
    // Start is called before the first frame update


    public void Activate()
    {
        Debug.Log("activating");
        visibility = maxVis;
        this.gameObject.GetComponent<Image>().color = new Color(255f, 255f, 255f, visibility);
        text.GetComponent<Text>().color = new Color(50f, 50f, 50f, visibility);
        isFading = true;
    }

    private void OnEnable()
    { 
        text = this.gameObject.GetComponentInChildren<Text>();
        visibility = minVis;
        this.gameObject.GetComponent<Image>().color = new Color(255f, 255f, 255f, visibility);
        text.GetComponent<Text>().color = new Color(0f, 0f, 0f, visibility);
    }

    private void FixedUpdate()
    {
        if (isFading)
        {
            visibility = visibility - visChange;
            this.gameObject.GetComponent<Image>().color = new Color(255f, 255f, 255f, visibility);
            text.GetComponent<Text>().color = new Color(0f, 0f, 0f, visibility);
            if (visibility <= minVis)
            {
                isFading = false;
                Debug.Log("Faded out");
            }
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
    public void ShotGlassCollected()
    {
        text.text = "Shot Glass Purchased!";
        Activate();
    }
    public void RocksGlassCollected()
    {
        text.text = "Rocks Glass Purchased!";
        Activate();
    }
    public void MasonJarCollected()
    {
        text.text = "Mason Jar Purchased!";
        Activate();
    }
    public void DecanterCollected()
    {
        text.text = "Decanter Purchased!";
        Activate();
    }

    public void CherryTreeBuy()
    {
        text.text = "Cherry Tree Planted Outside!";
        Activate();
    }
    public void AppleTreeBuy()
    {
        text.text = "Apple Tree Planted Outside!";
        Activate();
    }
    public void HoneyTreeBuy()
    {
        text.text = "Honey Tree Planted Outside!";
        Activate();
    }
    
}
