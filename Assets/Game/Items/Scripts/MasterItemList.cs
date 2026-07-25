using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MasterItemList : MonoBehaviour
{
    public static MasterItemList instance;
    public Dictionary<string, ItemStatsBase> ItemLookUpTable;
    [SerializeField] private List<ItemStatsBase> normalItems;
    [SerializeField] private List<ItemStatsUsable> UseableItem;
    [SerializeField] private List<ItemStatsWeapon> Weapons;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            InitMasterList();
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    public void InitMasterList()
    {
        ItemLookUpTable = new Dictionary<string, ItemStatsBase>();
        //dictionary items
        AddToListToDictionary(normalItems);
        AddToListToDictionary(UseableItem);
        AddToListToDictionary(Weapons);
    }

    public List<ItemStatsBase> GetRandomItems(int count)
    {
        List<ItemStatsBase> allItems = new List<ItemStatsBase>(ItemLookUpTable.Values);
        allItems.RemoveAll(item => item.itemID == "fist01");

        List<ItemStatsBase> result = new List<ItemStatsBase>();

        for (int i = 0; i < count; i++)
        {
            int randomIndex = Random.Range(0, allItems.Count);
            result.Add(allItems[randomIndex]);
            allItems.RemoveAt(randomIndex);
        }

        return result;
    }
    private void AddToListToDictionary(List<ItemStatsBase> ItemsList)
    {
        for (int i = 0; i < ItemsList.Count; i++)
        {
            ItemLookUpTable[ItemsList[i].itemID] = ItemsList[i];
        }
    }
    private void AddToListToDictionary(List<ItemStatsUsable> ItemsList)
    {
        for (int i = 0; i < ItemsList.Count; i++)
        {
            ItemLookUpTable[ItemsList[i].itemID] = ItemsList[i];
        }
    }
    private void AddToListToDictionary(List<ItemStatsWeapon> ItemsList)
    {
        for (int i = 0; i < ItemsList.Count; i++)
        {
            ItemLookUpTable[ItemsList[i].itemID] = ItemsList[i];
        }
    }


}
