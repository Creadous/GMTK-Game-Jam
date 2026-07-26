using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Game/Item/Useable")]
public class ItemStatsUsable : ItemStatsBase
{
    public CombatActionLogicBase combatLogic;
    public float useCoolDown;
    public int power;
    public int magicCost; // future you can have healht poition cost magic every time you drink
    public bool oneTimeUse;
    public GameObject VFX;
}
