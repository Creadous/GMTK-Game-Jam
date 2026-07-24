using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObject_DungeonDoorTeleporter : InteractableObject
{
    public DungeonExitDoorTeleporter teleporter;

    public override bool InteractWithObject(InteractablePlayerController interactablePlayerController)
    {
        DungeonManager.instance.ChangeRoom(teleporter.nextRoomType);
        return true;
    }
}
