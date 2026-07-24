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
    }
    // Update is called once per frame
    void Update()
    {
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
