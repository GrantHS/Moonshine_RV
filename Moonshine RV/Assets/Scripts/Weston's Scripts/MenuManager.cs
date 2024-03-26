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
    private Text TreeShopMoneyText,GlassShopMoneyText;

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
    [SerializeField]
    private GameObject CherryButton, AppleButton, HoneyButton;
    //[SerializeField]
    //bool MenusOpen;
    [SerializeField]
    private GameObject LightTree, CherTree, AppTree, HonTree;
    [SerializeField]
    private GameObject LightningFruit, CherryFruit, AppleFruit, HoneyFruit;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
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
        this.GetComponent<CameraShift>().MainArea();
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
        if (LightningHarvestable)
        {
            LightningTreeMenu.SetActive(true);
            LightningHarvestable = false;
            LightningFruit.SetActive(false);
        }
        //MenusOpen = true;

    }
    public void CherryTree()
    {
        if (CherryHarvestable)
        {
            CherryTreeMenu.SetActive(true);
            CherryHarvestable = false;
            CherryFruit.SetActive(false);
        }
        //MenusOpen = true;
    }
    public void AppleTree()
    {
        if (AppleHarvestable)
        {
            AppleTreeMenu.SetActive(true);
            AppleHarvestable = false;
            AppleFruit.SetActive(false);
        }
        //MenusOpen = true;
    }
    public void HoneyTree()
    {
        if (HoneyHarvestable)
        {
            HoneyTreeMenu.SetActive(true);
            HoneyHarvestable = false;
            HoneyFruit.SetActive(false);
        }
        //MenusOpen = true;
    }

    public void HideHarvest()
    {
        LightningTreeMenu.SetActive(false);
        CherryTreeMenu.SetActive(false);
        AppleTreeMenu.SetActive(false);
        HoneyTreeMenu.SetActive(false);
        //MenusOpen = false;
    }


    //Computer Buttons

    public void GlassMenu()
    {
        GlassComputerMenu.SetActive(true);
        GlassMenuButton.SetActive(false);
        TreeMenuButton.SetActive(false);
        //MenusOpen = true;
    }

    public void TreeMenu()
    {
        TreeComputerMenu.SetActive(true);
        TreeMenuButton.SetActive(false);
        GlassMenuButton.SetActive(false);
        //MenusOpen = true;
    }

    public void CloseComputerMenus()
    {
        GlassComputerMenu.SetActive(false);
        TreeComputerMenu.SetActive(false);
        GlassMenuButton.SetActive(true);
        TreeMenuButton.SetActive(true);
        //MenusOpen = false;
    }

    //Checks if the player's money count is enough and lowers it while unlocking the Cherry Tree
    public void BuyCherryTree()
    {
        if (Currency >= CherTreePrice)
        {
            Currency -= CherTreePrice;
            CherTree.SetActive(true);
            MoneyText.text = "Money: " + Currency + "$";
            TreeShopMoneyText.text = "Money: " + Currency + "$";
            GlassShopMoneyText.text = "Money: " + Currency + "$";
            CherryButton.GetComponent<Button>().interactable = false;
        }
    }

    //Checks if the player's money count is enough and lowers it while unlocking the Apple Tree
    public void BuyAppleTree()
    {
        if (Currency >= AppTreePrice)
        {
            Currency -= AppTreePrice;
            AppTree.SetActive(true);
            MoneyText.text = "Money: " + Currency + "$";
            TreeShopMoneyText.text = "Money: " + Currency + "$";
            GlassShopMoneyText.text = "Money: " + Currency + "$";
            AppleButton.GetComponent<Button>().interactable = false;
        }
    }

    //Checks if the player's money count is enough and lowers it while unlocking the Honey Tree
    public void BuyHoneyTree()
    {
        if (Currency >= HonTreePrice)
        {
            Currency -= HonTreePrice;
            HonTree.SetActive(true);
            MoneyText.text = "Money: " + Currency + "$";
            TreeShopMoneyText.text = "Money: " + Currency + "$";
            GlassShopMoneyText.text = "Money: " + Currency + "$";
            HoneyButton.GetComponent<Button>().interactable = false;
        }
    }
    //Checks if the player has enough money for the shot glass and takes it from them.
    public void BuyShotGlass(GameObject Item)
    {
        if (Currency >= ShotGlassPrice)
        {
            Currency -= ShotGlassPrice;
            MoneyText.text = "Money: " + Currency + "$";
            GlassShopMoneyText.text = "Money: " + Currency + "$";
            TreeShopMoneyText.text = "Money: " + Currency + "$";
            gameObject.GetComponent<Inventory>().getButtonItem(Item);
        }
    }

    public void BuyDoubleGlass(GameObject Item)
    {
        if (Currency >= DoubleGlassPrice)
        {
            Currency -= DoubleGlassPrice;
            MoneyText.text = "Money: " + Currency + "$";
            GlassShopMoneyText.text = "Money: " + Currency + "$";
            TreeShopMoneyText.text = "Money: " + Currency + "$";
            gameObject.GetComponent<Inventory>().getButtonItem(Item);
        }
    }

    public void BuyMasonJar(GameObject Item)
    {
        if (Currency >= MasonGlassPrice)
        {
            Currency -= MasonGlassPrice;
            MoneyText.text = "Money: " + Currency + "$";
            GlassShopMoneyText.text = "Money: " + Currency + "$";
            TreeShopMoneyText.text = "Money: " + Currency + "$";
            gameObject.GetComponent<Inventory>().getButtonItem(Item);
        }
    }

    public void BuyDecanter(GameObject Item)
    {
        if (Currency >= CanterGlassPrice)
        {
            Currency -= CanterGlassPrice;
            MoneyText.text = "Money: " + Currency + "$";
            GlassShopMoneyText.text = "Money: " + Currency + "$";
            TreeShopMoneyText.text = "Money: " + Currency + "$";
            gameObject.GetComponent<Inventory>().getButtonItem(Item);
        }
    }



    public void LightningGrown()
    {
        LightningHarvestable = true;
    }
    public void CherryGrown()
    {
        CherryHarvestable = true;
    }
    public void AppleGrown()
    {
        AppleHarvestable = true;
    }
    public void HoneyGrown()
    {
        HoneyHarvestable = true;
    }


    public void GainMoney(int Money)
    {
        Currency += Money;
        MoneyText.text = "Money: " + Currency + "$";
        GlassShopMoneyText.text = "Money: " + Currency + "$";
        TreeShopMoneyText.text = "Money: " + Currency + "$";
    }

}
