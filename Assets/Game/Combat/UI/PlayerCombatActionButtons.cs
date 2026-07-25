using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerCombatActionButtons : MonoBehaviour
{
    public Image Icon;
    public Image coolDownImage;
    [Space]
    public float coolDownCount;
    public float maxCoolDown;

    public ItemStatsBase equipmentReff;
    public bool isDisabled;

    private CombatActionLogicBase combatActionLogic;
    private GameObject combatVFX;

    public bool playMiniGameFirst = false;
    public int basePower = 0;
    public void SetUp(ItemStatsBase equipment)
    {
        isDisabled = false;
        Icon.gameObject.SetActive(true);
        equipmentReff = equipment;
        Icon.sprite = equipment.itemIcon;
        switch (equipment.ItemType)
        {
            case ItemType.Weapon:
                maxCoolDown = ((ItemStatsWeapon)equipment).useCoolDown;
                combatActionLogic = ((ItemStatsWeapon)equipment).combatLogic;
                combatVFX = ((ItemStatsWeapon)equipment).VFX;
                playMiniGameFirst = true;
                basePower = Random.Range(((ItemStatsWeapon)equipment).damageRolls.x, ((ItemStatsWeapon)equipment).damageRolls.y);
                break;
            case ItemType.UseableItem:
                maxCoolDown = ((ItemStatsUsable)equipment).useCoolDown;
                combatActionLogic = ((ItemStatsUsable)equipment).combatLogic;
                combatVFX = ((ItemStatsUsable)equipment).VFX;
                playMiniGameFirst = false;
                basePower = ((ItemStatsUsable)equipment).power;
                break;
        }
        coolDownCount = 0;
    }

    public void Update()
    {
        if (isDisabled) return;

        coolDownCount -= Time.deltaTime;
        if (coolDownCount < 0) coolDownCount = 0;


        coolDownImage.fillAmount =  coolDownCount / (float) maxCoolDown;
    }
    public void PressButton()
    {
        if (isDisabled) return;
        if (coolDownCount != 0) return;
        coolDownCount = maxCoolDown;

        //this is were you figure out what the action does.
        CombatActionData actionData = new CombatActionData();
        actionData.power = basePower;

        //here where you would play your mini game if you have one and add to base power

        combatActionLogic.PreformAction(PlayerController.instance.combatUnit, combatVFX, actionData);
    }
    public void ClearButton()
    {
        Icon.sprite = null;
        Icon.gameObject.SetActive(false);
        coolDownImage.fillAmount = 0;
        isDisabled = true;
    }
}
