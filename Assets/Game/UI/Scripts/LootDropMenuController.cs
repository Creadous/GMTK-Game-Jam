using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootDropMenuController : MonoBehaviour
{
    private bool finishedWithMenu = false;
    public List<ItemStatsBase> treasureDrop;
    public Vector2Int goldRange;
    public List<TreasureCard> treasureCards;
    public ButtonSelectionBase buttonSelection;

    public void Awake()
    {
        buttonSelection.SelectionAcceptedCallback.AddListener(() => LootDropMenuController_SelectionAcceptedCallback());
    }
    public void SetUp(List<ItemStatsBase> treasure, Vector2Int gold)
    {
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


    }

    public void Update()
    {
        if (finishedWithMenu) return;
        buttonSelection.HandleButtonCycle(InputManager.instance.move.x * -1);
        buttonSelection.HandleButtonInputs();
    }
    public void LootDropMenuController_SelectionAcceptedCallback()
    {

        if (treasureDrop[buttonSelection.selectedIndex].ItemType == ItemType.Money)
        {
            PlayerController.instance.gold += treasureCards[buttonSelection.selectedIndex].gold;
        }
        else
        {
            //how items are handeled
            //TODO: Figure out how to add/ swap items
        }

        //this is were you add the treasure to character data
        finishedWithMenu = true; //prevents double clicking
        GameController.instance.CloseLootDropMenu();
    }
}
