using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonRoom : MonoBehaviour
{
    [Header("Room Generation")]
    public int width = 10;
    public int height = 10;
    public int cellSize = 5;

    public GameObject DungeonTilePrefab;
    public GameObject dungeonStartingDoorPrefab;
    public GameObject dungeonExitDoorPrefab;
    public GameObject containerForDungeonTiles;

    public List<TileData> tiles = new List<TileData>();
    public int selectedTile = -1;

    [Space]
    public PlayerSpawnLocation startingPosition;

    public List<DungeonExitDoorTeleporter> exits;


    public TileData GetTile(int x, int y)
    {
        return tiles[x + y * width];
    }

    public void CreateGrid()
    {
        tiles.Clear();
        for(int y = 0; y <height; y++)
        {
            for(int x = 0; x <width; x++)
            {
                var tileData = new TileData();
                tileData.gridPosition = new Vector2Int(x, y);
                tiles.Add(tileData);
            }
        }

        ClearTiles();
    }

    #region wall logic
    public void UpdateWalls()
    {
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                TileData tile = GetTile(x, y);

                if (tile.type == TileType.Empty)
                    continue;

                switch (tile.type) 
                {
                    case TileType.Floor:
                        tile.walls.north = !IsWalkable(x, y + 1);
                        tile.walls.south = !IsWalkable(x, y - 1);
                        tile.walls.east = !IsWalkable(x + 1, y);
                        tile.walls.west = !IsWalkable(x - 1, y);
                        break;
                    case TileType.StartNorth:
                        tile.walls.north = false;
                        tile.walls.south = !IsWalkable(x, y - 1);
                        tile.walls.east = !IsWalkable(x + 1, y);
                        tile.walls.west = !IsWalkable(x - 1, y);
                        break;
                    case TileType.StartSouth:
                        tile.walls.north = !IsWalkable(x, y + 1);
                        tile.walls.south = false;
                        tile.walls.east = !IsWalkable(x + 1, y);
                        tile.walls.west = !IsWalkable(x - 1, y);
                        break;
                    case TileType.StartEast:
                        tile.walls.north = !IsWalkable(x, y + 1);
                        tile.walls.south = !IsWalkable(x, y - 1);
                        tile.walls.east = false;
                        tile.walls.west = !IsWalkable(x - 1, y);
                        break;
                    case TileType.StartWest:
                        tile.walls.north = !IsWalkable(x, y + 1);
                        tile.walls.south = !IsWalkable(x, y - 1);
                        tile.walls.east = !IsWalkable(x + 1, y);
                        tile.walls.west = false;
                        break;
                    case TileType.ExitNorth:
                        tile.walls.north = false;
                        tile.walls.south = !IsWalkable(x, y - 1);
                        tile.walls.east = !IsWalkable(x + 1, y);
                        tile.walls.west = !IsWalkable(x - 1, y);
                        break;
                    case TileType.ExitSouth:
                        tile.walls.north = !IsWalkable(x, y + 1);
                        tile.walls.south = false;
                        tile.walls.east = !IsWalkable(x + 1, y);
                        tile.walls.west = !IsWalkable(x - 1, y);
                        break;
                    case TileType.ExitWest:
                        tile.walls.north = !IsWalkable(x, y + 1);
                        tile.walls.south = !IsWalkable(x, y - 1);
                        tile.walls.east = !IsWalkable(x + 1, y);
                        tile.walls.west = false;
                        break;
                    case TileType.ExitEast:
                        tile.walls.north = !IsWalkable(x, y + 1);
                        tile.walls.south = !IsWalkable(x, y - 1);
                        tile.walls.east = false;
                        tile.walls.west = !IsWalkable(x - 1, y);
                        break;
               
                }
            }
        }
    }


    private bool IsWalkable(int x, int y)
    {
        // outside room boundary = wall
        if (x < 0 || x >= width)
            return false;

        if (y < 0 || y >= height)
            return false;

        if(GetTile(x, y).type == TileType.Empty)
        {
            return false;
        }
        else
        {
            return true;
        }
    }
    #endregion
    public void GenerateRoom()
    {
        //destory all object create for this room
        ClearTiles();

        exits = new List<DungeonExitDoorTeleporter>();

        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                TileData tile = GetTile(x, y);
                Vector3 position = new Vector3(x * cellSize, 0, y * cellSize);

                if (tile.type != TileType.Empty)
                {
                    //Genertate floor tile
                    GameObject DungeonTileObject = Instantiate(DungeonTilePrefab, position + Vector3.right * 0.5f, Quaternion.Euler(0, 90, 0),containerForDungeonTiles.transform);
                    DungeonTileObject.GetComponent<DungeonTile>().Setup(true, tile.walls.north, tile.walls.south, tile.walls.east, tile.walls.west);
                }

                //spawnDoor
                switch (tile.type)
                {
                    case TileType.StartNorth:
                    case TileType.StartSouth:
                    case TileType.StartEast:
                    case TileType.StartWest:
                        {
                            GameObject DungeonStartingDoorObject = Instantiate(dungeonStartingDoorPrefab, position + Vector3.right * 0.5f, Quaternion.Euler(0, 90, 0), containerForDungeonTiles.transform);
                            DungeonDoorTile doorTile = DungeonStartingDoorObject.GetComponent<DungeonDoorTile>();
                            doorTile.Setup(tile.type);
                            if (doorTile.startingDoor)
                            {
                                startingPosition = doorTile.currentSpawnLocation;
                            }
                        }
                        break;
                    case TileType.ExitNorth:
                    case TileType.ExitSouth:
                    case TileType.ExitWest:
                    case TileType.ExitEast:
                        {
                            GameObject DungeonStartingDoorObject = Instantiate(dungeonExitDoorPrefab, position + Vector3.right * 0.5f, Quaternion.Euler(0, 90, 0), containerForDungeonTiles.transform);
                            DungeonDoorTile doorTile = DungeonStartingDoorObject.GetComponent<DungeonDoorTile>();
                            doorTile.Setup(tile.type);
                            exits.Add(doorTile.currentExit);
                        }
                        break;
                }             
            }
        }
    }

    public void ClearTiles()
    {
        for (int i = containerForDungeonTiles.transform.childCount - 1; i >= 0; i--)
        {
            DestroyImmediate(containerForDungeonTiles.transform.GetChild(i).gameObject);
        }
    }
    public void DestroyRoom()
    {
        tiles.Clear();
        ClearTiles();
    }
}
