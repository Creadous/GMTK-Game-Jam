using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CombatUnit : MonoBehaviour
{
    [HideInInspector] public DungeonCrawlerMovment crawlerMovment;
    public Animator animator;
    public CombatStats combatStats;
    public bool IsDead = false;
    public Vector2Int deathGoldDrop;
    [Header("Events")]
    public UnityEvent OnDeathCallBack;
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
            if(animator != null) animator.SetTrigger("Death");
            IsDead = true;
            
        }
    }
    public void FinishWithDeathAnimation() // called form the animator
    {
        if(deathGoldDrop != Vector2Int.zero)
        {
            GameAudioManager.instance.PlaySoundEffect("coins");
            PlayerController.instance.gold += Random.Range(deathGoldDrop.x, deathGoldDrop.y);
        }
        this.gameObject.SetActive(false);
        OnDeathCallBack?.Invoke();
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
