using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Game/Item/Weapon")]
public class ItemStatsWeapon : ItemStatsBase
{
    public CombatActionLogicBase combatLogic;
    public GameObject VFX;
    public Vector2Int damageRolls;
    public int magiccost;
    public float useCoolDown;
}
