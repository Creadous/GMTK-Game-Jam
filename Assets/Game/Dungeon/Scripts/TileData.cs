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
    public Vector2Int gridPosition;
    public TileType type;
    public int floorIndex; //used for editor
    public WallData walls;
   
    public bool occupied;
    //astar pathfinding
    [HideInInspector] public TileData parent;
    public int gCost;
    public int hCost;
    public int fCost => gCost + hCost; // total cost

    public bool IsWalkable()
    {
        if(type != TileType.Empty)
        {
            return true;
        }
        return false;
    }
}
[System.Serializable]
public class WallData
{
    public bool north;
    public bool south;
    public bool east;
    public bool west;
}
