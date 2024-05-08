using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{

    [SerializeField]
    public GameObject StatusPage;
    public bool StatusEnabled;
    public bool StillWindow;
    public GameObject StillScreen;
    public bool SellWebsite;
    public GameObject SellScreen;

    [SerializeField]
    public List<GameObject> InventorySlots;
    [SerializeField]
    private GameObject InventoryItem;
    [SerializeField]
    private GameObject InventoryBox;
    [SerializeField]
    private bool Stacked;
    [SerializeField]
    GameObject CanvasObject;

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
        if (!StillWindow)
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
        if (StillWindow)
        {
            if (StillScreen.activeInHierarchy)
            {
                StillScreen.SetActive(false);
            }
            else
            {
                StillScreen.SetActive(true);
            }
        }
        if (SellWebsite)
        {
            if (SellScreen.activeInHierarchy)
            {
                SellScreen.SetActive(false);
            }
            else
            {
                SellScreen.SetActive(true);
            }
        }
        
    }

    void CreateInventory()
    {
        for (int i = 0; i < 16; i++)
        {
            GameObject InvenSlot = Instantiate(InventoryItem, InventoryBox.transform);
            
            InventorySlots.Add(InvenSlot);
        }


    }

    public void GetItem(string selectedGlassware, Color selectedColor, GameObject ItemGaining)
    {
        

        for (int i = 0; i < InventorySlots.Count; i++)
        {
            GameObject InvenSlot = InventorySlots[i];
            if (InvenSlot.GetComponent<Item>().Occupied == true)
            {
                GameObject ExistingObject = InvenSlot.GetComponent<Item>().CurrentItem;
                //Debug.Log("Slot Identified");
                if (ItemGaining.GetComponent<InventorySlot>().Flavoring == ExistingObject.GetComponent<InventorySlot>().Flavoring && ItemGaining.GetComponent<InventorySlot>().Coloring == ExistingObject.GetComponent<InventorySlot>().Coloring && ItemGaining.GetComponent<InventorySlot>().GlassType == ExistingObject.GetComponent<InventorySlot>().GlassType)
                {
                    Debug.Log("Match Identified");
                    InvenSlot.GetComponent<Item>().CurrentItem.GetComponent<InventorySlot>().Amount++;
                    InvenSlot.GetComponent<Item>().CurrentItem.GetComponent<InventorySlot>().ChangeText();
                    i = InventorySlots.Count;
                    Stacked = true;
                }

            }
        }
        if (!Stacked)
        {
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
        else
        {
            Stacked = false;
        }
       

            
        

        this.gameObject.GetComponent<MenuManager>().HideHarvest();
    }

    public void getButtonItem (GameObject ItemGaining)
    {


        for (int i = 0; i < InventorySlots.Count; i++)
        {
            GameObject InvenSlot = InventorySlots[i];
            if (InvenSlot.GetComponent<Item>().Occupied == true)
            {
                GameObject ExistingObject = InvenSlot.GetComponent<Item>().CurrentItem;
                //Debug.Log("Slot Identified");
                if (ItemGaining.GetComponent<InventorySlot>().Flavoring == ExistingObject.GetComponent<InventorySlot>().Flavoring && ItemGaining.GetComponent<InventorySlot>().Coloring == ExistingObject.GetComponent<InventorySlot>().Coloring && ItemGaining.GetComponent<InventorySlot>().GlassType == ExistingObject.GetComponent<InventorySlot>().GlassType)
                {
                    Debug.Log("Match Identified");
                    ExistingObject.GetComponent<InventorySlot>().Amount += 1;
                    ExistingObject.GetComponent<InventorySlot>().ChangeText();
                    i = InventorySlots.Count;
                    Stacked = true;
                }

            }
        }
        if (!Stacked)
        {
            for (int i = 0; i < InventorySlots.Count; i++)
            {
                GameObject InvenSlot = InventorySlots[i];

                if (InvenSlot.GetComponent<Item>().Occupied == false)
                {
                    GameObject NewItem = Instantiate(ItemGaining, Vector3.zero, new Quaternion(0f, 0f, 0f, 0f), CanvasObject.transform);
                    //for (i = 0; NewItem.GetComponent<RectTransform>().localScale.x < 1; i++)
                    //{
                    //    var scale = 1f;
                    //    NewItem.GetComponent<RectTransform>().sizeDelta = new Vector3(NewItem.transform.localScale.x, scale, NewItem.transform.localScale.z);
                    //    NewItem.GetComponent<RectTransform>().sizeDelta = new Vector3(scale, NewItem.transform.localScale.y, NewItem.transform.localScale.z);
                    //    NewItem.GetComponent<RectTransform>().sizeDelta = new Vector3(NewItem.transform.localScale.x, NewItem.transform.localScale.y, scale);
                    //}
                    NewItem.SetActive(true);
                    //StartCoroutine(InventoryScaleFix(NewItem,InvenSlot));
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
        else
        {
            Stacked = false;
        }





        this.gameObject.GetComponent<MenuManager>().HideHarvest();
    }

    IEnumerator InventoryScaleFix(GameObject NewItem,GameObject InvenSlot)
    {
        yield return new WaitForSeconds(1);
        NewItem.GetComponent<InventorySlot>().parentAfterDrag = InvenSlot.transform;
        NewItem.GetComponent<InventorySlot>().SwitchSlots();
        InvenSlot.GetComponent<Item>().Occupied = true;
        InvenSlot.GetComponent<Item>().CurrentItem = NewItem;
    }



    public void RemoveItem(GameObject itemToRemove)
    {
        // check to see if an item is able to remove from the inventory
        if (InventorySlots.Contains(itemToRemove))
        {
            // removes the item from weston's inventory
            InventorySlots.Remove(itemToRemove);

            // destroys the glassware item
            Destroy(itemToRemove.GetComponent<Item>().CurrentItem);

            // slot is unoccupied and clear the reference to the current item
            itemToRemove.GetComponent<Item>().Occupied = false;
            itemToRemove.GetComponent<Item>().CurrentItem = null;
        }
        else
        {
            Debug.LogWarning("The item to remove is not in the inventory.");
        }
    }



}
