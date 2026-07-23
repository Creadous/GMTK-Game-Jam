using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObject : MonoBehaviour, IInteractable
{
    public string interactionDescription = "Activate";
    private InteractablePlayerController interactablePlayercontrollerReff;
    public string interactionSoundEffectKey;
    public virtual bool AbleToInteractWith()
    {
        if(this.gameObject.activeInHierarchy == false)
        {
            return false;
        }
        return true;
    }

    public virtual GameObject GetGameObject()
    {
        return this.gameObject;
    }

    public string InteracatDescription()
    {
        return interactionDescription;
    }

    public virtual bool InteractWithObject(InteractablePlayerController interactablePlayerController)
    {
        interactablePlayercontrollerReff = interactablePlayerController;

        return true;
    }

    public virtual void StopInteractWithObject()
    {
        interactablePlayercontrollerReff.interactingWithObject = false;
    }
}
