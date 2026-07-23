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

    [Header("doorExit")]
    public DungeonExitDoorTeleporter exitNorth;
    public DungeonExitDoorTeleporter exitSouth;
    public DungeonExitDoorTeleporter exitEast;
    public DungeonExitDoorTeleporter exitWest;

    [Space]
    public DungeonExitDoorTeleporter currentExit;

    [HideInInspector] public PlayerSpawnLocation currentSpawnLocation;

    public void Setup(TileType tileType)
    {
        if (tileType == TileType.StartNorth || tileType == TileType.ExitNorth)
        {
            NorthObject.SetActive(true);
            if(startingDoor) currentSpawnLocation = northWall;
            if (tileType == TileType.ExitNorth) currentExit = exitNorth;
        }
        if (tileType == TileType.StartSouth || tileType == TileType.ExitSouth)
        {
            SouthObject.SetActive(true);
            if (startingDoor) currentSpawnLocation = southWall;
            if (tileType == TileType.ExitSouth) currentExit = exitSouth;
        }
        if (tileType == TileType.StartEast || tileType == TileType.ExitEast)
        {
            EastObject.SetActive(true);
            if (startingDoor) currentSpawnLocation = eastWall;
            if (tileType == TileType.ExitEast) currentExit = exitEast;
        }
        if (tileType == TileType.StartWest || tileType == TileType.ExitWest)
        {
            WestObject.SetActive(true);
            if (startingDoor) currentSpawnLocation = westWall;
            if (tileType == TileType.ExitWest) currentExit = exitWest;
        }
    }
}
