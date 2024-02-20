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


    // Start is called before the first frame update
    void Start()
    {
        OrderUp();
        InvokeRepeating("FlowOfOrders",orderDelay,orderDelay);


    }


    IEnumerator FlowOfOrders()
    {
        yield return new WaitForSeconds(orderDelay);
        OrderUp();
    }


    void OrderUp()
    {
        if (!CherryTree || !MasonJar)
        {
            //0=LightClearShot 1=LightClearDouble
            Random.Range(0, 2);
        }
        else if (!AppleTree || !Decanter)
        {
            //Flavor 0=Lightning 1=Cherry
            Random.Range(0, 2);
            //Color 0=Clear 1=Red
            Random.Range(0, 2);
            //Size 0=ShotGlass 1= DoubleRocks 2=MasonJar

            //0=LightClearShot 1=LightClearDouble 2=LightClearMason 3=LightRedShot 4=LightRedDouble 5=LightRedMason 
            //6 =CherryClearShot 7=CherryClearDouble 8=CherryClearMason 9=CherryRedShot 10=CherryRedDouble 11=CherryRedMason
            Random.Range(0, 3);
        }
        else if (!HoneyTree)
        {
            //Flavor 0=Lightning 1=Cherry 2=Apple
            Random.Range(0, 3);
            //Color 0=Clear 1=Red 2=Green
            Random.Range(0, 3);
            //Size 0=ShotGlass 1= DoubleRocks 2=MasonJar 3=Decanter
            Random.Range(0, 4);
        }
        else
        {
            //Flavor 0=Lightning 1=Cherry 2=Apple 3=Honey
            Random.Range(0, 4);
            //Color 0=Clear 1=Red 2=Green 3=Brown
            Random.Range(0, 4);
            //Size 0=ShotGlass 1= DoubleRocks 2=MasonJar 3=Decanter
            Random.Range(0, 4);
        }



    }
}
