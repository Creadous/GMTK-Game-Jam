using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Game/Combat/AI/Conditions")]
public class CombatUnitAICondition : ScriptableObject
{
    public enum AIConditionType
    {
        HasTarget,
        NoTarget,
        AttackRange,
        OutSideOfAttackRange,
        IsPlayerAlive,
        IsHealthLow
    }
    public AIConditionType ConditionType;

    public bool CheckConditionStatus(CombatUnitAIController unitAIController)
    {
        bool returnValue = false;
        switch (ConditionType)
        {
            case AIConditionType.HasTarget:
                if(unitAIController.Target != null)
                {
                    returnValue = true;
                }
                break;
            case AIConditionType.NoTarget:
                if (unitAIController.Target == null)
                {
                    returnValue = true;
                }
                break;
            case AIConditionType.AttackRange:
                returnValue = CloseEnoughToAttack(unitAIController);
                break;
            case AIConditionType.OutSideOfAttackRange:
                returnValue = !CloseEnoughToAttack(unitAIController);
                break;
            case AIConditionType.IsPlayerAlive:
                if(PlayerController.instance.combatUnit.combatStats.GetCurrentStamina() != 0)
                {
                    returnValue = true;
                }
                break;
            case AIConditionType.IsHealthLow:
                if(unitAIController.combatUnit.combatStats.GetCurrentStamina() < 8)
                {
                    return true;
                }
                break;
        }
        return returnValue;
    }
    
    private bool CloseEnoughToAttack(CombatUnitAIController unitAIController)
    {
        bool returnValue = false;
        if (unitAIController.IsInAttackRange(unitAIController.combatUnit.crawlerMovment.gridLocation, PlayerController.instance.movment.gridLocation, unitAIController.attackRange))
        {
            returnValue = true;
        }
        Vector3 start = unitAIController.combatUnit.transform.position + Vector3.up; // eye height
        Vector3 target = PlayerController.instance.combatUnit.transform.position + Vector3.up;

        Vector3 direction = target - start;
        float distance = direction.magnitude;

        if(distance > unitAIController.maxSpotDistance)
        {
            returnValue = false;
        }
        return returnValue;
    }
}
