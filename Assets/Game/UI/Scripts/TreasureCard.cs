using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class TreasureCard : MenuButtonBase
{
    public Image icon;
    public TMP_Text descriptionText;
    public int gold;
    
    public void SetUp(ItemStatsBase itemStatsBase,int goldAmount)
    {
        icon.sprite = itemStatsBase.itemIcon;
        gold = goldAmount;
        switch (itemStatsBase.ItemType)
        {
            case ItemType.Money:
                descriptionText.text = goldAmount.ToString();
                break;
            case ItemType.Weapon:
                descriptionText.text = ((ItemStatsWeapon)itemStatsBase).damageRolls.x.ToString() + " - " + ((ItemStatsWeapon)itemStatsBase).damageRolls.y.ToString();
                break;
            case ItemType.UseableItem:
                descriptionText.text =  ((ItemStatsUsable)itemStatsBase).power.ToString();

                break;
        }
        
    }
}
