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
    public LayerMask layerMask;

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
    private bool CloseEnoughToAttack(CombatUnitAIController ai)
    {
        // Too far away to even know about the player
        Vector3 start = ai.combatUnit.transform.position + Vector3.up;
        Vector3 target = PlayerController.instance.combatUnit.transform.position + Vector3.up;

        float worldDistance = Vector3.Distance(start, target);

        if (worldDistance > ai.maxSpotDistance)
            return false;

        // Check attack range in tiles
        bool InRange = ai.IsInAttackRange(
            ai.combatUnit.crawlerMovment.gridLocation,
            PlayerController.instance.movment.gridLocation,
            ai.attackRange);

        if (InRange)
        {
            return HasLineOfSight(ai.combatUnit.crawlerMovment.gridLocation, PlayerController.instance.movment.gridLocation);
        }

        return false;

    }
    public bool HasLineOfSight(Vector2Int start, Vector2Int end)
    {
        TileData[,] grid = DungeonManager.instance.currentRoom.GetTileDataGrid();

        //boundry check
        int width = grid.GetLength(0);
        int height = grid.GetLength(1);

        if (start.x < 0 || start.x >= width ||
            start.y < 0 || start.y >= height ||
            end.x < 0 || end.x >= width ||
            end.y < 0 || end.y >= height)
        {
            Debug.LogError($"Out of bounds! Start:{start} End:{end}");
            return false;
        }

        // Same column
        if (start.x == end.x)
        {
            int step;

            if (start.y < end.y)
            {
                step = 1;
            }
            else
            {
                step = -1;
            }

            for (int y = start.y + step; y != end.y; y += step)
            {
                
                if (grid[start.x, y].type == TileType.Empty)
                    return false;
            }

            return true;
        }

        // Same row
        if (start.y == end.y)
        {
            int step;

            if (start.x < end.x)
            {
                step = 1;
            }
            else
            {
                step = -1;
            }


            for (int x = start.x + step; x != end.x; x += step)
            {
                if (grid[x, start.y].type == TileType.Empty)
                    return false;
            }

            return true;
        }

        // Can't see around corners or diagonally.
        return false;
    }
}
