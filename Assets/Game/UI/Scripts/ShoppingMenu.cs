using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShoppingMenu : MonoBehaviour
{
    private bool finishedWithMenu = false;
    public List<ItemStatsBase> shopItems;
    public List<TreasureCard> treasureCards;
    public ButtonSelectionBase buttonSelection;
    public CantAffordMessage cantAffordMessage;

    public void Awake()
    {
        buttonSelection.SelectionAcceptedCallback.AddListener(() => ShoppingMenuController_SelectionAcceptedCallback());
    }
    public void SetUp(List<ItemStatsBase> items)
    {
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
                treasureCards[i].SetUp(items[i], 0, true); // true = shop mode display
            }
        }
        buttonSelection.BuildButtonList();
    }
    public void Update()
    {
        if (finishedWithMenu) return;
        if (InputManager.instance.cancel)
        {
            InputManager.instance.cancel = false;
            finishedWithMenu = true;
            GameController.instance.CloseShoppingMenu();
            return;
        }
        buttonSelection.HandleButtonCycle(InputManager.instance.move.x * -1);
        buttonSelection.HandleButtonInputs();
    }
    public void ShoppingMenuController_SelectionAcceptedCallback()
    {
        ItemStatsBase chosenItem = shopItems[buttonSelection.selectedIndex];
        if (PlayerController.instance.gold < chosenItem.shopPrice)
        {
            cantAffordMessage.ShowMessage();
            return;
        }
        PlayerController.instance.gold -= chosenItem.shopPrice;
        finishedWithMenu = true;
        GameController.instance.CloseShoppingMenu();
        GameController.instance.OpenInventoryMenu(chosenItem);
    }
}