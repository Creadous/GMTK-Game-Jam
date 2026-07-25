using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Game/Combat/logic/TileAction")]
public class CombatActionLogicTileAction : CombatActionLogicBase
{
    public enum TileActionSpawnLocationType
    {
        Self,
        Forward,
        ForwardBullet
    }
    public TileActionSpawnLocationType tileActionSpawnLocationType;

    public enum TitleActionType
    {
        Damage,
        HealStamina,
        HealMagic
    }
    public TitleActionType titleActionType;
    public override void PreformAction(CombatUnit attacker, GameObject VFX, CombatActionData data)
    {
        switch (tileActionSpawnLocationType)
        {
            case TileActionSpawnLocationType.Forward:
                {
                    var vfxObject = Instantiate(VFX, attacker.gameObject.transform);
                    vfxObject.transform.position += attacker.transform.forward * 4.5f;

                    var combatActionController = vfxObject.GetComponent<CombatActionCollider>();
                    combatActionController.power = data.power;
                    combatActionController.PlayAction();
                    
                }
                break;
            case TileActionSpawnLocationType.ForwardBullet:
                {
                    var vfxObject = Instantiate(VFX, attacker.gameObject.transform);
                    vfxObject.transform.parent = null; //deattach from parent and be free 
                    vfxObject.transform.position += attacker.transform.forward * 4.5f;

                    var combatActionController = vfxObject.GetComponent<CombatActionCollider>();
                    combatActionController.power = data.power;
                    combatActionController.PlayAction();

                }
                break;
                break;
            case TileActionSpawnLocationType.Self:
                {
                    var vfxObject = Instantiate(VFX, attacker.transform);
                    var combatActionController = vfxObject.GetComponent<CombatActionCollider>();
                    combatActionController.disableOnHit = true; // all the logic will be handle here
                    combatActionController.PlayAction(); //play animation

                    switch (titleActionType)
                    {
                        case TitleActionType.HealStamina:
                            attacker.combatStats.UpdateCurrentStamina(data.power);
                            break;
                        case TitleActionType.HealMagic:
                            attacker.combatStats.UpdateCurrentMagic(data.power);
                            break;
                    }
                }
                break;
        }
    }
    public void HitSomething(CombatUnit attacker, CombatUnit target)
    {
        
    }
}
