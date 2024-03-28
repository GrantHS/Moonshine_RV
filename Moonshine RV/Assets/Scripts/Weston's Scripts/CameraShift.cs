using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShift : MonoBehaviour
{
    [SerializeField]
    GameObject MainAreaButtons, StillButtons, BackyardButtons, OrderWindowButtons, ComputerButtons;
    [SerializeField]
    private Transform ComputerPoint, StillPoint, MainAreaPoint, OrderWindowPoint, BackyardPoint;


    public Transform p0, p1;
    public Transform r0, r1;
    public float timeDuration = 1f;
    public float timeStart;
    public bool CameraMoving = false;

    public Vector3 p01;
    public Quaternion r01;

    [SerializeField]
    private GameObject CameraObject;
    [SerializeField]
    private GameObject OrderWindowSlot;
    [SerializeField]
    private GameObject OrderWindowPreviews;

    // Start is called before the first frame update
    void Start()
    {
        p0 = MainAreaPoint;
    }


    public void GoToComputer()
    {
        p0 = ComputerPoint;
        MainAreaButtons.SetActive(false);
        StillButtons.SetActive(false);
        BackyardButtons.SetActive(false);
        OrderWindowButtons.SetActive(false);
        ComputerButtons.SetActive(true);
    }

    public void GoToStill()
    {
        p0 = StillPoint;
        MainAreaButtons.SetActive(false);
        StillButtons.SetActive(true);
        BackyardButtons.SetActive(false);
        OrderWindowButtons.SetActive(false);
        ComputerButtons.SetActive(false);
        this.GetComponent<Inventory>().StillScreen.SetActive(true);
        this.GetComponent<Inventory>().StillWindow = true;
        this.GetComponent<Inventory>().StatusPage.SetActive(true);
    }

    public void GoToStart()
    {
        p0 = MainAreaPoint;
        MainAreaButtons.SetActive(true);
        StillButtons.SetActive(false);
        BackyardButtons.SetActive(false);
        OrderWindowButtons.SetActive(false);
        ComputerButtons.SetActive(false);
    }

    public void GoToOrder()
    {
        p0 = OrderWindowPoint;
        MainAreaButtons.SetActive(false);
        StillButtons.SetActive(false);
        BackyardButtons.SetActive(false);
        OrderWindowButtons.SetActive(true);
        ComputerButtons.SetActive(false);
        OrderWindowSlot.SetActive(true);
        OrderWindowPreviews.SetActive(true);
    }

    public void GoToBackyard()
    {
        p0 = BackyardPoint;
        MainAreaButtons.SetActive(false);
        StillButtons.SetActive(false);
        BackyardButtons.SetActive(true);
        OrderWindowButtons.SetActive(false);
        ComputerButtons.SetActive(false);
    }

    public void Still()
    {
        p1 = StillPoint;
        timeStart = Time.time;
        CameraMoving = true;
    }
    public void Computer()
    {
        p1 = ComputerPoint;
        timeStart = Time.time;
        CameraMoving = true;
    }
    public void Backyard()
    {
        p1 = BackyardPoint;
        timeStart = Time.time;
        CameraMoving = true;
    }
    public void OrderWindow()
    {
        p1 = OrderWindowPoint;
        timeStart = Time.time;
        CameraMoving = true;
    }
    public void MainArea()
    {
        p1 = MainAreaPoint;
        timeStart = Time.time;
        CameraMoving = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (CameraMoving)
        {
            this.GetComponent<Inventory>().StillWindow = false;
            this.GetComponent<Inventory>().StillScreen.SetActive(false);
            this.GetComponent<Inventory>().StatusPage.SetActive(false);
            OrderWindowSlot.SetActive(false);
            OrderWindowPreviews.SetActive(false);
            ComputerButtons.SetActive(false);
            float u = (Time.time - timeStart) / timeDuration;
            if (u >= 1)
            {
                u = 1;
                CameraMoving = false;
                if (p1 == StillPoint)
                {
                    GoToStill();
                }
                else if (p1 == ComputerPoint)
                {
                    GoToComputer();
                }
                else if (p1 == BackyardPoint)
                {
                    GoToBackyard();
                }
                else if (p1 == OrderWindowPoint)
                {
                    GoToOrder();
                }
                else if (p1 == MainAreaPoint)
                {
                    GoToStart();
                }
                
            }


            p01 = (1 - u) * p0.position + u * p1.position;
            r01 = Quaternion.Slerp(p0.rotation, p1.rotation, u);

            CameraObject.transform.rotation = r01;
            CameraObject.transform.position = p01;
        }

        

    }
}
