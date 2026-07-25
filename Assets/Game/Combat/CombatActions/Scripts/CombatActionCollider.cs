using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
public class CombatActionCollider : MonoBehaviour
{
    public bool hitSomething = false;
    public PlayableDirector director;
    public bool disableDestory; // debug
    public int power;
    public bool disableOnHit = false;
    public void PlayAction()
    {
        director.Play();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (disableOnHit) return;
        if(hitSomething == false)
        {
            var combatUnit = other.gameObject.GetComponent<CombatUnit>();
            if(combatUnit != null)
            {
                Debug.Log("Hit " + combatUnit.combatStats.name);
                combatUnit.GetHit(-power);
                hitSomething = true;

            }
        }
    }
    public void AnimationFinished()
    {
        if (disableDestory) return;
       GameObject.DestroyImmediate(this.gameObject);
    }
}
