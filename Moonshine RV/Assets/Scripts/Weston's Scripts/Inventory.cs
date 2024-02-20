using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{

    [SerializeField]
    public GameObject StatusPage;
    public bool StatusEnabled;

    [SerializeField]
    private List<GameObject> InventorySlots;
    [SerializeField]
    private GameObject InventoryItem;
    [SerializeField]
    private GameObject InventoryBox;


    // Start is called before the first frame update
    void Start()
    {
        CreateInventory();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1) && StatusEnabled) 
        {
            ToggleStatusPage();
        }
    }

    private void ToggleStatusPage()
    {
        if (StatusPage.activeInHierarchy)
        {
            StatusPage.SetActive(false);
        }
        else
        {
            StatusPage.SetActive(true);
        }
        
    }

    void CreateInventory()
    {
        for (int i = 0; i < 24; i++)
        {
            GameObject InvenSlot = Instantiate(InventoryItem, InventoryBox.transform);
            InventorySlots.Add(InvenSlot);
        }


    }

    public void GetItem(GameObject ItemGaining)
    {
        

        for (int i = 0; i < InventorySlots.Count; i++)
        {
            GameObject InvenSlot = InventorySlots[i];
            if (InvenSlot.GetComponent<Item>().Occupied == true)
            {
                GameObject ExistingObject = InvenSlot.GetComponent<Item>().CurrentItem;
                Debug.Log("Slot Identified");
                if (ItemGaining.GetComponent<InventorySlot>().Flavoring == ExistingObject.GetComponent<InventorySlot>().Flavoring && ItemGaining.GetComponent<InventorySlot>().Coloring == ExistingObject.GetComponent<InventorySlot>().Coloring && ItemGaining.GetComponent<InventorySlot>().GlassType == ExistingObject.GetComponent<InventorySlot>().GlassType)
                {
                    Debug.Log("Match Identified");
                    InvenSlot.GetComponent<Item>().CurrentItem.GetComponent<InventorySlot>().Amount++;
                    InvenSlot.GetComponent<Item>().CurrentItem.GetComponent<InventorySlot>().ChangeText();
                    i = InventorySlots.Count;
                }

            }
        }
        

        for (int i = 0; i < InventorySlots.Count; i++)
        {
            GameObject InvenSlot = InventorySlots[i];

            if (InvenSlot.GetComponent<Item>().Occupied == false)
            {
                GameObject NewItem = Instantiate(ItemGaining, Vector3.zero, new Quaternion(0f, 0f, 0f, 0f));
                NewItem.GetComponent<InventorySlot>().parentAfterDrag = InvenSlot.transform;
                NewItem.GetComponent<InventorySlot>().SwitchSlots();
                i = InventorySlots.Count;
                InvenSlot.GetComponent<Item>().Occupied = true;
                InvenSlot.GetComponent<Item>().CurrentItem = NewItem;

            }

            if (i == InventorySlots.Count - 1 && (InventorySlots[i].GetComponent<Item>().Occupied == true))
            {
                Debug.Log("Inventory Full");
            }
        }


    }


}
