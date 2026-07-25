using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Game/Combat/logic/TileAction")]
public class CombatActionLogicTileAction : CombatActionLogicBase
{
    public enum TileActionSpawnLocationType
    {
        Self,
        Forward
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
                    /*
                    Vector3 CenterOffset = new Vector3(2.5f, 0, 2.5f);
                    vfxObject.transform.position = attacker.crawlerMovment.GetFacingWorldPosition();
                    vfxObject.transform.position += CenterOffset;
                    Vector3 attackDirection = new Vector3(
                        attacker.crawlerMovment.facingDirection.x,
                        0,
                        attacker.crawlerMovment.facingDirection.y
                    );

                    vfxObject.transform.forward = attackDirection;
                    */

                    var combatActionController = vfxObject.GetComponent<CombatActionCollider>();
                    combatActionController.power = data.power;
                    combatActionController.PlayAction();
                    /*
                    var vfxObject = Instantiate(VFX,attacker.gameObject.transform);
                    //vfxObject.transform.position += attacker.transform.forward * 4.5f;// new Vector3(0, 0, 4.5f); //should put it in the next tile

                    Vector3 attackDirection = new Vector3(attacker.crawlerMovment.facingDirection.x,0,attacker.crawlerMovment.facingDirection.y);
                    vfxObject.transform.position += attackDirection * 4.5f;
                    vfxObject.transform.forward = attackDirection;

                    var combatActionController = vfxObject.GetComponent<CombatActionCollider>();
                    combatActionController.power = data.power;
                    combatActionController.PlayAction(); //play animation

                    //logic is handle when it hits
                    */
                }
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
