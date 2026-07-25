using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapController : MonoBehaviour
{
    public float startingTime;
    [HideInInspector] public float currentTime;
    public float maxCoolDown;
    public CombatActionCollider actionCollider;

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
            actionCollider.PlayAction();
        }
    }
}
