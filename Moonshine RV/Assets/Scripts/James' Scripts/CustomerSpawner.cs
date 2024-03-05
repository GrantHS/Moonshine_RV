using UnityEngine;

public class CustomerSpawner : MonoBehaviour
{
    public GameObject customerPrefab;
    public Transform spawnPoint;
    public Transform windowPoint;     
    public float spawnDelay = 7f;

    private void Start()
    {
        SpawnCustomer();
    }

    public void SpawnCustomer()
    {
        GameObject customerObj = Instantiate(customerPrefab, spawnPoint.position, Quaternion.identity);
        CustomerMovement customerMovement = customerObj.GetComponent<CustomerMovement>();
        Customer customer = customerObj.GetComponent<Customer>();
        if (customer != null)
        {
            customerMovement.SetDestination(windowPoint.position);
            customer.Spawner = this; 
            customer.RequestOrder(); // request an order when starting the game
        }
    }

    public void CustomerLeft()
    {
        Invoke(nameof(SpawnCustomer), spawnDelay); // when customers destroys spawn a new customer with a delay
    }
}
