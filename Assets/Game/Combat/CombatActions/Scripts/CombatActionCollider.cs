using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
public class CombatActionCollider : MonoBehaviour
{
    public bool hitSomething = false;
    public PlayableDirector director;
    public int power;

    [Header("Bullet")]
    public Rigidbody rb;
    public bool isBullet;
    public float bulletSpeed;
    public GameObject impactDeathVFX;

    [Header("debug")]
    public bool disableOnHit = false;
    public bool disableDestory; // debug
    public void PlayAction()
    {
        
        hitSomething = false;
        if (isBullet == false)
        {
            director.Play();
        }
        else
        {
            rb.AddForce(this.transform.forward * bulletSpeed, ForceMode.Impulse);
        }
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
                if (other.gameObject.tag == "Player")
                {
                    PlayerController.instance.ShakeCamera();
                }
                hitSomething = true;

                if (isBullet)
                {
                    DestoryButllet();
                }
            }
            else if(isBullet && combatUnit == null)
            {
                //hit wall
                DestoryButllet();
            }
        }
    }
    public void DestoryButllet()
    {
        if(impactDeathVFX != null)
        {
            var vfxGameObject = Instantiate(impactDeathVFX, this.transform);
            vfxGameObject.transform.parent = null;

        }
        this.gameObject.SetActive(false);
        GameObject.Destroy(this.gameObject);
    }
    public void AnimationFinished()
    {
        if (disableDestory) return;
       GameObject.DestroyImmediate(this.gameObject);
    }
}
