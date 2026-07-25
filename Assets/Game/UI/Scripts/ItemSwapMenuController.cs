using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemSwapMenuController : MonoBehaviour
{
    [Header("Slot Displays")]
    public List<Image> slotIcons;       // 4 icons, one per slot, in order
    public List<TMP_Text> slotNames;    // 4 name labels, one per slot, in order

    [Header("Confirmation")]
    public GameObject confirmPanel;
    public TMP_Text confirmText;

    private ItemStatsBase incomingItem;
    private int pendingSlotIndex = -1;
    private bool awaitingConfirmation = false;

    public void SetUp(ItemStatsBase newItem)
    {
        incomingItem = newItem;
        confirmPanel.SetActive(false);
        awaitingConfirmation = false;
        RefreshSlotDisplay();
    }

    private void RefreshSlotDisplay()
    {
        var inventory = PlayerController.instance.combatUnit.combatStats.inventory;
        for (int i = 0; i < slotIcons.Count; i++)
        {
            if (inventory[i] != null)
            {
                slotIcons[i].sprite = inventory[i].itemIcon;
                slotIcons[i].gameObject.SetActive(true);
                slotNames[i].text = inventory[i].itemName;
            }
            else
            {
                slotIcons[i].gameObject.SetActive(false);
                slotNames[i].text = "Empty";
            }
        }
    }

    void Update()
    {

        if (awaitingConfirmation)
        {
            HandleConfirmationInput();
            return;
        }

        if (InputManager.instance.combat01) { InputManager.instance.combat01 = false; TrySelectSlot(0); }
        else if (InputManager.instance.combat02) { InputManager.instance.combat02 = false; TrySelectSlot(1); }
        else if (InputManager.instance.combat03) { InputManager.instance.combat03 = false; TrySelectSlot(2); }
        else if (InputManager.instance.combat04) { InputManager.instance.combat04 = false; TrySelectSlot(3); }
    }

    private void TrySelectSlot(int index)
    {
        if (InputManager.instance.combat01) { InputManager.instance.combat01 = false; Debug.Log("Q pressed in swap menu"); TrySelectSlot(0); }
        var inventory = PlayerController.instance.combatUnit.combatStats.inventory;

        if (inventory[index] == null)
        {
            PlaceItem(index);
        }
        else
        {
            pendingSlotIndex = index;
            awaitingConfirmation = true;
            confirmPanel.SetActive(true);
            confirmText.text = "Swap out " + inventory[index].itemName + " for " + incomingItem.itemName + "? (Q = Yes, X = No)";
        }
    }

    private void HandleConfirmationInput()
    {
        if (InputManager.instance.combat01) // Q = confirm
        {
            InputManager.instance.combat01 = false;
            PlaceItem(pendingSlotIndex);
        }
        else if (InputManager.instance.combat04) // X = cancel
        {
            InputManager.instance.combat04 = false;
            confirmPanel.SetActive(false);
            awaitingConfirmation = false;
            pendingSlotIndex = -1;
        }
    }

    private void PlaceItem(int index)
    {
        PlayerController.instance.combatUnit.combatStats.inventory[index] = incomingItem;
        PlayerCombatActionController.instance.UpdateActionButtons();
        confirmPanel.SetActive(false);
        awaitingConfirmation = false;
        GameController.instance.CloseItemSwapMenu();
    }
}