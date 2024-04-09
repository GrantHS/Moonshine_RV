using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadioController : MonoBehaviour
{
    public GameObject radioPanel;
    public GameObject Country;
    public GameObject HipHop;

    private void Start()
    {
        radioPanel.SetActive(false);    
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit)) //inside of update it checks a raycast of your mouse pointer if it hits a tag called radio
            {
                if (hit.collider.CompareTag("radio"))
                {
                    // if true toggle the visibility of the radio panel
                    radioPanel.SetActive(!radioPanel.activeSelf);
                }
            }
        }
    }

    public void CountryMusic()
    {
        radioPanel.SetActive(false);
        Country.SetActive(true);
    }

    public void HipHopMusic()
    {
        radioPanel.SetActive(false);
        HipHop.SetActive(true);
    }
}
