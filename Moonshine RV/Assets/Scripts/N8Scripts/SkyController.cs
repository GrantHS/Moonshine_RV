using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class SkyController : MonoBehaviour
{
    private Material SkyBox;
    public float Speed;
    private float tFactor;
    public bool isNight;

    public float DayValue;
    public float NightValue;
    public float changeSpeed;

    public float Timer = 0f;
    private float timeToChange = 600f; 
  
    
    private void Start()
    {
        SkyBox = RenderSettings.skybox;

        
    }


   
    private void Update()
    {
        SkyBox.SetFloat("_Rotation", SkyBox.GetFloat("_Rotation") + Time.deltaTime * Speed);


        Timer += Time.deltaTime;

        if (Timer >= timeToChange)
        {
            Timer = 0f;
            isNight = !isNight;
        }

        if (isNight == true && tFactor >= NightValue)
        {
            tFactor -= Time.deltaTime * changeSpeed;
           
            SkyBox.SetColor("_Tint",new Color(tFactor, tFactor, tFactor, 1));

        }

        else if (isNight == false && tFactor <= DayValue)
        {
            tFactor += Time.deltaTime * changeSpeed;
            SkyBox.SetColor("_Tint",new Color(tFactor, tFactor, tFactor, 1));

           

        }

        

    }

  

   

}
