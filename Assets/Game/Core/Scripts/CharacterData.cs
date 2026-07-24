using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterData : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField] private int currentStamina;
    public int totalStamina;
    [SerializeField] private int currentMagic;
    public int totalMagic;

    public int gold;

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
    #endregion

    public void ResetStats()
    {
        currentStamina = totalStamina;
        currentMagic = totalMagic;
        gold = 0;

    }
}
