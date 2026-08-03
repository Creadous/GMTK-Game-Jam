using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObject_RecoveryFountain : InteractableObject
{
    public bool hasBeenUsed;
    public int stanimaReovery;
    public GameObject vfx;
    public List<GameObject> useDisableObjects;
    public override bool AbleToInteractWith()
    {
        if (hasBeenUsed) return false;

        return base.AbleToInteractWith();
    }
    public override bool InteractWithObject(InteractablePlayerController interactablePlayerController)
    {
        hasBeenUsed = true;
        PlayerController.instance.combatUnit.combatStats.UpdateCurrentStamina(stanimaReovery);
        Instantiate(vfx, PlayerController.instance.combatUnit.gameObject.transform);
        for(int i= 0; i< useDisableObjects.Count; i++)
        {
            useDisableObjects[i].SetActive(false);
        }
        return false;

    }
}
