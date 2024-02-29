using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    [SerializeField]
    private GameObject MainMenu;
    [SerializeField]
    private GameObject OptionsMenuObject;
    [SerializeField]
    private GameObject CreditsMenu;

    bool Tutorial = true;
    [SerializeField]
    private Text TutorialText;
    [SerializeField]
    private Text MoneyText;

    [Header("Shop Menus")]
    [SerializeField]
    private bool LightningHarvestable, CherryHarvestable, AppleHarvestable, HoneyHarvestable;
    [SerializeField]
    private GameObject LightningTreeMenu, CherryTreeMenu, AppleTreeMenu, HoneyTreeMenu;
    [SerializeField]
    private GameObject GlassComputerMenu;
    [SerializeField]
    private GameObject TreeComputerMenu;
    [SerializeField]
    private GameObject MainComputerMenu;
    [SerializeField]
    private GameObject GlassMenuButton;
    [SerializeField]
    private GameObject TreeMenuButton;

    private int Currency;

    bool MenusOpen;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && !MenusOpen) 
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if(Physics.Raycast(ray,out hit, 1000))
            {
                if (hit.collider.tag == "LightningTree")
                {
                    LightningTree();
                }
                if (hit.collider.tag == "CherryTree")
                {
                    CherryTree();
                }
                if (hit.collider.tag == "AppleTree")
                {
                    AppleTree();
                }
                if (hit.collider.tag == "HoneyTree")
                {
                    HoneyTree();
                }
            }
        }
    }

    //Main Menu Buttons

    public void StartGame()
    {
        MainMenu.SetActive(false);
        this.GetComponent<Inventory>().StatusEnabled = true;
    }

    public void OptionsMenu()
    {
        MainMenu.SetActive(false);
        OptionsMenuObject.SetActive(true);
    }

    public void Credits()
    {
        MainMenu.SetActive(false);
        CreditsMenu.SetActive(true);
    }



    public void QuitGame()
    {
        Application.Quit();
    }

    //Options Menu Buttons


    public void TutorialToggle()
    {
        if (Tutorial) Tutorial = false;
        else Tutorial = true;
        if (!Tutorial) TutorialText.text = "TUTORIAL: OFF";
        else TutorialText.text = "TUTORIAL: ON";
    }

    public void BacktoMenu()
    {
        OptionsMenuObject.SetActive(false);
        CreditsMenu.SetActive(false);
        MainMenu.SetActive(true);
    }

    //Tree Menu Buttons
    public void LightningTree()
    {
        if (LightningHarvestable && !MenusOpen) LightningTreeMenu.SetActive(true);
        MenusOpen = true;
    }
    public void CherryTree()
    {
        if (CherryHarvestable && !MenusOpen) CherryTreeMenu.SetActive(true);
        MenusOpen = true;
    }
    public void AppleTree()
    {
        if (AppleHarvestable && !MenusOpen) AppleTreeMenu.SetActive(true);
        MenusOpen = true;
    }
    public void HoneyTree()
    {
        if (HoneyHarvestable && !MenusOpen) HoneyTreeMenu.SetActive(true);
        MenusOpen = true;
    }

    public void HideHarvest()
    {
        LightningTreeMenu.SetActive(false);
        CherryTreeMenu.SetActive(false);
        AppleTreeMenu.SetActive(false);
        HoneyTreeMenu.SetActive(false);
        MenusOpen = false;
    }


    //Computer Buttons

    public void GlassMenu()
    {
        GlassComputerMenu.SetActive(true);
        GlassMenuButton.SetActive(false);
        TreeMenuButton.SetActive(false);
        MenusOpen = true;
    }

    public void TreeMenu()
    {
        TreeComputerMenu.SetActive(false);
        TreeMenuButton.SetActive(false);
        GlassMenuButton.SetActive(false);
        MenusOpen = true;
    }

    public void CloseComputerMenus()
    {
        GlassComputerMenu.SetActive(false);
        TreeComputerMenu.SetActive(false);
        GlassMenuButton.SetActive(true);
        TreeMenuButton.SetActive(true);
        MenusOpen = false;
    }


}
