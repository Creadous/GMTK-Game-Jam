using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonTile : MonoBehaviour
{
    public Vector2Int gridPos;
    public GameObject floorObject;
    public GameObject northwallObject;
    public GameObject southwallObject;
    public GameObject eastwallObject;
    public GameObject westwallObject;

    public void Setup(Vector2Int gridPosition, bool floor, bool northwall, bool southwall, bool eastwall, bool westwall)
    {
        gridPos = gridPosition;
        if (floor) floorObject.SetActive(true);
        if (northwall) northwallObject.SetActive(true);
        if (southwall) southwallObject.SetActive(true);
        if (eastwall) eastwallObject.SetActive(true);
        if (westwall) westwallObject.SetActive(true);
    }
}
