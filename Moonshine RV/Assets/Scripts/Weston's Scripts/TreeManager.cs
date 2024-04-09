using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeManager : MonoBehaviour
{
    [Header("Tree Harvesting Variables")]
    [SerializeField]
    private float LightningTiming = 20f;
    [SerializeField]
    [Tooltip("Sets the amount of lightning strikes until the fruit is ready")]
    private int CherryStrikes, AppleStrikes, HoneyStrikes;
    //Amount of strikes remaining until fruit is ready
    private int CherryRemain, AppleRemain, HoneyRemain;
    [SerializeField]
    private GameObject LightningFruit, CherryFruit, AppleFruit, HoneyFruit;
    [SerializeField]
    private GameObject LightningTree, CherryTree, AppleTree, HoneyTree;


    [Header("Sound Effects")]
    [SerializeField] private AudioClip lightningSoundEffect; //lightning sound
    public AudioSource audioSource; //lightning tree



    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("TreeIntervalActive", 0f, LightningTiming);
        CherryRemain = CherryStrikes;
        AppleRemain = AppleStrikes;
        HoneyRemain = HoneyStrikes;
    }

    private void TreeIntervalActive()
    {
        if (!LightningFruit.activeInHierarchy)
        {
            LightningFruit.SetActive(true);
            PlayLightningSound();
            this.GetComponent<MenuManager>().LightningGrown();
        }
        if (!CherryFruit.activeInHierarchy && CherryRemain <= 0 && CherryTree.activeInHierarchy)
        {
            CherryRemain = CherryStrikes;
            CherryFruit.SetActive(true);
            this.GetComponent<MenuManager>().CherryGrown();
        }
        else if (CherryRemain > 0 && !CherryFruit.activeInHierarchy && CherryTree.activeInHierarchy)  
        {
            CherryRemain--;
        }
        if (!AppleFruit.activeInHierarchy && AppleRemain <= 0 && AppleTree.activeInHierarchy)
        {
            AppleRemain = AppleStrikes;
            AppleFruit.SetActive(true);
            this.GetComponent<MenuManager>().AppleGrown();
        }
        else if (AppleRemain > 0 && !AppleFruit.activeInHierarchy && AppleTree.activeInHierarchy) 
        {
            AppleRemain--;
        }

        if (!HoneyFruit.activeInHierarchy && HoneyRemain <= 0 && HoneyTree.activeInHierarchy)
        {
            HoneyRemain = HoneyStrikes;
            HoneyFruit.SetActive(true);
            this.GetComponent<MenuManager>().HoneyGrown();
        }
        else if (HoneyRemain > 0 && !HoneyFruit.activeInHierarchy && HoneyTree.activeInHierarchy)  
        {
            HoneyRemain--;
        }


    }
    private void PlayLightningSound()
    {
        if (audioSource != null && lightningSoundEffect != null)
        {
            audioSource.PlayOneShot(lightningSoundEffect); // Play the sound effect
        }
    }

}
