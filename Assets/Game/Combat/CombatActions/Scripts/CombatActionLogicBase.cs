using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatActionLogicBase : ScriptableObject
{
    public virtual void PreformAction(CombatUnit Attacker, GameObject VFX, CombatActionData data)
    {
        //this is ment to be overwritten
    }
}
