using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatUnit : MonoBehaviour
{
    [HideInInspector] public DungeonCrawlerMovment crawlerMovment;
    public Animator animator;
    public CombatStats combatStats;
    public bool IsDead = false;
    private void Awake()
    {
        crawlerMovment = GetComponent<DungeonCrawlerMovment>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (IsDead == true) return;
        CheckLife();
    }
    private void CheckLife()
    {
        if(combatStats.GetCurrentStamina() == 0)
        {
            animator.SetTrigger("Death");
            IsDead = true;
        }
    }
    public void GetHit(int power)
    {
        if(animator != null) animator.SetTrigger("GetHit");
        combatStats.UpdateCurrentStamina(power);
    }
    public void PlayAttackAnimation()
    {
        if (animator != null) animator.SetTrigger("Attack");
    }
}
