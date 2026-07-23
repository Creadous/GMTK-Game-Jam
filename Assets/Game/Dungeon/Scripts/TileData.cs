using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TileType
{
    Empty,
    Floor,
    StartNorth,
    StartSouth,
    StartEast,
    StartWest,
    ExitNorth,
    ExitSouth,
    ExitEast,
    ExitWest
}

[System.Serializable]
public class TileData
{
    public TileType type;

    public WallData walls;
   
    public bool occupied;
}
[System.Serializable]
public class WallData
{
    public bool north;
    public bool south;
    public bool east;
    public bool west;
}
