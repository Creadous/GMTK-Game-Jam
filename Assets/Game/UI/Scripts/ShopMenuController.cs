using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class ShopMenuController : MonoBehaviour
{
    public ButtonSelectionBase buttonSelectionBase;
    public InteractableObject_NPC talkerRef;
    public TMP_Text goldAmount;

    public ShoppingMenu shoppingMenu;
    public ItemSwapMenuController itemSwapController;

    public List<ItemStatsBase> shopItems;

    public enum ShoppingState
    {
        Intro,
        Buying,
        Selling,
        EquipingItem,
        Leaving
    }
    public ShoppingState shoppingState;
    // Start is called before the first frame update
    private void Awake()
    {
        buttonSelectionBase.SelectionAcceptedCallback.AddListener(ShopMenuController_SelectionAcceptedCallback);
    }
    void OnDestroy()
    {
        if (buttonSelectionBase != null)
        {
            buttonSelectionBase.SelectionAcceptedCallback.RemoveListener(ShopMenuController_SelectionAcceptedCallback);
        }
    }
    void Start()
    {
        buttonSelectionBase.BuildButtonList();
    }
    public void SetUp(InteractableObject_NPC talker)
    {
        talkerRef = talker;
    }
    // Update is called once per frame
    void Update()
    {
        if (shoppingState == ShoppingState.Leaving) return;
        goldAmount.text = PlayerController.instance.gold.ToString();
        HandleInput();
    }
    void HandleInput()
    {
        switch (shoppingState)
        {
            case ShoppingState.Intro:
                buttonSelectionBase.HandleButtonCycle(InputManager.instance.move.y);
                buttonSelectionBase.HandleButtonInputs();
                break;
            case ShoppingState.Buying:
                if (InputManager.instance.CanceledInputRequested())
                {
                    shoppingMenu.gameObject.SetActive(false);
                    shoppingState = ShoppingState.Intro;
                    return;
                }

                shoppingMenu.HandleInput();

                if (shoppingMenu.finishedWithMenu)
                {
                    //load itemswapmenu
                    shoppingState = ShoppingState.EquipingItem;
                    itemSwapController.SetUp(shoppingMenu.chosenItem);
                    itemSwapController.gameObject.SetActive(true);
                }
                break;
            case ShoppingState.EquipingItem:
                if (itemSwapController.isFinished)
                {
                    itemSwapController.gameObject.SetActive(false);
                    shoppingMenu.SetUp(shopItems);
                    shoppingMenu.gameObject.SetActive(true);
                    shoppingState = ShoppingState.Buying;
                }
                break;
        }
    }
    private void ShopMenuController_SelectionAcceptedCallback()
    {
        switch (buttonSelectionBase.selectedIndex)
        {
            case 0:
                //buy
                shopItems = MasterItemList.instance.GetRandomItems(3, true);
                shoppingMenu.SetUp(shopItems);
                shoppingMenu.gameObject.SetActive(true);
                shoppingState = ShoppingState.Buying;
                break;
            //case 1:
                //sell
                //break;
            case 1:
                //leave
                shoppingState = ShoppingState.Leaving;
                GameController.instance.CloseShoppingMenu();
                SimpleDialogueSystem.instance.NextNode();// move conversion along
                break;
        }
    }
}
