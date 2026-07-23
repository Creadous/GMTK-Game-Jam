using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public enum SlotUIType
{
    ItemNormal,
    ItemCombat,
    ItemShopBuying,
    SkillCombat,
    EquipmentItem,
    EquipmentWeapon,
    EquipmentArmor,
    EquipmentAccessory,
    Character,
    CharacerRace,
    CharacterJob,
    CharacterPartyStatus,
    Crafting,
    ItemShopSelling,
    Quest,
    EquipmentEnchantment,
    SkillData,
    LimitSkillCombat
}

public class MenuSlot : MenuButtonBase
{
    [Header("MenuSlot")]
    public Image icon;
    public TMP_Text slotName;
    public TMP_Text extraInfo;

    public LayoutElement layoutElement;

    protected object reffenceObject;

    public void SetUp(object reffenceItem, SlotUIType slotUIType, int minHeight)
    {
        layoutElement.minHeight = minHeight;
        //SetUp(reffenceItem, slotUIType);
    }

    /*
    private SkillData refrenceSkill;
    private Item refrenceItem;
    private CharacterData refrenceCharacterData;
    public SlotUIType uIType;

    private void Start()
    {
        //if (iconBackground != null) iconBackground.color = ColorConstantManager.instance.colorConstant.primarySecondOffsetColor;
    }

    public void SetUp(object reffenceItem, SlotUIType slotUIType, int minHeight)
    {
        layoutElement.minHeight = minHeight;
        SetUp(reffenceItem, slotUIType);
    }

    public virtual void SetUp(object reffenceObject, SlotUIType slotUIType)
    {
        this.reffenceObject = reffenceObject;
        uIType = slotUIType;
        switch (uIType)
        {
            case SlotUIType.SkillCombat:
                refrenceSkill = (SkillData)reffenceObject;
                slotName.text = refrenceSkill.GetName();// + " lv " + refrenceSkill.GetLevel();
                icon.sprite = refrenceSkill.skill.icon;
                extraInfo.text = "AP: " + refrenceSkill.GetAPCost().ToString();
                break;
            case SlotUIType.ItemCombat:
                refrenceItem = (Item)reffenceObject;
                slotName.text = refrenceItem.itemStats.itemName;
                icon.sprite = refrenceItem.itemStats.itemIcon;
                extraInfo.text = "" + refrenceItem.amountInStack;
                break;
            case SlotUIType.LimitSkillCombat:
                refrenceSkill = (SkillData)reffenceObject;
                slotName.text = refrenceSkill.GetName();// + " lv " + refrenceSkill.GetLevel();
                icon.sprite = refrenceSkill.skill.icon;
                extraInfo.text = "";
                break;
        }
        /*
        switch (uIType)
        {
            
            case SlotUIType.ItemNormal:
                if(reffenceObject != null)
                {
                    refrenceItem = (Item)reffenceObject;
                    //refrenceItemBaseStats = ((ItemStatsUseable)refrenceItem.itemStatsBase);
                    icon.gameObject.SetActive(true);
                    icon.sprite = refrenceItem.itemStatsBase.itemIcon;
                    slotName.text = refrenceItem.GetItemName();
                    if (refrenceItem.itemStatsBase.stackable)
                    {
                        extraInfo.text = "x" + refrenceItem.amountInStack;
                    }
                    else
                    {
                        extraInfo.text = "";
                    }
                }
                else
                {
                    icon.gameObject.SetActive(false);
                    slotName.text = "Remove";
                    extraInfo.text = "";
                }
                break;
            case SlotUIType.ItemShopBuying:
                refrenceItem = (Item)reffenceObject;
                icon.sprite = refrenceItem.itemStatsBase.itemIcon;
                slotName.text = refrenceItem.GetItemName();
                extraInfo.text = refrenceItem.GetBuyingPrice().ToString() + " gp";
                break;
            case SlotUIType.ItemShopSelling:
                refrenceItem = (Item)reffenceObject;
                icon.sprite = refrenceItem.itemStatsBase.itemIcon;
                slotName.text = refrenceItem.GetItemName();
                extraInfo.text = refrenceItem.GetSellingPrice().ToString() + " gp";
                break;
            case SlotUIType.ItemCombat:
                refrenceItem = (Item)reffenceObject;
                refrenceItemBaseStats = ((ItemStatsUseable)refrenceItem.itemStatsBase);
                if (icon != null) icon.sprite = refrenceItemBaseStats.itemIcon;
                slotName.text = refrenceItem.GetItemName();// refrenceItemBaseStats.itemName;
                extraInfo.text = "x" + refrenceItem.amountInStack;
                break;
            case SlotUIType.SkillCombat:
                if(reffenceObject != null)
                {
                    refrenceSkill = (SkillData)reffenceObject;
                    slotName.text = refrenceSkill.GetName() + " lv " + refrenceSkill.GetLevel();
                    extraInfo.text = "MP: " + refrenceSkill.GetMPCost().ToString();
                    if (refrenceSkill.UseHPInsteadOfMP())
                    {
                        extraInfo.text = "HP: " + refrenceSkill.GetMPCost().ToString();
                    }
                }
                else
                {
                    //ghost menu
                    slotName.text = "";
                    extraInfo.text = "";
                }

                break;
            case SlotUIType.SkillData:
                refrenceSkill = (SkillData)reffenceObject;
                slotName.text = refrenceSkill.GetName() + " lv " + refrenceSkill.GetLevel();
                extraInfo.text = "";
                break;
            case SlotUIType.Crafting:
                refrenceCraftingRecipe = (CraftingRecipes)reffenceObject;
                slotName.text = refrenceCraftingRecipe.craftingResult.itemStatsBase.itemName;
                extraInfo.text = "";
                icon.sprite = refrenceCraftingRecipe.craftingResult.itemStatsBase.itemIcon;
                break;
            case SlotUIType.EquipmentWeapon:
            case SlotUIType.EquipmentArmor:
            case SlotUIType.EquipmentAccessory:
            case SlotUIType.EquipmentEnchantment:
            {
                    refrenceItem = null;

                    if (reffenceObject == null || ((Item)reffenceObject).itemStatsBase == null)
                    {
                       
                        icon.sprite = null;
                        icon.gameObject.SetActive(false);
                        slotName.text = "";
                        extraInfo.text = "";
                    }
                    else
                    {
                        //need to create item you can see
                        icon.gameObject.SetActive(true);
                        refrenceItem = (Item)reffenceObject;
                        icon.sprite = refrenceItem.itemStatsBase.itemIcon;
                        slotName.text = refrenceItem.GetItemName();
                        extraInfo.text = "";
                    }
                }
                break;
            case SlotUIType.EquipmentItem:
                //done sperectaly because it being used by forge menu and I might need to change this down the line
                refrenceItem = null;

                if (reffenceObject == null || ((Item)reffenceObject).itemStatsBase == null)
                {

                    icon.sprite = null;
                    icon.gameObject.SetActive(false);
                    slotName.text = "";
                    extraInfo.text = "";
                }
                else
                {
                    //need to create item you can see
                    icon.gameObject.SetActive(true);
                    refrenceItem = (Item)reffenceObject;
                    icon.sprite = refrenceItem.itemStatsBase.itemIcon;
                    slotName.text = refrenceItem.GetItemName();
                    extraInfo.text = "";
                }
                break;
            case SlotUIType.Character:
                refrenceCharacterData = (CharacterData)reffenceObject;
                slotName.text = refrenceCharacterData.combatStats.characterName;
                extraInfo.text = "Lv " + refrenceCharacterData.level;
                break;
            case SlotUIType.CharacterPartyStatus:
                refrenceCharacterData = (CharacterData)reffenceObject;
                icon.sprite = refrenceCharacterData.GetSmallCharacterImage();
                slotName.text = refrenceCharacterData.combatStats.characterName;
                if (refrenceCharacterData.inParty)
                {
                    slotName.text += " - party";
                }
                extraInfo.text = "Lv " + refrenceCharacterData.level;
                break;
            case SlotUIType.Quest:
                refreanceQuestData = (QuestData)reffenceObject;
                slotName.text = refreanceQuestData.QuestName;
                extraInfo.text = "";
                break;

        }
        */
    //}
    /*
    public void SetUpLocation(LocationNode locationNode)
    {
        refrenceLocationNode = locationNode;
        slotName.text = locationNode.Name;
        extraInfo.text = "";
    }
    public object GetReffenceObject()
    {
        return reffenceObject;
    }
    public Item GetReffenceItem()
    {
        return refrenceItem;
    }
*/
}
