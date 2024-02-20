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
        Amount = 1;
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

    public void OnEndDrag(PointerEventData eventData)
    {
        transform.SetParent(parentAfterDrag);
        image.raycastTarget = true;
        if (PreviousSlot != null )
        {
            if (PreviousSlot.childCount == 0)
            {
                PreviousSlot.GetComponent<Item>().Occupied = false;
                PreviousSlot.GetComponent<Item>().CurrentItem = null;
            }
        }
        PreviousSlot = parentAfterDrag;
    }


    public void SwitchSlots()
    {
        transform.SetParent(parentAfterDrag);
        image.raycastTarget = true;
        PreviousSlot = parentAfterDrag;
    }

    public void ChangeText()
    {
        TextAmount.text = Amount.ToString();
    }
}
