using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHudController : MonoBehaviour
{
    public FillBar staminaBar;
    public FillBar mpBar;

    public void FixedUpdate()
    {
        UpdateHud();
    }

    public void UpdateHud()
    {
        staminaBar.UpdateFillBar(GameController.instance.characterData.GetCurrentStamina(), GameController.instance.characterData.totalStamina);
        mpBar.UpdateFillBar(GameController.instance.characterData.GetCurrentMagic(), GameController.instance.characterData.totalMagic);
    }
}
