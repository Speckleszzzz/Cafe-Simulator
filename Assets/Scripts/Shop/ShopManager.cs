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
    
    
    void Start()
    {
        for (int i = 0; i < shopItemsSO.Length; i++)
            shopPanelsGO[i].SetActive(true);
        coinUI.text = "Coins : " + coins.ToString();
        LoadPanels();
        CheckPurchaseable();
    }
    void Update()
    {
        
    }

    public void CheckPurchaseable()
    {
        for (int i= 0; i < shopItemsSO.Length; i++)
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
            coins = coins - shopItemsSO[buttonNo].baseCost;
            coinUI.text = "Coins : " + coins.ToString();
            CheckPurchaseable();
        }
    }

    public void LoadPanels() {
        for (int i=0; i < shopItemsSO.Length; i++)
        {
            shopPanels[i].titleTxt.text = shopItemsSO[i].title;
            shopPanels[i].descriptionTxt.text = shopItemsSO[i].description;
            shopPanels[i].CoinTxt.text = "Coins : " + shopItemsSO[i].baseCost.ToString();
            //shopPanels[i].iconImage.sprite = shopItemsSO[i].image;
        }
    }
}
