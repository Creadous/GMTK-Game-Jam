using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterData : MonoBehaviour
{
    [Header("Stats")]
    public CombatStats combatStats;
    public int gold;

    public void ResetStats()
    {
        combatStats.SetCurrentStamina(combatStats.totalStamina);
        combatStats.SetCurrentMagic(combatStats.totalMagic);
        gold = 0;

    }
}
