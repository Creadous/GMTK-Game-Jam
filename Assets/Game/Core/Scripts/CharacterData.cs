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
    }
    public int GetCurrentMagic()
    {
        return currentMagic;
    }
    public void UpdateCurrentMagic(int amount)
    {
        currentMagic += amount;
    }
    #endregion
}
