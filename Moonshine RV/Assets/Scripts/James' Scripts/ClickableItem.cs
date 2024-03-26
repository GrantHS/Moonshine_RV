using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;

public class ClickableItem : MonoBehaviour
{
    public GameObject GameManager;
    public LiquidEffect liquidEffect;
    public Material material;
    public GameObject[] flavorImages; // array of flavor image game objects
    public GameObject[] itemChosen;
    private Inventory inventory;

    public OrderSlot orderSlot; // Assign this in the Unity Editor


    private int index;

    InventorySlot.Flavor curFlavor = InventorySlot.Flavor.Apple;
    InventorySlot.Color curColor = InventorySlot.Color.Red;
    InventorySlot.Glass curGlass = InventorySlot.Glass.Double;


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
        index = 0;
        inventory = GameManager.GetComponent<Inventory>();


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
                    if (!HasGlasswareInInventory())
                    {
                        UnityEngine.Debug.Log("You need to acquire glassware first.");
                        return; // player needs to have glassware to go further
                    }
                    // checks if you hit the object that is one of the glassware options
                    if (hit.transform.CompareTag("shotglass") ||
                        hit.transform.CompareTag("masonjar") ||
                        hit.transform.CompareTag("decanter") ||
                        hit.transform.CompareTag("doublerocksglass"))
                    {
                        if (hit.transform.CompareTag("shotglass"))
                        {
                            curGlass = InventorySlot.Glass.Shot;
                            RemoveSelectedGlasswareFromInventory();

                        }
                        else if (hit.transform.CompareTag("masonjar"))
                        {
                            curGlass = InventorySlot.Glass.Mason;
                            RemoveSelectedGlasswareFromInventory();
                        }
                        else if (hit.transform.CompareTag("decanter"))
                        {
                            curGlass = InventorySlot.Glass.Decanter;
                            RemoveSelectedGlasswareFromInventory();
                        }
                        else if (hit.transform.CompareTag("doublerocksglass"))
                        {
                            curGlass = InventorySlot.Glass.Double;
                            RemoveSelectedGlasswareFromInventory();
                        }
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
                        if (hit.transform.CompareTag("clear"))
                        {
                            curColor = InventorySlot.Color.Clear;
                        }
                        else if (hit.transform.CompareTag("red"))
                        {
                            curColor = InventorySlot.Color.Red;
                        }
                        else if (hit.transform.CompareTag("green"))
                        {
                            curColor = InventorySlot.Color.Green;
                        }
                        else if (hit.transform.CompareTag("brown"))
                        {
                            curColor = InventorySlot.Color.Brown;
                        }
                      
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
                        if (hit.transform.CompareTag("cherry"))
                        {
                            curFlavor = InventorySlot.Flavor.Cherry;
                        }
                        else if (hit.transform.CompareTag("honey"))
                        {
                            curFlavor = InventorySlot.Flavor.Honey;
                        }
                        else if (hit.transform.CompareTag("lightning"))
                        {
                            curFlavor = InventorySlot.Flavor.Lightning;
                        }
                        else if (hit.transform.CompareTag("apple"))
                        {
                            curFlavor = InventorySlot.Flavor.Apple;
                        }
                        UnityEngine.Debug.Log(curFlavor);
                        UnityEngine.Debug.Log(curColor);
                        UnityEngine.Debug.Log(curGlass);

                        // update selected flavor and log the selection for my clarity sake. 
                        selectedFlavor = hit.transform.tag;
                     
                        UnityEngine.Debug.Log("Selected glassware: " + selectedGlassware +
                                  " | Selected color: " + selectedColor +
                                  " | Selected flavor: " + selectedFlavor);
                        // enable the flavor image corresponding to the selected flavor
                        EnableFlavorImage(selectedFlavor);

                        for (int i = 0; i < itemChosen.Length; i++)
                        {
                            InventorySlot invs = itemChosen[i].GetComponent<InventorySlot>();
                            if (invs.Flavoring==curFlavor&&invs.Coloring==curColor&&invs.GlassType==curGlass)
                            {
                                index = i;
                                break;
                            }
                        }
                        
                        inventory.GetItem(selectedGlassware, selectedColor, itemChosen[index]);
                        CheckOrderCompletion();

                        ResetSelection(); // reset's the selection process
                    }
                }
            }
        }
    }

    private bool HasGlasswareInInventory()
    {
        
        foreach (var slotGameObject in inventory.InventorySlots)
        {
            // access the inventoryslot component of the gameobject
            InventorySlot slot = slotGameObject.GetComponent<Item>().CurrentItem?.GetComponent<InventorySlot>();

            //checks if the glassware has a title with no falvor or color
            if (slot != null &&
                (slot.GlassType == InventorySlot.Glass.Shot ||
                 slot.GlassType == InventorySlot.Glass.Mason ||
                 slot.GlassType == InventorySlot.Glass.Decanter ||
                 slot.GlassType == InventorySlot.Glass.Double) &&
                slot.Coloring == InventorySlot.Color.None && 
                slot.Flavoring == InventorySlot.Flavor.None)  
            {
                return true; // found at least one glassware item with no color or flavor.
            }
        }
        return false; // no glassware items found.
    }

    private GameObject GetSelectedGlasswareFromInventory()
    {
        
        foreach (var slotGameObject in inventory.InventorySlots)
        {
            // access the onventoryslot component of the gameobject
            InventorySlot slot = slotGameObject.GetComponent<Item>().CurrentItem?.GetComponent<InventorySlot>();

            
            if (slot != null && //checks if the glassware has a title with no falvor or color
                (slot.GlassType == InventorySlot.Glass.Shot ||
                 slot.GlassType == InventorySlot.Glass.Mason ||
                 slot.GlassType == InventorySlot.Glass.Decanter ||
                 slot.GlassType == InventorySlot.Glass.Double) &&
                slot.Coloring == InventorySlot.Color.None &&
                slot.Flavoring == InventorySlot.Flavor.None)
            {
                return slotGameObject; // finds at least one glassware item with no color or flavor
            }
        }
        return null; // no glassware items found
    }

    private void RemoveSelectedGlasswareFromInventory()
    {
        GameObject selectedGlassware = GetSelectedGlasswareFromInventory();
        if (selectedGlassware != null)
        {
            // removes the selected glassware from the inventory
            inventory.RemoveItem(selectedGlassware);

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
        // Set isSelecting to true
        isSelecting = true;
        UnityEngine.Debug.Log("click? " + isSelecting);
    }

    // checks if the player completed the order.
    public void CheckOrderCompletion()
    {
        // Convert the player's selection into the format used by OrderSlot.Order
        int flavorIndex = ConvertFlavorToIndex(curFlavor);
        int colorIndex = ConvertColorToIndex(curColor); // clear will always be 0
        int glassIndex = ConvertGlassToIndex(curGlass);

        // check if the player's selection matches the order
        //if (orderSlot.CheckOrder(flavorIndex, colorIndex, glassIndex))
        //{
        //    UnityEngine.Debug.Log("Order is correct!");

        //    // Find the Customer instance dynamically
        //    Customer customer = FindObjectOfType<Customer>();
        //    if (customer != null)
        //    {
        //        customer.Leave(); // destroys the customer.
        //    }
        //    else
        //    {
        //        UnityEngine.Debug.LogError("No customer found to leave.");
        //    }
        //}
        //else
        //{
        //    UnityEngine.Debug.Log("Order is incorrect!");
        //    // order not correct.
        //}
    }

    // Helper methods to convert the player's selection to indices
    private int ConvertFlavorToIndex(InventorySlot.Flavor flavor)
    {
        // This mapping should match the one used in OrderMaker
        switch (flavor)
        {
            case InventorySlot.Flavor.Lightning: return 0;
            case InventorySlot.Flavor.Cherry: return 1;
            case InventorySlot.Flavor.Apple: return 2;
            case InventorySlot.Flavor.Honey: return 3;
            default: return -1; // Unknown flavor
        }
    }

    private int ConvertColorToIndex(InventorySlot.Color color)
    {
        // Since color is always clear for now, we return 0
        return 0;
    }

    private int ConvertGlassToIndex(InventorySlot.Glass glass)
    {
        // This mapping should match the one used in OrderMaker
        switch (glass)
        {
            case InventorySlot.Glass.Shot: return 0;
            case InventorySlot.Glass.Double: return 1;
            case InventorySlot.Glass.Mason: return 2;
            case InventorySlot.Glass.Decanter: return 3;
            default: return -1; // Unknown glassware
        }
    }

}
