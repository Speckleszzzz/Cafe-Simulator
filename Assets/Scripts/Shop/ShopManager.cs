using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    public int coins;
    public TMP_Text coinUI;
    public ShopItemSO[] shopItemsSO;
    public ShopTemplate[] shopPanels;
    public GameObject[] shopPanelsGO;
    public Button[] myPurchaseButton;
    public Button[] newButtons;
    private bool[] newButtonsClicked;

    void Start()
    {
        for (int i = 0; i < shopItemsSO.Length; i++)
            shopPanelsGO[i].SetActive(true);
        coinUI.text = "Coins : " + coins.ToString();
        LoadPanels();
        CheckPurchaseable();
        EnableNewButtons(); // Enable new buttons initially
        LinkNewButtons(); // Link new buttons to NewButtonClicked method
    }

    void Update()
    {

    }

    public void CheckPurchaseable()
    {
        for (int i = 0; i < shopItemsSO.Length; i++)
        {
            if (coins >= shopItemsSO[i].baseCost)
                myPurchaseButton[i].interactable = true;
            else
                myPurchaseButton[i].interactable = false;
        }
    }

    public void PurchaseItem(int buttonNo)
    {
        if (coins >= shopItemsSO[buttonNo].baseCost)
        {
            coins -= shopItemsSO[buttonNo].baseCost;
            coinUI.text = "Coins : " + coins.ToString();
            CheckPurchaseable();
        }
    }

    public void LoadPanels()
    {
        for (int i = 0; i < shopItemsSO.Length; i++)
        {
            shopPanels[i].titleTxt.text = shopItemsSO[i].title;
            shopPanels[i].descriptionTxt.text = shopItemsSO[i].description;
            shopPanels[i].CoinTxt.text = "Coins : " + shopItemsSO[i].baseCost.ToString();
            shopPanels[i].iconImage.sprite = shopItemsSO[i].Icon;
        }
    }

    public void EnableNewButtons()
    {
        foreach (Button button in newButtons)
        {
            button.interactable = true;
        }
        newButtonsClicked = new bool[newButtons.Length];
    }

    public void NewButtonClicked(int buttonNo)
    {
        if (!newButtonsClicked[buttonNo])
        {
            newButtonsClicked[buttonNo] = true;
            newButtons[buttonNo].gameObject.SetActive(false);
            myPurchaseButton[buttonNo].interactable = true;
            Debug.Log("New button " + buttonNo + " clicked!");
        }
    }

    void LinkNewButtons()
    {
        for (int i = 0; i < newButtons.Length; i++)
        {
            int buttonNo = i; // Need to store the current value of i for each iteration
            newButtons[buttonNo].onClick.AddListener(() => NewButtonClicked(buttonNo));
        }
    }
}
