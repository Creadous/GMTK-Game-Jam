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
        goldText.text = GameController.instance.characterData.gold.ToString();
        staminaBar.UpdateFillBar(GameController.instance.characterData.combatStats.GetCurrentStamina(), GameController.instance.characterData.combatStats.totalStamina);
        mpBar.UpdateFillBar(GameController.instance.characterData.combatStats.GetCurrentMagic(), GameController.instance.characterData.combatStats.totalMagic);
    }
}
