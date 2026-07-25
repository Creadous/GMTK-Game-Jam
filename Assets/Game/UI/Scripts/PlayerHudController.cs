using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class PlayerHudController : MonoBehaviour
{
    public FillBar staminaBar;
    public FillBar mpBar;
    public TMP_Text goldText;
    public void FixedUpdate()
    {
        UpdateHud();
    }

    public void UpdateHud()
    {
        goldText.text = PlayerController.instance.gold.ToString();
        staminaBar.UpdateFillBar(PlayerController.instance.combatUnit.combatStats.GetCurrentStamina(), PlayerController.instance.combatUnit.combatStats.totalStamina);
        mpBar.UpdateFillBar(PlayerController.instance.combatUnit.combatStats.GetCurrentMagic(), PlayerController.instance.combatUnit.combatStats.totalMagic);
    }
}
