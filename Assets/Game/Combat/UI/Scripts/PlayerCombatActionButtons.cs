using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class PlayerCombatActionButtons : MonoBehaviour
{
    public Image Icon;
    public Image coolDownImage;
    public TMP_Text amountText;
    [Space]
    public float coolDownCount;
    public float maxCoolDown;

    public ItemStatsBase equipmentReff;
    private ItemStatsWeapon weaponReff;
    public int numberOfUses;
    public bool destoryOnUse;
    public bool isDisabled;

    private CombatActionLogicBase combatActionLogic;
    private GameObject combatVFX;

    public bool playMiniGameFirst = false;
    public int basePower = 0;
    private bool hasMagicCost;
    private int magicCost;


    public void SetUp(ItemStatsBase equipment)
    {
        weaponReff = null;
        isDisabled = false;
        destoryOnUse = false;
        amountText.gameObject.SetActive(false);
        numberOfUses = 0;
        Icon.gameObject.SetActive(true);
        equipmentReff = equipment;
        Icon.sprite = equipment.itemIcon;
        switch (equipment.ItemType)
        {
            case ItemType.Weapon:
                ItemStatsWeapon weaponData = ((ItemStatsWeapon)equipment);

                maxCoolDown = weaponData.useCoolDown;
                combatActionLogic = weaponData.combatLogic;
                combatVFX = weaponData.VFX;
                playMiniGameFirst = true;
             
                //cost logic
                if (weaponData.magiccost != 0)
                {
                    hasMagicCost = true;
                    magicCost = weaponData.magiccost;
                }

                weaponReff = weaponData;

                break;
            case ItemType.UseableItem:
                ItemStatsUsable itemStatsUsable = ((ItemStatsUsable)equipment);
                maxCoolDown = itemStatsUsable.useCoolDown;
                combatActionLogic = itemStatsUsable.combatLogic;
                combatVFX = itemStatsUsable.VFX;
                numberOfUses = itemStatsUsable.numberOfuses;
                destoryOnUse = itemStatsUsable.destoryAfterUse;
                playMiniGameFirst = false;
                basePower = itemStatsUsable.power;
                if(numberOfUses != 0)
                {
                    amountText.text = numberOfUses.ToString();
                    amountText.gameObject.SetActive(true);
                }
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

        if (hasMagicCost)
        {
            if (PlayerController.instance.combatUnit.combatStats.GetCurrentMagic() >= magicCost)
            {
                PlayerController.instance.combatUnit.combatStats.UpdateCurrentMagic(magicCost * -1);
            }
            else
            {
                return;
            }
        }

        coolDownCount = maxCoolDown;

        //this is were you figure out what the action does.
        CombatActionData actionData = new CombatActionData();

        if(equipmentReff.ItemType == ItemType.Weapon)
        {
            Debug.Log("player base damage rolls" + weaponReff.damageRolls.x.ToString() + " , " + weaponReff.damageRolls.y.ToString());
            basePower = Random.Range(weaponReff.damageRolls.x, weaponReff.damageRolls.y);
            Debug.Log("player base damage" + basePower.ToString());
        }
        actionData.power = basePower;

        //here where you would play your mini game if you have one and add to base power

        combatActionLogic.PreformAction(PlayerController.instance.combatUnit, combatVFX, actionData);

        if (numberOfUses > 0)
        {

            numberOfUses--;
            amountText.text = numberOfUses.ToString();
            if (numberOfUses == 0 && destoryOnUse)
            {
                ClearButton();
            }
        }

    }
    public void ClearButton()
    {
        Icon.sprite = null;
        Icon.gameObject.SetActive(false);
        amountText.gameObject.SetActive(false);
        coolDownImage.fillAmount = 0;
        isDisabled = true;
    }
}
