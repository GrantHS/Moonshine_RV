using System.Collections;
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

    public OrderSlot orderSlot; 
    public Order CurrentOrder { get; private set; }

    // order structure for flavor, color and size
    public struct Order
    {
        public int flavor;
        public int color;
        public int size;
    }

    void Start()
    {
        /*OrderUp();
        InvokeRepeating("FlowOfOrders", orderDelay, orderDelay);*/
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
        Order newOrder = new Order();
        // lavor is random,  0=Lightning, 1=Cherry, 2=Apple, 3=Honey
        newOrder.flavor = Random.Range(0, 4);
        // color is always clear,  0=Clear
        newOrder.color = 0;
        // glassware is random, 0=ShotGlass, 1=DoubleRocks, 2=MasonJar, 3=Decanter
        newOrder.size = Random.Range(0, 4);

        // send the order to the OrderSlot
        if (orderSlot != null)
        {
            orderSlot.ReceiveOrder(newOrder);
        }
    }
}
