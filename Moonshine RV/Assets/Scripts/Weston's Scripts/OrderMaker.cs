using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderMaker : MonoBehaviour
{
    [SerializeField]
    private float orderDelay;

    [Header("Player Unlocks")]
    [SerializeField]
    private bool CherryTree;
    [SerializeField]
    private bool AppleTree;
    [SerializeField]
    private bool HoneyTree;
    [SerializeField]
    private bool MasonJar;
    [SerializeField]
    private bool Decanter;

    
    //public Order CurrentOrder { get; private set; }
    [SerializeField]
    int unlockDetermined = 1;
    [SerializeField]
    public List<GameObject> Orders;

    [SerializeField]
    private List<Texture> DrinkImages;

    [SerializeField]
    private GameObject OrderPrefab;

    [SerializeField]
    private GameObject OrderBox;

    [SerializeField]
    private GameObject NotificationObject;



    //Code for Handling Finished Drinks and Serving Them
    [SerializeField]
    private int DeCanterEarning, ShotGlassEarning, DoubleRocksEarning, MasonEarning;
    [SerializeField]
    private int LightningEarning, CherryEarning, AppleEarning, HoneyEarning;
    [SerializeField]
    private int ClearEarning, RedEarning, GreenEarning, BrownEarning;
    private int Earnings;



    // order structure for flavor, color and size
    //public struct Order
    //{
    //    public int flavor;
    //    public int color;
    //    public int size;
    //}

    void Start()
    {
        //OrderUp();
        InvokeRepeating("OrderUp", 10f, orderDelay);
        unlockDetermined = 1;
    }

    IEnumerator FlowOfOrders()
    {
        while (true)
        {
            yield return new WaitForSeconds(orderDelay);
            OrderUp();
        }
    }

   public void OrderUp()
   {
        if (Orders.Count < 8)
        {
            GameObject Order = Instantiate(OrderPrefab, OrderBox.transform);
            // Flavor is random,  0=Lightning, 1=Cherry, 2=Apple, 3=Honey
            Order.GetComponent<OrderSlot>().flavor = Random.Range(0, unlockDetermined);
            // color is always clear,  0=Clear 1=Red 2=Green 4=Brown
            Order.GetComponent<OrderSlot>().color = Random.Range(0, unlockDetermined);
            // glassware is random, 0=ShotGlass, 1=DoubleRocks, 2=MasonJar, 3=Decanter
            Order.GetComponent<OrderSlot>().size = Random.Range(0, 4);
            Orders.Add(Order);
            Order.GetComponent<OrderSlot>().SetIcons();

            NotificationObject.GetComponent<Notification>().Activate();

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
                Earnings = 0; //resets money
                FinishedDrink.GetComponent<InventorySlot>().PreviousSlot.GetComponent<Item>().Occupied = false;
                Destroy(FinishedDrink);
                Orders[i].GetComponent<OrderSlot>().OrderCompleted();
                i = Orders.Count + 1;
            }


        }


    }



}
