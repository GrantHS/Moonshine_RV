using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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
    private int currency;
    [SerializeField]
    private int cherTreePrice, appTreePrice, honTreePrice, shotGlassPrice, doubleGlassPrice, masonGlassPrice, canterGlassPrice;
    [SerializeField]
    private GameObject CherryButton, AppleButton, HoneyButton;
    //[SerializeField]
    //bool MenusOpen;
    [SerializeField]
    private GameObject LightTree, CherTree, AppTree, HonTree;
    [SerializeField]
    private GameObject LightningFruit, CherryFruit, AppleFruit, HoneyFruit;


    [Header("Reputation Variables")]
    [SerializeField]
    private int Reputation;
    [SerializeField]
    private Text ReputationText;
    [SerializeField]
    private GameObject ReputationScaler;
    private GameObject ReputationBar;

    [Header("End of Game Variables")]
    [SerializeField]
    private int Points;
    [SerializeField]
    private GameObject GameOverScreen;
    [SerializeField]
    private Text ResultsText;

    public int Currency { get => currency; private set => currency = value; }
    public int CherTreePrice { get => cherTreePrice; private set => cherTreePrice = value; }
    public int AppTreePrice { get => appTreePrice; private set => appTreePrice = value; }
    public int HonTreePrice { get => honTreePrice; private set => honTreePrice = value; }
    public int ShotGlassPrice { get => shotGlassPrice; private set => shotGlassPrice = value; }
    public int DoubleGlassPrice { get => doubleGlassPrice; private set => doubleGlassPrice = value; }
    public int MasonGlassPrice { get => masonGlassPrice; private set => masonGlassPrice = value; }
    public int CanterGlassPrice { get => canterGlassPrice; private set => canterGlassPrice = value; }

    private void Start()
    {
        //print(Screen.currentResolution);
        //Screen.SetResolution(900, 600, true);
        if (Screen.currentResolution.width <= 1080)
        {

        } 
    }

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
        ReputationBar = ReputationScaler.transform.GetChild(0).gameObject;
        ReputationFlavor();
        this.GetComponent<OrderMaker>().BeginOrders(); //Starts Orders coming in after the player hits Start Button
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
        }
        //MenusOpen = true;

    }
    public void CherryTree()
    {
        if (CherryHarvestable)
        {
            CherryTreeMenu.SetActive(true);
        }
        //MenusOpen = true;
    }
    public void AppleTree()
    {
        if (AppleHarvestable)
        {
            AppleTreeMenu.SetActive(true);
        }
        //MenusOpen = true;
    }
    public void HoneyTree()
    {
        if (HoneyHarvestable)
        {
            HoneyTreeMenu.SetActive(true);
        }
        //MenusOpen = true;
    }

    public void FruitHarvested(int type)
    {
        switch (type) //Lightning=0,Cherry=1,Apple=2,Honey=3;
        {
            case 0:
                LightningHarvestable = false;
                LightningFruit.SetActive(false);
                break;
            case 1:
                CherryHarvestable = false;
                CherryFruit.SetActive(false);
                break;
            case 2:
                AppleHarvestable = false;
                AppleFruit.SetActive(false);
                break;
            case 3:
                HoneyHarvestable = false;
                HoneyFruit.SetActive(false);
                break;
        }
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

    public void GainPoints(int points)
    {
        Points += points;
    }

    public void GainReputation(int Rep)
    {
        GainPoints(Rep * 5);
        Reputation += Rep;
        if (Reputation > 100) Reputation = 100;
        ReputationText.text = "Reputation: " + Reputation;
        ReputationFlavor();
    }

    public void LoseReputation(int Rep)
    {
        Reputation -= Rep;
        ReputationText.text = "Reputation: " + Reputation;
        ReputationFlavor();
        if (Reputation <= 0) GameOver();
    }

    private void GameOver()
    {
        this.gameObject.GetComponent<OrderMaker>().GameEnded();
        GameOverScreen.SetActive(true);
        ResultsText.text = ("Points Earned: " + Points + "\n" + "Customers Served: " + this.gameObject.GetComponent<OrderMaker>().CompletedOrders);
    }

    public void BacktoMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    private void ReputationFlavor() //Changes the color of the reputation bar to let you know you're in trouble or doing ok.
    {
        float RepPercent = Reputation / 100;
        //ReputationScaler.transform.localScale = new Vector3(RepPercent, 0f, 0f);
        if (Reputation >= 75) ReputationBar.GetComponent<RawImage>().color = new Color(0f, 1f, 0f); //turns green if rep is high
        if (Reputation < 75 && Reputation > 25) ReputationBar.GetComponent<RawImage>().color = new Color(0f, 0f, 1f); //sets it blue is reputation is doing ok
        if (Reputation <= 25) ReputationBar.GetComponent<RawImage>().color = new Color(1f, 0f, 0f); //sets it red if reputaiton is low
    }
    



}
