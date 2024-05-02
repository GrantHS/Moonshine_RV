using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerMover : MonoBehaviour
{
    [SerializeField]
    private Transform p0, p1;
    [SerializeField]
    private Vector3 p01;
    [SerializeField]
    private Animator CustomerAnimation;
    [SerializeField]
    private float timeDuration;
    private float timeStart;
    [SerializeField]
    public bool Moving;
    [SerializeField]
    private Transform RVPoint, SpawnPoint, ExitPoint;
    [SerializeField]
    private GameObject CustomerObject;
    [SerializeField]
    private GameObject OrderObject;
    public GameObject Drink;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Moving)
        {
            float u = (Time.time - timeStart) / timeDuration;
            if (u >= 1)
            {
                u = 1;
                
                if (p1 == RVPoint)
                {
                    StartCoroutine("WaitingTime");
                    GiveDrink();
                }
                if (p0 == RVPoint)
                {
                    HideCustomer();
                }
                Moving = false;
            }
            p01 = (1 - u) * p0.position + u * p1.position;
            CustomerObject.transform.position = p01;
        }
    }


    public void MoveToRV()
    {
        CustomerObject.SetActive(true);
        p0 = SpawnPoint;
        p1 = RVPoint;
        timeDuration = 2f;
        timeStart = Time.time;
        CustomerAnimation.SetBool("HasDrink", false);
        Moving = true;
    }

    public void DrinkRecieved()
    {
        CustomerAnimation.SetBool("HasDrink", true);
    }

    public void MovetoExit()
    {
        p0 = RVPoint;
        p1 = ExitPoint;
        timeDuration = 3f;
        timeStart = Time.time;
        Moving = true;
        
    }

    IEnumerator WaitingTime()
    {
        yield return new WaitForSeconds(2f);
        DrinkRecieved();
        MovetoExit();
    }

    private void HideCustomer()
    {
        CustomerObject.SetActive(false);
        CustomerObject.transform.position = SpawnPoint.position;
        if (Drink != null) Destroy(Drink);
        if (OrderObject.GetComponent<OrderMaker>().DrinkCheck())
        {
            MoveToRV();
        }
    }

    private void GiveDrink()
    {
        if (OrderObject != null)
        {
            OrderObject.GetComponent<OrderMaker>().ExtractDrink(CustomerObject);
        }

        
    }



}
