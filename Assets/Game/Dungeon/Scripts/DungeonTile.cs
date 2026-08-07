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
    public List<GameObject> floorType;

    public void Setup(Vector2Int gridPosition,bool floor, TileData tile)//  bool floor, bool northwall, bool southwall, bool eastwall, bool westwall, int floorindex)
    {
       gridPos = gridPosition;
        if (floor) floorObject.SetActive(true);
        if (tile.walls.north) northwallObject.SetActive(true);
        if (tile.walls.south) southwallObject.SetActive(true);
        if (tile.walls.east) eastwallObject.SetActive(true);
        if (tile.walls.west) westwallObject.SetActive(true);
        floorType[0].SetActive(false);
        floorType[tile.floorIndex].SetActive(true);
    }
}
