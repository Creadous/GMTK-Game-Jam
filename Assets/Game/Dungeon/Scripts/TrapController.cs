using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapController : MonoBehaviour
{
    public enum TrapType
    {
        spike,
        bullet
    }
    public TrapType trapType;
    public float startingTime;
    [HideInInspector] public float currentTime;
    public float maxCoolDown;
    public CombatActionCollider actionCollider;
    public GameObject bulletPrefab;
    public int bulletpower;
    private void Awake()
    {
        currentTime = startingTime;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        currentTime -= Time.deltaTime;
        if(currentTime < 0)
        {
            currentTime = maxCoolDown;
            if(trapType == TrapType.spike) actionCollider.PlayAction();
            if (trapType == TrapType.bullet) SpawnBullet();
        }
    }
    public void SpawnBullet()
    {
        var vfxObject = Instantiate(bulletPrefab, this.gameObject.transform);
        vfxObject.transform.parent = null; //deattach from parent and be free 
        vfxObject.transform.position += this.transform.forward * 4.5f;

        var combatActionController = vfxObject.GetComponent<CombatActionCollider>();
        combatActionController.power = bulletpower;
        combatActionController.PlayAction();
    }
}
