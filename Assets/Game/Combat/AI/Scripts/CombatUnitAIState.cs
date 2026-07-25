using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Game/Combat/AI/State")]
public class CombatUnitAIState : ScriptableObject
{
    public string description;
    public List<CombatUnitAIAction> aIAction;
    public List<AITransition> transitions;
}
[System.Serializable]
public class AITransition
{
    public CombatUnitAICondition condition;
    public CombatUnitAIState targetState;
}
