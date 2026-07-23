using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonDoorTile : MonoBehaviour
{
    public bool startingDoor;
    public GameObject NorthObject;
    public GameObject SouthObject;
    public GameObject EastObject;
    public GameObject WestObject;

    public PlayerSpawnLocation northWall;    
    public PlayerSpawnLocation southWall;    
    public PlayerSpawnLocation eastWall;    
    public PlayerSpawnLocation westWall;

    [HideInInspector] public PlayerSpawnLocation currentSpawnLocation;

    public void Setup(TileType tileType)
    {
        if (tileType == TileType.StartNorth || tileType == TileType.ExitNorth)
        {
            NorthObject.SetActive(true);
            if(startingDoor) currentSpawnLocation = northWall;
        }
        if (tileType == TileType.StartSouth || tileType == TileType.ExitSouth)
        {
            SouthObject.SetActive(true);
            if (startingDoor) currentSpawnLocation = southWall;
        }
        if (tileType == TileType.StartEast || tileType == TileType.ExitEast)
        {
            EastObject.SetActive(true);
            if (startingDoor) currentSpawnLocation = eastWall;
        }
        if (tileType == TileType.StartWest || tileType == TileType.ExitWest)
        {
            WestObject.SetActive(true);
            if (startingDoor) currentSpawnLocation = westWall;
        }
    }
}
