using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    UseableItem,
    Weapon,
    Money
}

[CreateAssetMenu(menuName = "Game/Item/base")]
public class ItemStatsBase : ScriptableObject
{
    public string itemID;
    public string itemName;
    public string itemDescription;
    public ItemType ItemType;
    public Sprite itemIcon;
    public int gold;
}
