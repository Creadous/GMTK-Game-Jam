using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
public class InteractableObject_Chest : InteractableObject
{
    public bool isOpened = false;
    private PlayableDirector director;

    [Header("Loot")]
    public Vector2Int goldRange;
    public List<ItemStatsBase> ItemDrops;

    public void Awake()
    {
        director = GetComponent<PlayableDirector>();
    }
    public override bool AbleToInteractWith()
    {
        if (isOpened == false)
        {
            return true;
        }
        return false;
    }
    public override bool InteractWithObject(InteractablePlayerController interactablePlayerController)
    {
        director.Play();
        isOpened = true;
        //pop up item second
        GameController.instance.OpenLootDropMenu(ItemDrops, goldRange);
        return true;
    }
}
