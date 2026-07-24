using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractable
{
    public bool AbleToInteractWith();

    public string InteracatDescription();
    public bool InteractWithObject(InteractablePlayerController interactablePlayerController);
    public void StopInteractWithObject();
    public GameObject GetGameObject();
}
