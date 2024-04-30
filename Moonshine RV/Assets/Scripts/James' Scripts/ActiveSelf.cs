using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveSelf : MonoBehaviour
{
    public GameObject character;

    private void Start()
    {
        InvokeRepeating("activateSelf", 7f, 3f);
    }

    void activateSelf()
    {
        character.SetActive(!gameObject.activeSelf);
    }
    
}
