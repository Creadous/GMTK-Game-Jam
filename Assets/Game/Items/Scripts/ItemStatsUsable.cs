using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Game/Item/Useable")]
public class ItemStatsUsable : ItemStatsBase
{
    public float useCoolDown;
    public int amountOfUsesBeforeDestoryed;
    public GameObject actionVFX;
}
