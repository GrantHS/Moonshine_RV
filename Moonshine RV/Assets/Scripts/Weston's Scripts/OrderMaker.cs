using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class OrderMaker : MonoBehaviour
{
    [SerializeField]
    [Tooltip("Determines the Delay between ordes.")]
    private float orderDelay;

    [SerializeField]
    private GameObject CustomerMove;
  
    [SerializeField]
    int unlockDetermined = 1;
    public List<GameObject> Orders;

    [SerializeField]
    private List<Texture> DrinkImages;

    [SerializeField]
    private GameObject OrderPrefab;

    [SerializeField]
    private GameObject OrderPreview;

    [SerializeField]
    private GameObject OrderBox;

    [SerializeField]
    private GameObject OrderPreviewBox;

    [SerializeField]
    private GameObject NotificationObject;

    [SerializeField]
    private List<GameObject> CounterSpaces;
    [SerializeField]
    private List<GameObject> DrinkPrefabs;
    private GameObject DrinkPrefab;

    //Code for Handling Finished Drinks and Serving Them
    [SerializeField]
    private int DeCanterEarning, ShotGlassEarning, DoubleRocksEarning, MasonEarning;
    [SerializeField]
    private int LightningEarning, CherryEarning, AppleEarning, HoneyEarning;
    [SerializeField]
    private int ClearEarning, RedEarning, GreenEarning, BrownEarning;
    private int Earnings;
    //checks how many drinks you've finished, and if its enough, unlocks new ones.
    public int CompletedOrders;

    [SerializeField]
    private float BonusTime; //Gives Players More Time for Orders At the Beginning

    [SerializeField]
    private int OrderCombo; //Tracks How Many Orders a player finishes in a row.

    [SerializeField]
    int OrderThreshold1, OrderThreshold2, OrderThreshold3; //Determines when each new set of ingredients can be ordered by customers.

    [SerializeField]
    private TMP_Text OrderComboText;

    [SerializeField]
    private GameObject Tier2, Tier3, Tier4;

    [SerializeField]
    private AudioSource BellSound;

    void Start()
    {
        //OrderUp();
        
    }

    public void BeginOrders()
    {
        InvokeRepeating("OrderUp", 10f, orderDelay);
        unlockDetermined = 1;
    }

    IEnumerator FlowOfOrders()
    {
        while (true)
        {
            yield return new WaitForSeconds(orderDelay);
            OrderPreviews();
        }
    }

    public void OrderPreviews()
    {
        if (Orders.Count < 8 && OrderPreviewBox.transform.childCount < 2)
        {
            GameObject Order = Instantiate(OrderPreview, OrderPreviewBox.transform);
            // Flavor is random,  0=Lightning, 1=Cherry, 2=Apple, 3=Honey
            Order.GetComponent<OrderPreview>().flavor = Random.Range(0, unlockDetermined);
            // color is random,  0=Clear 1=Red 2=Green 4=Brown
            Order.GetComponent<OrderPreview>().color = Random.Range(0, unlockDetermined);
            // glassware is random, 0=ShotGlass, 1=DoubleRocks, 2=MasonJar, 3=Decanter
            Order.GetComponent<OrderPreview>().size = Random.Range(0, unlockDetermined);

            Order.GetComponent<OrderPreview>().SetIcons();

            NotificationObject.GetComponent<Notification>().Activate();

        }
        else
        {

        }
    }



   public void OrderUp()
   {
        if (Orders.Count < 8)
        {
             GameObject Order = Instantiate(OrderPrefab, OrderBox.transform);
             // Flavor is random,  0=Lightning, 1=Cherry, 2=Apple, 3=Honey
             Order.GetComponent<OrderSlot>().flavor = Random.Range(0, unlockDetermined);
             // color is random,  0=Clear 1=Red 2=Green 4=Brown
             Order.GetComponent<OrderSlot>().color = Random.Range(0, unlockDetermined);
             // glassware is random, 0=ShotGlass, 1=DoubleRocks, 2=MasonJar, 3=Decanter
             Order.GetComponent<OrderSlot>().size = Random.Range(0, unlockDetermined);
             Orders.Add(Order);
             Order.GetComponent<OrderSlot>().SetIcons(BonusTime);
            NotificationObject.GetComponent<Notification>().Activate();
            BellSound.Play();
             
             /*
            GameObject Order = Instantiate(OrderPrefab, OrderBox.transform);
            // Flavor is set to whatever was applied,  0=Lightning, 1=Cherry, 2=Apple, 3=Honey
            Order.GetComponent<OrderSlot>().flavor = flavor;
            // color is set to whatever was applied,  0=Clear 1=Red 2=Green 4=Brown
            Order.GetComponent<OrderSlot>().color = color;
            // glassware is set to whatever was applied, 0=ShotGlass, 1=DoubleRocks, 2=MasonJar, 3=Decanter
            Order.GetComponent<OrderSlot>().size = size;
            Orders.Add(Order);
            Order.GetComponent<OrderSlot>().SetIcons();
            //NotificationObject.GetComponent<Notification>().Activate();
            */
        }
        else
        {

        }

        
   }

    public void DeList(GameObject Remove)
    {
        Orders.Remove(Remove);
        Destroy(Remove);
    }

    public void CheckOrder(GameObject FinishedDrink)
    {
        for (int i = 0; i < Orders.Count+1; i++)
        {
            InventorySlot DrinkStats = FinishedDrink.GetComponent<InventorySlot>();
            OrderSlot OrderStats = Orders[i].GetComponent<OrderSlot>();
            //Checks if the Drink being served matches any existing orders
            if ((int)DrinkStats.Coloring == OrderStats.color && (int)DrinkStats.Flavoring == OrderStats.flavor && (int)DrinkStats.GlassType == OrderStats.size)  
            {
                //Debug.Log("Found Match");
                //Gives Money Depending on Glass Size
                switch ((int)DrinkStats.GlassType)
                {
                    case 0: //Shot Glass
                        Earnings += ShotGlassEarning;
                        break;
                    case 1: //Double Glass
                        Earnings += DoubleRocksEarning;
                        break;
                    case 2: //Mason Jar
                        Earnings += MasonEarning;
                        break;
                    case 3: //Decanter
                        Earnings += DeCanterEarning;
                        break;

                }
                //Gives Money Depending on Color Type
                switch ((int)DrinkStats.Coloring)
                {
                    case 0: //Clear
                        Earnings += ClearEarning;
                        break;
                    case 1: //Red
                        Earnings += RedEarning;
                        break;
                    case 2: //Green
                        Earnings += GreenEarning;
                        break;
                    case 3: //Brown
                        Earnings += BrownEarning;
                        break;

                }
                //Gives Money Depending on Flavoring
                switch ((int)DrinkStats.Flavoring)
                {
                    case 0: //Lightning
                        Earnings += LightningEarning;
                        break;
                    case 1: //Cherry
                        Earnings += CherryEarning;
                        break;
                    case 2: //Apple
                        Earnings += AppleEarning;
                        break;
                    case 3: //Honey
                        Earnings += HoneyEarning;
                        break;

                }
                GetComponent<MenuManager>().GainMoney(Earnings); //sends money to player
                //Adds Points to the Player's score!!
                this.gameObject.GetComponent<MenuManager>().GainPoints(Earnings);

                Earnings = 0; //resets money
                FinishedDrink.GetComponent<InventorySlot>().PreviousSlot.GetComponent<Item>().Occupied = false;
                Destroy(FinishedDrink);
                Orders[i].GetComponent<OrderSlot>().OrderCompleted();
                i = Orders.Count + 1;
                CompletedOrders++; //Adds to your complete orders.
                OrderCombo++;
                if (OrderCombo % 5 == 0) gameObject.GetComponent<MenuManager>().GainReputation(5);
                if (OrderCombo % 10 == 0) gameObject.GetComponent<MenuManager>().GainReputation(10);
                OrderComboText.text = "Order Streak: " + OrderCombo + "x";
                if (CompletedOrders == OrderThreshold1)
                {
                    unlockDetermined++; //If you have enough complete orders, unlock new order types
                    Tier2.SetActive(true);
                }
                if (CompletedOrders == OrderThreshold2)
                {
                    unlockDetermined++;
                    Tier3.SetActive(true);
                }

                if (CompletedOrders == OrderThreshold3)
                {
                    unlockDetermined++;
                    Tier4.SetActive(true);
                }



                if (CompletedOrders == 15) BonusTime -= 20;
                if (CompletedOrders == 20) BonusTime -= 20;
                if (CompletedOrders == 25) BonusTime -= 20;

                //Spawns Drinks On each Slot
                for (int slot = 0; slot < CounterSpaces.Count; slot++) //Checks for Space for a drink
                {
                    if (!CounterSpaces[slot].GetComponent<CounterSpace>().Occupied)
                    {
                        int drink = 0;
                        //Shot Glasses
                        if (DrinkStats.GlassType == 0) //Checks all Shot Glasses
                        {
                            
                            switch ((int)DrinkStats.Coloring) //Checks Color
                            {
                                case 0: //Clear
                                    drink = 0;
                                    DrinkPrefab = DrinkPrefabs[drink];
                                    break;
                                case 1: //Red
                                    drink = 1;
                                    DrinkPrefab = DrinkPrefabs[drink];
                                    break;
                                case 2: //Green
                                    drink = 2;
                                    DrinkPrefab = DrinkPrefabs[drink];
                                    break;
                                case 3: //Brown
                                    drink = 3;
                                    DrinkPrefab = DrinkPrefabs[drink];
                                    break;
                            }
                            
                        }
                        //Double Rocks
                        if ((int)DrinkStats.GlassType == 1) //Checks all Double Glasses
                        {

                            switch ((int)DrinkStats.Coloring) //Checks Color
                            {
                                case 0: //Clear
                                    drink = 4;
                                    DrinkPrefab = DrinkPrefabs[drink];
                                    break;
                                case 1: //Red
                                    drink = 5;
                                    DrinkPrefab = DrinkPrefabs[drink];
                                    break;
                                case 2: //Green
                                    drink = 6;
                                    DrinkPrefab = DrinkPrefabs[drink];
                                    break;
                                case 3: //Brown
                                    drink = 7;
                                    DrinkPrefab = DrinkPrefabs[drink];
                                    break;
                            }

                        }
                        //Mason Jar
                        if ((int)DrinkStats.GlassType == 2) //Checks all Mason Jars
                        {

                            switch ((int)DrinkStats.Coloring) //Checks Color
                            {
                                case 0: //Clear
                                    drink = 8;
                                    DrinkPrefab = DrinkPrefabs[drink];
                                    break;
                                case 1: //Red
                                    drink = 9;
                                    DrinkPrefab = DrinkPrefabs[drink];
                                    break;
                                case 2: //Green
                                    drink = 10;
                                    DrinkPrefab = DrinkPrefabs[drink];
                                    break;
                                case 3: //Brown
                                    drink = 11;
                                    DrinkPrefab = DrinkPrefabs[drink];
                                    break;
                            }

                        }
                        //Decanter
                        if ((int)DrinkStats.GlassType == 3) //Checks all Decanters
                        {

                            switch ((int)DrinkStats.Coloring) //Checks Color
                            {
                                case 0: //Clear
                                    drink = 12;
                                    DrinkPrefab = DrinkPrefabs[drink];
                                    break;
                                case 1: //Red
                                    drink = 13;
                                    DrinkPrefab = DrinkPrefabs[drink];
                                    break;
                                case 2: //Green
                                    drink = 14;
                                    DrinkPrefab = DrinkPrefabs[drink];
                                    break;
                                case 3: //Brown
                                    drink = 15;
                                    DrinkPrefab = DrinkPrefabs[drink];
                                    break;
                            }

                        }

                        if (CustomerMove != null)
                        {

                            if (CustomerMove.GetComponent<CustomerMover>().Moving == false) CustomerMove.GetComponent<CustomerMover>().MoveToRV();
                        }

                        //Instantiate(DrinkPrefab, CounterSpaces[slot].transform.position, CounterSpaces[slot].transform.rotation, CounterSpaces[slot].transform);
                        CounterSpaces[slot].GetComponent<CounterSpace>().Occupied = true;
                        slot = CounterSpaces.Count + 1;
                    }
                }
            }
        }
    }

    public void ComboBreaker() //resets the order combo.
    {
        OrderCombo = 0;
        OrderComboText.text = "Order Streak: " + OrderCombo + "x";
    }

    public void GameEnded() //shuts off flow of orders for the player.
    {
        CancelInvoke("OrderUp");
        for (int i = Orders.Count-1; i == 0; i--)
        {
            DeList(Orders[i]);
        }
    }

    public bool DrinkCheck()
    {
        for (int i = 0; i < CounterSpaces.Count; i++)
        {
            if (CounterSpaces[i].GetComponent<CounterSpace>().Occupied)
            {
                return true;
            }
        }
        return false;
    }

    public void ExtractDrink(GameObject Customer)
    {
        for (int i = 0; i < CounterSpaces.Count; i++)
        {
            if (CounterSpaces[i].GetComponent<CounterSpace>().Occupied)
            {
                GameObject CurrentCounter = CounterSpaces[i];
                Transform Drink = CurrentCounter.transform.GetChild(0);
                Drink.SetParent(Customer.transform);
                CurrentCounter.GetComponent<CounterSpace>().Occupied = false;
                CustomerMove.GetComponent<CustomerMover>().Drink = Drink.gameObject;
                i = CounterSpaces.Count + 1;
            }
        }
    }

}
