using System.Collections;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;

public class ClickableItem : MonoBehaviour
{
    public LiquidEffect liquidEffect;
    public Material material;
    public GameObject[] flavorImages; // array of flavor image game objects

    // enum that are for different states of selection
    private enum SelectionState
    {
        Glassware,
        Color,
        Flavor,
        Finished
    }

    // current state of selection
    private SelectionState currentState = SelectionState.Glassware;
    private Color selectedColor = Color.white;
    private string selectedFlavor = "";
    private string selectedGlassware = "";
    private bool isSelecting = false;

    private void Start()
    {
        // set liquid level to 0 and disable flavor images
        material.SetFloat("_Liquid", 0f);
        foreach (var image in flavorImages)
        {
            image.SetActive(false);
        }
    }

    void Update()
    {
        // checks if selecting is true, and mouse button is clicked
        if (isSelecting && Input.GetMouseButtonDown(0))
        {
            // this casts a ray from the mouse position
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            // if the ray hits something
            if (Physics.Raycast(ray, out hit))
            {
                // this current state handle's the glassware
                if (currentState == SelectionState.Glassware)
                {
                    // checks if you hit the object that is one of the glassware options
                    if (hit.transform.CompareTag("shotglass") ||
                        hit.transform.CompareTag("masonjar") ||
                        hit.transform.CompareTag("decanter") ||
                        hit.transform.CompareTag("doublerocksglass"))
                    {
                        // this updates the selected glassware and move to the next state
                        selectedGlassware = hit.transform.tag;
                        currentState = SelectionState.Color;
                    }
                }
                else if (currentState == SelectionState.Color)
                {
                    // checks if you hit the object that is one of the color options
                    if (hit.transform.CompareTag("clear") ||
                        hit.transform.CompareTag("red") ||
                        hit.transform.CompareTag("green") ||
                        hit.transform.CompareTag("brown"))
                    {
                        // update selected color and apply it to the liquid effect
                        selectedColor = GetColorFromTag(hit.transform.tag);
                        liquidEffect.Top = selectedColor;
                        liquidEffect.Side = selectedColor;
                        StartCoroutine(IncreaseLiquid()); // start's increaseliquid coroutine
                        material.SetColor("_Top", liquidEffect.Top);
                        material.SetColor("_Side", liquidEffect.Side);
                        currentState = SelectionState.Flavor; // move to flavor selection state
                    }
                }
                else if (currentState == SelectionState.Flavor)
                {
                    // check if you hit the object that is one of the flavor options
                    if (hit.transform.CompareTag("cherry") ||
                        hit.transform.CompareTag("honey") ||
                        hit.transform.CompareTag("lightning") ||
                        hit.transform.CompareTag("apple"))
                    {
                        // update selected flavor and log the selection for my clarity sake. 
                        selectedFlavor = hit.transform.tag;
                        UnityEngine.Debug.Log("Selected glassware: " + selectedGlassware +
                                  " | Selected color: " + selectedColor +
                                  " | Selected flavor: " + selectedFlavor);
                        // enable the flavor image corresponding to the selected flavor
                        EnableFlavorImage(selectedFlavor);
                        ResetSelection(); // reset's the selection process
                    }
                }
            }
        }
    }

    // gets color from tag
    Color GetColorFromTag(string tag)
    {
        switch (tag)
        {
            case "clear":
                return Color.white;
            case "red":
                return Color.red;
            case "green":
                return Color.green;
            case "brown":
                return new Color(0.64f, 0.16f, 0.16f);
            default:
                return Color.white;
        }
    }

    // coroutine to increase liquid level
    IEnumerator IncreaseLiquid()
    {
        float currentValue = material.GetFloat("_Liquid");
        while (currentValue < 1f)
        { //increase liquid effect from 0 += 1
            currentValue += 0.1f;
            material.SetFloat("_Liquid", currentValue);
            yield return new WaitForSeconds(0.8f); 
        }
        material.SetFloat("_Liquid", 1f); // making sure liquid level is exactly 1
        while (currentValue > 0f)
        {
            yield return new WaitForSeconds(0.8f); 
            // Disable all flavor images
            foreach (var image in flavorImages)
            {
                image.SetActive(false); //png flavor image gets hidden
            }
            currentValue = 0f; // reset liquid level
            material.SetFloat("_Liquid", currentValue);
        }
        material.SetFloat("_Liquid", 0f); // same as the other setfloat, makes sure liquid level is 1
    }

    // reset selection state
    void ResetSelection()
    {
        isSelecting = false;
        selectedGlassware = "";
        selectedColor = Color.white; //resets all variables
        selectedFlavor = ""; 
        currentState = SelectionState.Glassware; // go back to glassware selection state
    }

    // enable flavor image
    void EnableFlavorImage(string flavorTag)
    {
        UnityEngine.Debug.Log("Flavor tag: " + flavorTag);

        // enable the flavor image that is tied to the flavor tag that the player chooses
        foreach (var image in flavorImages)
        {
            if (image.CompareTag(flavorTag))
            {
                image.SetActive(true);
                break;
            }
        }
    }

    // bool to handle selection process
    void OnMouseDown()
    {
        isSelecting = true;
        UnityEngine.Debug.Log("click? " + isSelecting);
    }
}
