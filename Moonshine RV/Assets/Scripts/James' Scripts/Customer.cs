using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class Customer : MonoBehaviour
{
    public CustomerSpawner Spawner { get; set; }

    private OrderMaker orderMaker;
    public OrderMaker.Order CurrentOrder { get; private set; }

    private NavMeshAgent agent;
    private bool hasRequestedOrder = false; // Flag to prevent multiple orders

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        orderMaker = FindObjectOfType<OrderMaker>();
    }

    private void Update()
    {
        // Check if customer has reached the window point and then requests an order, only once
        if (!hasRequestedOrder && Vector3.Distance(transform.position, Spawner.windowPoint.position) < 1.3f)
        {
            RequestOrder();
            hasRequestedOrder = true; // Ensure RequestOrder is called only once
        }
    }

    public void RequestOrder()
    {
        if (orderMaker != null)
        {
            orderMaker.OrderUp();
            CurrentOrder = orderMaker.CurrentOrder;
        }
    }

    public void Leave()
    {
        Destroy(gameObject);
        if (Spawner != null)
        {
            Spawner.CustomerLeft();
        }
    }
}
