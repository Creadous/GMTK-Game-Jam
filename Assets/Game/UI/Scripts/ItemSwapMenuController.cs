using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class ItemSwapMenuController : MonoBehaviour
{
    public enum ItemSwapMenuState 
    {
        WaitForPlacement,
        WaitConfirm,
        FinishedWithMenu
    }
    public ItemSwapMenuState menuState;
    public ButtonSelectionBase buttonSelection;
    public List<Image> imageSlots;
    
    public ItemStatsBase newItemReff;

    public TMP_Text newItemNameText;
    public Image newItemIcon;

    [Header("ItemInfoUI")]
    public TMP_Text selectItemText;
    public TMP_Text selectItemDescription;

    [Header("ConfirmMenu")]
    public GameObject confirmMenuPrefab;
    [HideInInspector] public ConfirmHud confirmHud;

    public void Awake()
    {
        buttonSelection.SelectionAcceptedCallback.AddListener(() => ItemSwapMenuController_SelectionAcceptedCallback());
        buttonSelection.SelectionChangedCallback.AddListener(() => ItemSwapMenuController_SelectionChangedCallback());
    }

    

    public void Start()
    {
        buttonSelection.BuildButtonList();
        UpdateSelectedText();
    }

    public void SetUp(ItemStatsBase itemStats) 
    {
        newItemReff = itemStats;
        newItemNameText.text = newItemReff.itemName;
        newItemIcon.sprite = newItemReff.itemIcon;
        UpdateActionButton();
    }
    public void UpdateActionButton()
    {
        int index = 0;
        foreach (Image icon in imageSlots)
        {
            if (PlayerController.instance.combatUnit.combatStats.inventory[index] == null)
            {
                icon.gameObject.SetActive(false);
            }
            else
            {
                icon.gameObject.SetActive(true);
                icon.sprite = PlayerController.instance.combatUnit.combatStats.inventory[index].itemIcon;
            }

            index++;
        }
    }
    public void Update()
    {
        HandleInput();
    }
    public void HandleInput()
    {
        switch (menuState)
        {
            case ItemSwapMenuState.WaitForPlacement:
                buttonSelection.HandleButtonCycle(InputManager.instance.move.x *-1);
                buttonSelection.HandleButtonInputs(); 
                break;
            case ItemSwapMenuState.WaitConfirm:
                if (confirmHud.finishedWithMenu)
                {
                    if (confirmHud.chooseYes)
                    {
                        //replace item;
                        GameObject.DestroyImmediate(confirmHud.gameObject);
                        AddItem();
                        UpdateActionButton();
                        menuState = ItemSwapMenuState.FinishedWithMenu;
                        GameController.instance.CloseInventoryMenu();
                    }
                    else
                    {
                        GameObject.DestroyImmediate(confirmHud.gameObject);
                        menuState = ItemSwapMenuState.WaitForPlacement;
                    }
                }
                break;
        }
    }
    private void ItemSwapMenuController_SelectionAcceptedCallback()
    {
        if (IsSlotFull() == false)
        {
            AddItem();
            UpdateActionButton();
            menuState = ItemSwapMenuState.FinishedWithMenu;
            GameController.instance.CloseInventoryMenu();
        }
        else
        {
            var confirmObj = Instantiate(confirmMenuPrefab, GameController.instance.Canvas);
            confirmHud = confirmObj.GetComponent<ConfirmHud>();
            string oldItem = PlayerController.instance.combatUnit.combatStats.inventory[buttonSelection.selectedIndex].itemName;
            string newItem = newItemReff.itemName;
            confirmHud.SetUp("Do you want to swap " + oldItem + " for " + newItem);
            menuState = ItemSwapMenuState.WaitConfirm;
        }
    }

    private void AddItem()
    {
        PlayerController.instance.combatUnit.combatStats.inventory[buttonSelection.selectedIndex] = newItemReff;
        PlayerCombatActionController.instance.UpdateActionButtons(); // this will foce the buttons to update
    }

    public bool IsSlotFull()
    {
        if (PlayerController.instance.combatUnit.combatStats.inventory[buttonSelection.selectedIndex] != null)
        {
            return true;
        }
        return false;
    }

    private void ItemSwapMenuController_SelectionChangedCallback()
    {
        UpdateSelectedText();
    }

    public void UpdateSelectedText()
    {
        if(PlayerController.instance.combatUnit.combatStats.inventory[buttonSelection.selectedIndex] != null)
        {
            selectItemText.text = PlayerController.instance.combatUnit.combatStats.inventory[buttonSelection.selectedIndex].itemName;
            selectItemDescription.text = PlayerController.instance.combatUnit.combatStats.inventory[buttonSelection.selectedIndex].itemDescription;
        }
        else
        {
            selectItemText.text = "";
            selectItemDescription.text = "";
        }
    }

}