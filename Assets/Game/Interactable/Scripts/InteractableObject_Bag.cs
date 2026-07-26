using UnityEngine;

public class InteractableObject_Bag : InteractableObject
{
    public Vector2Int goldRange = new Vector2Int(2, 9); // Random.Range max is exclusive, so 9 gives up to 8

    public override bool AbleToInteractWith()
    {
        return true;
    }

    public override bool InteractWithObject(InteractablePlayerController interactablePlayerController)
    {
        int goldAmount = Random.Range(goldRange.x, goldRange.y);
        PlayerController.instance.gold += goldAmount;
        Destroy(gameObject);
        return false;
    }
}