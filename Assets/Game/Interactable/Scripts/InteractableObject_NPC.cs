using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObject_NPC : InteractableObject
{
    public Dialogue dialogue;
    private void Awake()
    {
        dialogue = GetComponent<Dialogue>();
    }
    public override bool InteractWithObject(InteractablePlayerController interactablePlayerController)
    {
        GameController.instance.LaunchDialogueSystem(dialogue, this);
        return false;
    }
}
