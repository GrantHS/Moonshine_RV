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
    [SerializeField]
    private Text ShopMoneyText;

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
    [SerializeField]
    private int Currency;
    [SerializeField]
    private int CherTreePrice, AppTreePrice, HonTreePrice, ShotGlassPrice, DoubleGlassPrice, MasonGlassPrice, CanterGlassPrice;

    bool MenusOpen;
    [SerializeField]
    private GameObject LightTree, CherTree, AppTree, HonTree;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && !MenusOpen)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 1000))
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
        TreeComputerMenu.SetActive(true);
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

    public void BuyCherryTree()
    {
        if (Currency >= CherTreePrice)
        {
            Currency -= CherTreePrice;
            CherTree.SetActive(true);
            MoneyText.text = "Money: " + Currency + "$";
            ShopMoneyText.text = "Money: " + Currency + "$";
        }
    }

    public void BuyAppleTree()
    {
        if (Currency >= AppTreePrice)
        {
            Currency -= AppTreePrice;
            AppTree.SetActive(true);
            MoneyText.text = "Money: " + Currency + "$";
            ShopMoneyText.text = "Money: " + Currency + "$";
        }
    }

    public void BuyHoneyTree()
    {
        if (Currency >= HonTreePrice)
        {
            Currency -= HonTreePrice;
            HonTree.SetActive(true);
            MoneyText.text = "Money: " + Currency + "$";
            ShopMoneyText.text = "Money: " + Currency + "$";
        }
    }

    public void BuyShotGlass()
    {
        if (Currency >= ShotGlassPrice)
        {
            Currency -= ShotGlassPrice;
            MoneyText.text = "Money: " + Currency + "$";
            ShopMoneyText.text = "Money: " + Currency + "$";
        }
    }

    public void BuyDoubleGlass()
    {
        if (Currency >= DoubleGlassPrice)
        {
            Currency -= DoubleGlassPrice;
            MoneyText.text = "Money: " + Currency + "$";
            ShopMoneyText.text = "Money: " + Currency + "$";
        }
    }

    public void BuyMasonJar()
    {
        if (Currency >= MasonGlassPrice)
        {
            Currency -= MasonGlassPrice;
            MoneyText.text = "Money: " + Currency + "$";
            ShopMoneyText.text = "Money: " + Currency + "$";
        }
    }

    public void BuyDecanter()
    {
        if (Currency >= CanterGlassPrice)
        {
            Currency -= CanterGlassPrice;
            MoneyText.text = "Money: " + Currency + "$";
            ShopMoneyText.text = "Money: " + Currency + "$";
        }
    }

}
