using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class TreasureCard : MenuButtonBase
{
    public Image icon;
    public TMP_Text titleText;
    public TMP_Text descriptionText;
    public int gold;
    
    public void SetUp(ItemStatsBase itemStatsBase,int goldAmount)
    {
        icon.sprite = itemStatsBase.itemIcon;
        gold = goldAmount;
        if(itemStatsBase.ItemType != ItemType.Money)
        {
            titleText.text = itemStatsBase.itemName;
        }
        else
        {
            titleText.text = itemStatsBase.itemName + " " + gold;
        }
        descriptionText.text = itemStatsBase.itemDescription;
    }
}
