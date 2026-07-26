using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShoppingMenu : MonoBehaviour
{
    public bool IsSellingItems; //turn this switch on and you can now sell items
    public bool finishedWithMenu = false;
    public List<ItemStatsBase> shopItems;
    public List<TreasureCard> treasureCards;
    public ButtonSelectionBase buttonSelection;
    public CantAffordMessage cantAffordMessage;

    public ItemStatsBase chosenItem;

    [Header("ItemInfoUI")]
    public TMP_Text selectItemText;
    public TMP_Text selectItemDescription;
    public TMP_Text goldAmountText;

    public void Awake()
    {
        buttonSelection.SelectionAcceptedCallback.AddListener(() => ShoppingMenuController_SelectionAcceptedCallback());
        buttonSelection.SelectionChangedCallback.AddListener(() => ShoppingMenuController_SelectionChangedCallback());
    }
    public void SetUp(List<ItemStatsBase> items)
    {
        finishedWithMenu = false;
        shopItems = new List<ItemStatsBase>();
        for (int i = 0; i < items.Count; i++)
        {
            shopItems.Add(items[i]);
        }
        for (int i = 0; i < items.Count; i++)
        {
            if (i < treasureCards.Count)
            {
                treasureCards[i].gameObject.SetActive(true);
                treasureCards[i].SetUp(items[i], 0); 
            }
        }
        buttonSelection.BuildButtonList();
        UpdateSelectedText();
    }
    public void HandleInput()
    {
        buttonSelection.HandleButtonCycle(InputManager.instance.move.x * -1);
        buttonSelection.HandleButtonInputs();

    }
    public void ShoppingMenuController_SelectionAcceptedCallback()
    {
        chosenItem = shopItems[buttonSelection.selectedIndex];
        if (PlayerController.instance.gold < chosenItem.gold)
        {
            cantAffordMessage.ShowMessage();
            return;
        }

        PlayerController.instance.gold -= chosenItem.gold;
        finishedWithMenu = true;
    }
    #region item info
    private void ShoppingMenuController_SelectionChangedCallback()
    {
        UpdateSelectedText();
    }
    public void UpdateSelectedText()
    {
        if (IsSellingItems)
        {
            //settingmenu
            if (PlayerController.instance.combatUnit.combatStats.inventory[buttonSelection.selectedIndex] != null)
            {
                selectItemText.text = PlayerController.instance.combatUnit.combatStats.inventory[buttonSelection.selectedIndex].itemName;
                selectItemDescription.text = PlayerController.instance.combatUnit.combatStats.inventory[buttonSelection.selectedIndex].itemDescription;
                goldAmountText.text = PlayerController.instance.combatUnit.combatStats.inventory[buttonSelection.selectedIndex].gold.ToString();
            }
            else
            {
                selectItemText.text = "";
                selectItemDescription.text = "";
            }
        }
        else
        {
            //buying menu
            selectItemText.text = shopItems[buttonSelection.selectedIndex].itemName;
            selectItemDescription.text = shopItems[buttonSelection.selectedIndex].itemDescription;
            goldAmountText.text = shopItems[buttonSelection.selectedIndex].gold.ToString();
        }
    }
    #endregion
}