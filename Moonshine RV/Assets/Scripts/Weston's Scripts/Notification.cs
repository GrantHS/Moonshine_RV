using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Notification : MonoBehaviour
{
    public GameObject Marker,Text;
    public float visibility = 1;
    // Start is called before the first frame update
    

    // Update is called once per frame
    void FixedUpdate()
    {
        visibility = visibility - 0.005f;
        Marker.GetComponent<RawImage>().color = new Color(255f, 255f, 255f, visibility);
        Text.GetComponent<TMP_Text>().color = new Color(0f, 0f, 0f, visibility);
        if (visibility <= 0)
        {
            this.gameObject.SetActive(false);
        }
    }

    public void Activate()
    {
        visibility = 1;
        this.gameObject.SetActive(true);
    }
}

