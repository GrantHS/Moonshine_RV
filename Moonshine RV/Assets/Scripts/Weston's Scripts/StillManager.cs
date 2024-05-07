using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StillManager : MonoBehaviour
{

    [SerializeField]
    private TMP_Text BrewTimeVisual;
    [SerializeField]
    private int Color, Flavor, Glass;
    [SerializeField]
    bool ColorSet, FlavorSet, GlassSet;

    [SerializeField]
    private TMP_Text FlavorText;
    private int FlavorCycle = 3;

    [Header("Brewing Variables")]
    bool Brewing;
    [SerializeField]
    private float ShotBrewTime, DoubleBrewTime, MasonBrewTime, DecanterBrewTime;

    [SerializeField]
    float BrewingTime;
    [SerializeField]
    GameObject ColorSlot, FlavorSlot, GlassSlot, FinishedSlot, BrewingShield;
    GameObject CurrentDrink;
    [SerializeField]
    private List<GameObject> ClearDrinks, RedDrinks, GreenDrinks, BrownDrinks;

    public void SetColoring(int Colorint)
    {
        Color = Colorint;
        ColorSet = true;
    }

    public void SetFlavoring(int Flavorint)
    {
        Flavor = Flavorint;
        FlavorSet = true;
    }

    public void SetGlassType(int Glassint)
    {
        Glass = Glassint;
        GlassSet = true;
    }

    public void ClearColor()
    {
        if(ColorSlot.transform.childCount==0) ColorSet = false;
    }

    public void ClearFlavor()
    {
        if (FlavorSlot.transform.childCount == 0) FlavorSet = false;
    }

    public void ClearGlass()
    {
        if (GlassSlot.transform.childCount == 0) GlassSet = false;
    }




    // Start is called before the first frame update
    void Start()
    {
        
    }

    


    // Update is called once per frame
    void Update()
    {
        if (GlassSet && FlavorSet && ColorSet && !Brewing && FinishedSlot.transform.childCount < 5) 
        {
            switch (Glass)
            {
                case 0://ShotGlass
                    BrewingTime = ShotBrewTime;
                    break;
                case 1://Double
                    BrewingTime = DoubleBrewTime;
                    break;
                case 2: //MasonJar
                    BrewingTime = MasonBrewTime;
                    break;
                case 3: //Decanter
                    BrewingTime = DecanterBrewTime;
                    break;
            }
            Brewing = true;
            InvokeRepeating("TextEffect", 0f, 1f);
            BrewingShield.SetActive(true);
        }
        if (Brewing)
        {
            if (BrewingTime > 0)
            {
                BrewingTime -= Time.deltaTime;
                
            }
            else
            {
                BrewingTime = 0;
                Brewing = false;
                CancelInvoke("TextEffect");
                BrewingShield.SetActive(false);
                //Destroy all of the ingredients used
                if (GlassSlot.GetComponent<Item>().CurrentItem.GetComponent<InventorySlot>().Amount > 1)
                {
                    //Debug.Log("Glass Removed");
                    GameObject GlassObject = GlassSlot.GetComponent<Item>().CurrentItem;
                    GlassObject.GetComponent<InventorySlot>().Amount -= 1;
                    GlassObject.GetComponent<InventorySlot>().ChangeText();
                }
                else
                {
                    Destroy(GlassSlot.GetComponent<Item>().CurrentItem);
                    GlassSlot.GetComponent<Item>().Occupied = false;
                    GlassSet = false;
                }
                if (ColorSlot.GetComponent<Item>().CurrentItem.GetComponent<InventorySlot>().Amount > 1)
                {
                    GameObject ColorObject = ColorSlot.GetComponent<Item>().CurrentItem;
                    ColorObject.GetComponent<InventorySlot>().Amount -= 1;
                    ColorObject.GetComponent<InventorySlot>().ChangeText();
                }
                else
                {
                    Destroy(ColorSlot.GetComponent<Item>().CurrentItem);
                    ColorSlot.GetComponent<Item>().Occupied = false;
                    ColorSet = false;
                }
                if (FlavorSlot.GetComponent<Item>().CurrentItem.GetComponent<InventorySlot>().Amount > 1)
                {
                    GameObject FlavorObject = FlavorSlot.GetComponent<Item>().CurrentItem;
                    FlavorObject.GetComponent<InventorySlot>().Amount -= 1;
                    FlavorObject.GetComponent<InventorySlot>().ChangeText();
                }
                else
                {
                    Destroy(FlavorSlot.GetComponent<Item>().CurrentItem);
                    FlavorSlot.GetComponent<Item>().Occupied = false;

                    FlavorSet = false;
                }
               
                

                //finish the drink and put it in the finished drink slot

                if (Color == 0)//Search through Clear Drinks
                {
                    for (int i = 0; i < ClearDrinks.Count + 1; i++) 
                    {
                        if ((int)ClearDrinks[i].GetComponent<InventorySlot>().Flavoring == Flavor && (int)ClearDrinks[i].GetComponent<InventorySlot>().GlassType == Glass) 
                        {
                            CurrentDrink = ClearDrinks[i];
                            i = ClearDrinks.Count + 1;
                        }
                    }

                }
                if (Color == 1)//Search through Red Drinks
                {
                    for (int i = 0; i < RedDrinks.Count + 1; i++)
                    {
                        if ((int)RedDrinks[i].GetComponent<InventorySlot>().Flavoring == Flavor && (int)RedDrinks[i].GetComponent<InventorySlot>().GlassType == Glass)
                        {
                            CurrentDrink = RedDrinks[i];
                            i = RedDrinks.Count + 1;
                        }
                    }
                }
                if (Color == 2)//Search through Green Drinks
                {
                    for (int i = 0; i < GreenDrinks.Count + 1; i++)
                    {
                        if ((int)GreenDrinks[i].GetComponent<InventorySlot>().Flavoring == Flavor && (int)GreenDrinks[i].GetComponent<InventorySlot>().GlassType == Glass)
                        {
                            CurrentDrink = GreenDrinks[i];
                            i = GreenDrinks.Count + 1;
                        }
                    }
                }
                if (Color == 3)//Search through Brown Drinks
                {
                    for (int i = 0; i < BrownDrinks.Count + 1; i++)
                    {
                        if ((int)BrownDrinks[i].GetComponent<InventorySlot>().Flavoring == Flavor && (int)BrownDrinks[i].GetComponent<InventorySlot>().GlassType == Glass)
                        {
                            CurrentDrink = BrownDrinks[i];
                            i = BrownDrinks.Count + 1;
                        }
                    }

                }

                if (CurrentDrink != null)
                {
                    Instantiate(CurrentDrink, FinishedSlot.transform);
                    CurrentDrink.GetComponent<InventorySlot>().PreviousSlot = FinishedSlot.transform;
                }
                else
                {
                    Debug.Log("Prefab not Found!");
                }


            }
            DisplayTime(BrewingTime);
        }
    }

    private void DisplayTime(float TimetoDisplay) //displays the time properly
    {
        if (TimetoDisplay < 0)
        {
            TimetoDisplay = 0;
        }
        else if (TimetoDisplay > 0)
        {
            TimetoDisplay += 1;
        }
        
        float minutes = Mathf.FloorToInt(TimetoDisplay / 60); //converts the float to minutes
        float seconds = Mathf.FloorToInt(TimetoDisplay % 60); //converts the float to seconds

        BrewTimeVisual.text = string.Format("{0:00}:{1:00}", minutes, seconds); //formats the text for a time output
    }

    void TextEffect()
    {
        switch (FlavorCycle)
        {
            case 3:
                FlavorCycle--;
                FlavorText.text = "Brewing";
                break;
            case 2:
                FlavorCycle--;
                FlavorText.text = "Brewing.";
                break;
            case 1:
                FlavorCycle--;
                FlavorText.text = "Brewing..";
                break;
            case 0:
                FlavorCycle = 3;
                FlavorText.text = "Brewing...";
                break;
        } //adds a effect to the text above the timer on the still screen
    }



}
