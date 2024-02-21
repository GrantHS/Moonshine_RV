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
                    // if you hit the object is one of the glassware options
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
                    // Check if the hit object is one of the color options
                    if (hit.transform.CompareTag("clear") ||
                        hit.transform.CompareTag("red") ||
                        hit.transform.CompareTag("green") ||
                        hit.transform.CompareTag("brown"))
                    {
                        // Update selected color and apply it to the liquid effect
                        selectedColor = GetColorFromTag(hit.transform.tag);
                        liquidEffect.Top = selectedColor;
                        liquidEffect.Side = selectedColor;
                        StartCoroutine(IncreaseLiquid()); // Start liquid increase animation
                        material.SetColor("_Top", liquidEffect.Top);
                        material.SetColor("_Side", liquidEffect.Side);
                        currentState = SelectionState.Flavor; // Move to flavor selection state
                    }
                }
                else if (currentState == SelectionState.Flavor)
                {
                    // Check if the hit object is one of the flavor options
                    if (hit.transform.CompareTag("cherry") ||
                        hit.transform.CompareTag("honey") ||
                        hit.transform.CompareTag("lightning") ||
                        hit.transform.CompareTag("apple"))
                    {
                        // Update selected flavor and log the selection
                        selectedFlavor = hit.transform.tag;
                        Debug.Log("Selected glassware: " + selectedGlassware +
                                  " | Selected color: " + selectedColor +
                                  " | Selected flavor: " + selectedFlavor);
                        // Enable the flavor image corresponding to the selected flavor
                        EnableFlavorImage(selectedFlavor);
                        ResetSelection(); // Reset the selection process
                    }
                }
            }
        }
    }

    // Get color from tag
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
                return new Color(0.64f, 0.16f, 0.16f); // brown color
            default:
                return Color.white;
        }
    }

    // Coroutine to increase liquid level
    IEnumerator IncreaseLiquid()
    {
        float currentValue = material.GetFloat("_Liquid");
        while (currentValue < 1f)
        {
            currentValue += 0.1f;
            material.SetFloat("_Liquid", currentValue);
            yield return new WaitForSeconds(0.8f); // Wait before increasing more
        }
        material.SetFloat("_Liquid", 1f); // Ensure liquid level is exactly 1
        while (currentValue > 0f)
        {
            yield return new WaitForSeconds(0.8f); // Wait before decreasing
            // Disable all flavor images
            foreach (var image in flavorImages)
            {
                image.SetActive(false);
            }
            currentValue = 0f; // Reset liquid level
            material.SetFloat("_Liquid", currentValue);
        }
        material.SetFloat("_Liquid", 0f); // Ensure liquid level is exactly 0
    }

    // Reset selection state
    void ResetSelection()
    {
        isSelecting = false;
        selectedGlassware = "";
        selectedColor = Color.white;
        selectedFlavor = "";
        currentState = SelectionState.Glassware; // Go back to glassware selection state
    }

    // Enable flavor image
    void EnableFlavorImage(string flavorTag)
    {
        Debug.Log("Flavor tag: " + flavorTag);

        // Enable the flavor image corresponding to the flavor tag
        foreach (var image in flavorImages)
        {
            if (image.CompareTag(flavorTag))
            {
                image.SetActive(true);
                break;
            }
        }
    }

    // Handle mouse down event to initiate selection process
    void OnMouseDown()
    {
        isSelecting = true;
    }
}
