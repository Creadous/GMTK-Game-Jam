using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CombatStats
{
    public string name;
    [SerializeField] private int currentStamina;
    public int totalStamina;
    [SerializeField] private int currentMagic;
    public int totalMagic;

    public Vector2Int attackRange;
    public int defence;

    public List<ItemStatsBase> inventory;

    #region Get Sets
    public int GetCurrentStamina()
    {
        return currentStamina;
    }
    public void UpdateCurrentStamina(int amount)
    {
        currentStamina += amount;
        if (currentStamina < 0) currentStamina = 0;
        if (currentStamina > totalStamina) currentStamina = totalStamina;
    }
    public void SetCurrentStamina(int amount)
    {
        currentStamina = amount;
    }
    public int GetCurrentMagic()
    {
        return currentMagic;
    }
    public void UpdateCurrentMagic(int amount)
    {
        currentMagic += amount;
        if (currentMagic < 0) currentMagic = 0;
        if (currentMagic > totalMagic) currentMagic = totalMagic;
    }
    public void SetCurrentMagic(int amount)
    {
        currentMagic = amount;
    }
    #endregion

}
