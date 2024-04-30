using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class InventorySlot : MonoBehaviour,IBeginDragHandler,IDragHandler,IEndDragHandler
{
    //Handling Dragging Items
    public Transform parentAfterDrag;
    [SerializeField]
    RawImage image;
    [SerializeField]
    public Transform PreviousSlot;
    [SerializeField]
    private Text TextAmount;

    [Header("Ingredients")]
    [SerializeField]
    bool SlotFilled;
    [SerializeField]
    public int Amount;
    public enum Flavor { Lightning, Cherry, Apple, Honey,None }
    [Header("Flavor Settings")]
    [Tooltip("Set the Flavor of Beverage")]
    public Flavor Flavoring;

    public enum Color { Clear, Red, Green, Brown,None }
    [Header("Color Settings")]
    public Color Coloring;

    public enum Glass { Shot, Double, Mason, Decanter,None }
    [Header("Glass Settings")]
    public Glass GlassType;

    void Start()
    {
        //Amount = 1;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        parentAfterDrag = transform.parent;
        transform.SetParent(transform.root);
        transform.SetAsLastSibling();
        image.raycastTarget = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;

    }

    public void OnEndDrag(PointerEventData eventData) //places item into slot
    {
        if (parentAfterDrag.GetComponent<Item>().SlotType == Item.SpecialSlot.None)
        {
            transform.SetParent(parentAfterDrag);
            image.raycastTarget = true;
            if (PreviousSlot != null)
            {

                //Clears types if the object was moved from SpecialSlots (Slots within the Still) 0=Color,1=Flavor,2=Glass,3=All
                if (PreviousSlot.GetComponent<Item>().SlotType == Item.SpecialSlot.StillColor) PreviousSlot.GetComponent<Item>().ClearType(0);
                if (PreviousSlot.GetComponent<Item>().SlotType == Item.SpecialSlot.StillFlavor) PreviousSlot.GetComponent<Item>().ClearType(1);
                if (PreviousSlot.GetComponent<Item>().SlotType == Item.SpecialSlot.StillGlass) PreviousSlot.GetComponent<Item>().ClearType(2);
                if (PreviousSlot.GetComponent<Item>().SlotType == Item.SpecialSlot.FinishedStill) PreviousSlot.GetComponent<Item>().ClearType(3);

                if (PreviousSlot.childCount == 0)
                {
                    PreviousSlot.GetComponent<Item>().Occupied = false;
                    PreviousSlot.GetComponent<Item>().CurrentItem = null;
                }
            }
            PreviousSlot = parentAfterDrag;
        }
        else if (parentAfterDrag.GetComponent<Item>().SlotType == Item.SpecialSlot.StillColor && Coloring != Color.None && Flavoring == Flavor.None)
        {
            transform.SetParent(parentAfterDrag);
            image.raycastTarget = true;
            if (PreviousSlot != null)
            {
                if (PreviousSlot.childCount == 0)
                {
                    PreviousSlot.GetComponent<Item>().Occupied = false;
                    PreviousSlot.GetComponent<Item>().CurrentItem = null;
                }
            }
            PreviousSlot = parentAfterDrag;
        }
        else if (parentAfterDrag.GetComponent<Item>().SlotType == Item.SpecialSlot.StillFlavor && Flavoring != Flavor.None && Coloring == Color.None)
        {
            transform.SetParent(parentAfterDrag);
            image.raycastTarget = true;
            if (PreviousSlot != null)
            {
                if (PreviousSlot.childCount == 0)
                {
                    PreviousSlot.GetComponent<Item>().Occupied = false;
                    PreviousSlot.GetComponent<Item>().CurrentItem = null;
                }
            }
            PreviousSlot = parentAfterDrag;
        }
        else if (parentAfterDrag.GetComponent<Item>().SlotType == Item.SpecialSlot.StillGlass && GlassType != Glass.None && Coloring == Color.None)
        {
            transform.SetParent(parentAfterDrag);
            image.raycastTarget = true;
            if (PreviousSlot != null)
            {
                if (PreviousSlot.childCount == 0)
                {
                    PreviousSlot.GetComponent<Item>().Occupied = false;
                    PreviousSlot.GetComponent<Item>().CurrentItem = null;
                }
            }
            PreviousSlot = parentAfterDrag;
        }
        else if (parentAfterDrag.GetComponent<Item>().SlotType == Item.SpecialSlot.OrderWindow && GlassType != Glass.None && Coloring != Color.None && Flavoring != Flavor.None)
        {
            transform.SetParent(parentAfterDrag);
            image.raycastTarget = true;
            if (PreviousSlot != null)
            {
                if (PreviousSlot.childCount == 0)
                {
                    PreviousSlot.GetComponent<Item>().Occupied = false;
                    PreviousSlot.GetComponent<Item>().CurrentItem = null;
                }
            }
            PreviousSlot = parentAfterDrag;

        }
    }


    public void SwitchSlots() //switches the slot of items if the slot being placed into is occupied
    {
        transform.SetParent(parentAfterDrag);
        image.raycastTarget = true;
        PreviousSlot = parentAfterDrag;
    }

    public void ChangeText() //Adds to the amount if a repeat is discovered
    {
        TextAmount.text = Amount.ToString();
    }
}
