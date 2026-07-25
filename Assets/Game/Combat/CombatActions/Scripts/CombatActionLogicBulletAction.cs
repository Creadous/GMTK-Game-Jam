using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Game/Combat/logic/BulletAction")]
public class CombatActionLogicBulletAction : CombatActionLogicBase
{
    public override void PreformAction(CombatUnit attacker, GameObject VFX, CombatActionData actionData)
    {
        //this will create a bullet then fire it off
    }
}
