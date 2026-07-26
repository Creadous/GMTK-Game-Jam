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
    public List<ItemStatsBase> lootPool;
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
        GameAudioManager.instance.PlaySoundEffect("chest_open");
        director.Play();
        isOpened = true;
        var loot = MasterItemList.instance.GetRandomItemsFromPool(lootPool, 3);
        GameController.instance.OpenLootDropMenu(loot, goldRange);
        return false;
    }
}
