using UnityEngine;
[RequireComponent(typeof(UnityEngine.AI.NavMeshAgent))]
public class Customer : MonoBehaviour
{
    public CustomerSpawner Spawner { get; set; } // reference to the spawner

    private OrderMaker orderMaker;
    public OrderMaker.Order CurrentOrder { get; private set; }

    private UnityEngine.AI.NavMeshAgent agent;

    private void Start()
    {
        orderMaker = FindObjectOfType<OrderMaker>();
        RequestOrder();
    }

    // order request triggered automatically when the customer is spawned
    public void RequestOrder()
    {
        if (orderMaker != null)
        {
            orderMaker.OrderUp();
            CurrentOrder = orderMaker.CurrentOrder; // store the current order for this customer
        }
    }

    // leave is called when the order is done 
    public void Leave()
    {
        Destroy(gameObject); // Destroy the customer GameObject

        // creates a new customer after a delay
        if (Spawner != null)
        {
            Spawner.CustomerLeft();
        }
    }
}
