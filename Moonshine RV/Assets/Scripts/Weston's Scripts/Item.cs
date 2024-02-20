using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Item : MonoBehaviour,IDropHandler
{
    public GameObject CurrentItem;
    public bool Occupied;

    public void OnDrop(PointerEventData eventData)
    {
        if (transform.childCount == 0)
        {
            GameObject dropped = eventData.pointerDrag;
            InventorySlot inventorySlot = dropped.GetComponent<InventorySlot>();
            inventorySlot.parentAfterDrag = transform;
            CurrentItem = dropped;
            Occupied = true;
        }
        else if (transform.childCount == 1) 
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

    }

   
}
