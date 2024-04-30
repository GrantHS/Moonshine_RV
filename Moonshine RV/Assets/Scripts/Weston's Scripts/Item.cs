using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Item : MonoBehaviour,IDropHandler
{
    public GameObject CurrentItem;
    public bool Occupied;

    //Special Slots are the ones located in the Still
    //This will allow the game to detect and deny specific items from these slots.

    public enum SpecialSlot { None,StillColor, StillFlavor, StillGlass, FinishedStill,OrderWindow,SellBox }
    [Header("SpecialSlot")]
    [Tooltip("Changes the Slot to a Special Slot")]
    public SpecialSlot SlotType;

    [SerializeField]
    private GameObject GameManager;

    void Start()
    {
        GameManager = GameObject.Find("GameManager");
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (transform.childCount == 0 && SlotType == SpecialSlot.None)
        {
            GameObject dropped = eventData.pointerDrag;
            InventorySlot inventorySlot = dropped.GetComponent<InventorySlot>();
            inventorySlot.parentAfterDrag = transform;
            CurrentItem = dropped;
            Occupied = true;
        }
        else if (transform.childCount == 1 && SlotType == SpecialSlot.None)
        {
            GameObject dropped = eventData.pointerDrag;
            InventorySlot inventorySlot = dropped.GetComponent<InventorySlot>();
            inventorySlot.parentAfterDrag = transform;
            CurrentItem.GetComponent<InventorySlot>().parentAfterDrag = inventorySlot.PreviousSlot;
            CurrentItem.GetComponent<InventorySlot>().SwitchSlots();
            inventorySlot.PreviousSlot.GetComponent<Item>().CurrentItem = CurrentItem;
            CurrentItem = dropped;
            Occupied = true;
        }
        else if (transform.childCount == 0 && SlotType == SpecialSlot.StillColor) 
        {
            GameObject dropped = eventData.pointerDrag;

            //if statement to make sure this slot can only take in colors. Flavoring check is to make sure it isn't a finished drink.
            if (dropped.GetComponent<InventorySlot>().Coloring != InventorySlot.Color.None && dropped.GetComponent<InventorySlot>().Flavoring == InventorySlot.Flavor.None)  
            {
                InventorySlot inventorySlot = dropped.GetComponent<InventorySlot>();
                inventorySlot.parentAfterDrag = transform;
                CurrentItem = dropped;
                Occupied = true;
                int Colorint = (int)dropped.GetComponent<InventorySlot>().Coloring;
                GameManager.GetComponent<StillManager>().SetColoring(Colorint);
            } 
        }
        else if(transform.childCount == 0 && SlotType == SpecialSlot.StillFlavor)
        {
            GameObject dropped = eventData.pointerDrag;

            //if statement to make sure this slot can only take in flavors. Coloring check is to make sure it isn't a finished drink.
            if (dropped.GetComponent<InventorySlot>().Flavoring != InventorySlot.Flavor.None && dropped.GetComponent<InventorySlot>().Coloring == InventorySlot.Color.None)
            {
                InventorySlot inventorySlot = dropped.GetComponent<InventorySlot>();
                inventorySlot.parentAfterDrag = transform;
                CurrentItem = dropped;
                Occupied = true;
                int Flavorint = (int)dropped.GetComponent<InventorySlot>().Flavoring;
                GameManager.GetComponent<StillManager>().SetFlavoring(Flavorint);

            }
        }
        else if(transform.childCount == 0 && SlotType == SpecialSlot.StillGlass)
        {
            GameObject dropped = eventData.pointerDrag;

            //if statement to make sure this slot can only take in glasses. Coloring check is to make sure it isn't a finished drink.
            if (dropped.GetComponent<InventorySlot>().GlassType != InventorySlot.Glass.None && dropped.GetComponent<InventorySlot>().Coloring == InventorySlot.Color.None)
            {
                InventorySlot inventorySlot = dropped.GetComponent<InventorySlot>();
                inventorySlot.parentAfterDrag = transform;
                CurrentItem = dropped;
                Occupied = true;
                int Glassint = (int)dropped.GetComponent<InventorySlot>().GlassType;
                GameManager.GetComponent<StillManager>().SetGlassType(Glassint);
            }
        }
        else if (transform.childCount == 0 && SlotType == SpecialSlot.OrderWindow)
        {
            GameObject dropped = eventData.pointerDrag;
            //if statement to make sure this slot can only take in finished drinks. Checks if it has a Glass and Color
            if (dropped.GetComponent<InventorySlot>().GlassType != InventorySlot.Glass.None && dropped.GetComponent<InventorySlot>().Coloring != InventorySlot.Color.None)
            {
                InventorySlot inventorySlot = dropped.GetComponent<InventorySlot>();
                GameManager.GetComponent<OrderMaker>().CheckOrder(dropped);

            }
        }
        else if (transform.childCount == 0 && SlotType == SpecialSlot.SellBox) //Allows you to sell items
        {
            GameObject dropped = eventData.pointerDrag;
            //sells item and gives player money for the price of the item.
            
            InventorySlot inventorySlot = dropped.GetComponent<InventorySlot>();
            int MoneyMade = 0;
            if ((int)inventorySlot.GlassType < 4) //checks for flavors and colors
            {
                MoneyMade = 5;
            }
            switch ((int)inventorySlot.GlassType)
            {
                case 0:

                    break;
            }

            GameManager.GetComponent<MenuManager>().GainMoney(MoneyMade);
            inventorySlot.GetComponent<InventorySlot>().PreviousSlot.GetComponent<Item>().Occupied = false;
            Destroy(inventorySlot.gameObject);

        }

    }
    //Messages the StillManager to inform them an item has been removed from the still
    public void ClearType(int TypeToClear)
    {
        switch (TypeToClear)
        {
            case 0: //Color
                GameManager.GetComponent<StillManager>().ClearColor();
                break;
            case 1: //Flavor
                GameManager.GetComponent<StillManager>().ClearFlavor();
                break;
            case 2: //Glass
                GameManager.GetComponent<StillManager>().ClearGlass();
                break;
            case 3: //ClearAll
                GameManager.GetComponent<StillManager>().ClearColor();
                GameManager.GetComponent<StillManager>().ClearFlavor();
                GameManager.GetComponent<StillManager>().ClearGlass();
                break;
        }
    }
   
}
