using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatUnitAIController : MonoBehaviour
{
    public CombatUnit combatUnit;

    public CombatUnitAIState startingState;
    public CombatUnitAIState currentState;

    public float maxSpotDistance;
    public int attackRange = 1; // cell are 5 feet
    public GameObject Target; // why it not combatUnit or player is because you can make this thing follow a path with notes.
    public bool finishedTurn = true;
    public void Start()
    {
        currentState = startingState;
    }
    public void FixedUpdate()
    {
        if (GameController.IsGamePaused()) return;
        if (combatUnit.IsDead) return;
        if (finishedTurn)
        {
            StartCoroutine(ExecuteTurn());
        }

    }
    public IEnumerator ExecuteTurn()
    {
        finishedTurn = false;
        foreach(CombatUnitAIAction action in currentState.aIAction)
        {
            yield return action.PreformAction(this);
            yield return null;
        }
        
        foreach(AITransition transtion in currentState.transitions)
        {
            if (transtion.condition.CheckConditionStatus(this))
            {
                currentState = transtion.targetState;
                break;
            }
        }
        finishedTurn = true;
        yield return null;
    }


    #region helper
    public bool IsInAttackRange(Vector2Int enemyGrid, Vector2Int playerGrid, int range)
    {
        int distance =
            Mathf.Abs(enemyGrid.x - playerGrid.x) +
            Mathf.Abs(enemyGrid.y - playerGrid.y);

        return distance <= range;
    }
    #endregion
}
