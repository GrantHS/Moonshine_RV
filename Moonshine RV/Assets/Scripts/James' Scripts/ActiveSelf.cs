using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveSelf : MonoBehaviour
{
    public GameObject character;

    private void Start()
    {
        InvokeRepeating("activateSelf", 1f, 7f);
    }

    void activateSelf()
    {
        character.SetActive(!gameObject.activeSelf);
    }
    
}
