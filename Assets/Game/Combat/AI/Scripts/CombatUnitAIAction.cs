using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Game/Combat/AI/Actions")]
public class CombatUnitAIAction : ScriptableObject
{
    public enum AIActionType
    {
        Wait,
        Move,
        Attack,
        LookForPlayer
    }
    public AIActionType ActionType;
    public LayerMask layerMask;

    public IEnumerator PreformAction(CombatUnitAIController unitAIController)
    {
        switch (ActionType)
        {
            case AIActionType.Wait:
                //no code needed
                Debug.Log(unitAIController.combatUnit.combatStats.name + " waiting");
                yield return new WaitForSeconds(1);
                break;
            case AIActionType.Move:
                Debug.Log(unitAIController.combatUnit.combatStats.name + " moving");
                yield return MoveTo(unitAIController);
                yield return new WaitForSeconds(1);

                break;
            case AIActionType.Attack:
                Debug.Log(unitAIController.combatUnit.combatStats.name + " attacking");
                yield return Attack(unitAIController);

                break;
            case AIActionType.LookForPlayer:
                yield return LookForPlayer(unitAIController);
                break;
        }
        yield return null;
    }
    public IEnumerator Attack(CombatUnitAIController unitAIController)
    {
        Vector3 direction = unitAIController.Target.transform.position - unitAIController.gameObject.transform.position;

        // Keep rotation only on the Y axis
        direction.y = 0;

        if (direction != Vector3.zero)
        {
            unitAIController.gameObject.transform.rotation = Quaternion.LookRotation(direction);
        }

       

        unitAIController.combatUnit.PlayAttackAnimation();

        var weaponStats = (ItemStatsWeapon)unitAIController.combatUnit.combatStats.inventory[0]; //first inventory is alwasy weapon
        int weaponBonus = Random.Range(weaponStats.damageRolls.x, weaponStats.damageRolls.y);
        int basePower = Random.Range(unitAIController.combatUnit.combatStats.attackRange.x, unitAIController.combatUnit.combatStats.attackRange.y);
        basePower += weaponBonus;

        //this is were you figure out what the action does.
        CombatActionData actionData = new CombatActionData();
        actionData.power = basePower;

        weaponStats.combatLogic.PreformAction(unitAIController.combatUnit, weaponStats.VFX, actionData);
        yield return new WaitForSeconds(weaponStats.useCoolDown);
    }
    public IEnumerator MoveTo(CombatUnitAIController unitAIController)
    {
        Vector2Int enemyPos = unitAIController.combatUnit.crawlerMovment.gridLocation;
        Vector2Int playerPos = PlayerController.instance.movment.gridLocation;

        Vector2Int difference = playerPos - enemyPos;
        Vector3 direction = Vector3.zero;

        if (difference.x > 0)
        {
            direction = Vector3.right;
        }
        else if (difference.x < 0)
        {
            direction = Vector3.left;
        }
        else if (difference.y > 0)
        {
            direction = Vector3.forward;
        }
        else if (difference.y < 0)
        {
            direction = Vector3.back;
        }

        yield return unitAIController.combatUnit.crawlerMovment.Move(direction);
        yield return null;
    }
    public IEnumerator LookForPlayer(CombatUnitAIController unitAIController)
    {
        Vector3 start = unitAIController.combatUnit.transform.position + Vector3.up; // eye height
        Vector3 target = PlayerController.instance.combatUnit.transform.position + Vector3.up;

        Vector3 direction = target - start;
        float distance = direction.magnitude;

        if (distance < unitAIController.maxSpotDistance)
        {
            if (Physics.Raycast(
            start,
            direction.normalized,
            out RaycastHit hit,
            distance, layerMask))
            {
                // Something blocked the view
                if (hit.collider.gameObject.transform.tag == "Player")
                {
                    unitAIController.Target = hit.collider.gameObject;
                }
                else
                {
                    unitAIController.Target = null;
                }
            }
        }
        yield return null;
    }
}
