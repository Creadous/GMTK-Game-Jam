using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class TreasureCard : MenuButtonBase
{
    public Image icon;
    public Image priceIcon;
    public TMP_Text titleText;
    public TMP_Text descriptionText;
    public TMP_Text priceText;
    public int gold;


    public void SetUp(ItemStatsBase itemStatsBase, int goldAmount, bool isShopMode = false)
    {
        icon.sprite = itemStatsBase.itemIcon;
        Debug.Log(gameObject.name + " icon set to: " + (icon.sprite != null ? icon.sprite.name : "NULL"));
        gold = goldAmount;
        if (itemStatsBase.ItemType != ItemType.Money)
        {
            titleText.text = itemStatsBase.itemName;
        }
        else
        {
            titleText.text = itemStatsBase.itemName + " " + gold;
        }
        descriptionText.text = itemStatsBase.itemDescription;

        if (isShopMode)
        {
            priceIcon.gameObject.SetActive(true);
            priceText.gameObject.SetActive(true);
            priceText.text = itemStatsBase.shopPrice.ToString();
        }
        else
        {
            priceText.gameObject.SetActive(false);
        }


    }
}