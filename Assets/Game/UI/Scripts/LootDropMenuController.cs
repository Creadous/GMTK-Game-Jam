using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class LootDropMenuController : MonoBehaviour
{
    private bool finishedWithMenu = false;
    public List<ItemStatsBase> treasureDrop;
    public Vector2Int goldRange;
    public List<TreasureCard> treasureCards;
    public ButtonSelectionBase buttonSelection;

    public TMP_Text selectItemText;
    public TMP_Text selectItemDescription;

    public GameObject menuHud;
    public ItemSwapMenuController itemSwapMenu;

    public enum LootDropState
    {
        Idle,
        ItemSwap
    }
    public LootDropState dropState;

    public void Awake()
    {
        buttonSelection.SelectionAcceptedCallback.AddListener(() => LootDropMenuController_SelectionAcceptedCallback());
        buttonSelection.SelectionChangedCallback.AddListener(() => LootDropMenuController_SelectionChangedCallback());
    }

    public void SetUp(List<ItemStatsBase> treasure, Vector2Int gold)
    {
        dropState = LootDropState.Idle;
        goldRange = gold;

        treasureDrop = new List<ItemStatsBase>();
        for(int i = 0; i< treasure.Count; i++)
        {
            treasureDrop.Add(treasure[i]);
        }

        for (int i = 0; i < treasure.Count; i++)
        {
            if(i < treasureCards.Count) //this is only here to stop  a future bug
            {
                treasureCards[i].gameObject.SetActive(true);
                if(treasure[i].ItemType != ItemType.Money)
                {
                    treasureCards[i].SetUp(treasure[i], 0);
                }
                else
                {
                    var goldAmount = Random.Range(gold.x, gold.y);
                    treasureCards[i].SetUp(treasure[i], goldAmount);
                }
            }
        }

        buttonSelection.BuildButtonList();
        UpdateSelectedText();
    }

    public void Update()
    {
        if (finishedWithMenu) return;
        switch (dropState)
        {
            case LootDropState.Idle:
                buttonSelection.HandleButtonCycle(InputManager.instance.move.x * -1);
                buttonSelection.HandleButtonInputs();
                break;
            case LootDropState.ItemSwap:
                if (itemSwapMenu.isFinished)
                {
                    GameController.instance.CloseLootDropMenu();
                }
                break;
        }

    }
    public void LootDropMenuController_SelectionAcceptedCallback()
    {
        if (treasureDrop[buttonSelection.selectedIndex].ItemType == ItemType.Money)
        {
            PlayerController.instance.gold += treasureCards[buttonSelection.selectedIndex].gold;
            finishedWithMenu = true;
            GameController.instance.CloseLootDropMenu();
        }
        else
        {
            menuHud.SetActive(false); // turn off item select
            ItemStatsBase chosenItem = treasureDrop[buttonSelection.selectedIndex];
            itemSwapMenu.SetUp(chosenItem);
            itemSwapMenu.gameObject.SetActive(true);
            dropState = LootDropState.ItemSwap;
        }
    }
    private void LootDropMenuController_SelectionChangedCallback()
    {
        UpdateSelectedText();
    }
    public void UpdateSelectedText()
    {
        selectItemText.text = treasureDrop[buttonSelection.selectedIndex].itemName;
        selectItemDescription.text = treasureDrop[buttonSelection.selectedIndex].itemDescription;
    }
}
