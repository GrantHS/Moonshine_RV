using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddOn : MonoBehaviour
{
    //Copy and paste the class below into the MenuManager sript to add the funcionality of the "How To Play" button in the start menu

    //start of DO NOT COPY (Already in MenuManager.cs)
    [SerializeField]
    private GameObject MainMenu;
    [SerializeField]
    private GameObject OptionsMenuObject;
    [SerializeField]
    private GameObject CreditsMenu;
    //End of DO NOT COPY

    [SerializeField]
    private GameObject howToMenu;

    //Assign this function to the "How To Play" button
    public void HowToMenu()
    {
        MainMenu.SetActive(false);
        howToMenu.SetActive(true);
    }

    public void BacktoMenu()
    {
        OptionsMenuObject.SetActive(false);
        CreditsMenu.SetActive(false);
        howToMenu.SetActive(false); //Add this line to the BacktoMenu() function that is already present in MenuManager.cs
        MainMenu.SetActive(true);
    }
}
