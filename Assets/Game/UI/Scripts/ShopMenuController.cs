using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopMenuController : MonoBehaviour
{
    public ButtonSelectionBase buttonSelectionBase;
    public InteractableObject_NPC talkerRef;
    // Start is called before the first frame update
    private void Awake()
    {
        buttonSelectionBase.SelectionAcceptedCallback.AddListener(() => ShopMenuController_SelectionAcceptedCallback());
    }

    

    void Start()
    {
        buttonSelectionBase.BuildButtonList();
    }
    public void SetUp(InteractableObject_NPC talker)
    {
        talkerRef = talker;
        GameController.UnPauseGame();
    }
    // Update is called once per frame
    void Update()
    {
        Debug.Log("ShopMenuController - GameController.instance null? " + (GameController.instance == null));
        if (GameController.IsGamePaused()) return;
        HandleInput();
    }
    void HandleInput()
    {
        buttonSelectionBase.HandleButtonCycle(InputManager.instance.move.y);
        buttonSelectionBase.HandleButtonInputs();
    }
    private void ShopMenuController_SelectionAcceptedCallback()
    {
        switch (buttonSelectionBase.selectedIndex)
        {
            case 0:
                //buy
                var shopItems = MasterItemList.instance.GetRandomItems(3, true);
                GameController.instance.OpenShoppingMenu(shopItems);
                break;
            case 1:
                //sell
                break;
            case 2:
                //leave
                GameController.instance.CloseShopMenu();
                break;
        }
    }
}
