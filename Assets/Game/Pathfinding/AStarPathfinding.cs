using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AStarPathfinding : MonoBehaviour
{
    public static AStarPathfinding instance;

    private void Awake()
    {
        instance = this;
    }


    public List<Vector2Int> FindPath(Vector2Int startPos,Vector2Int targetPos)
    {
        TileData[,] grid = DungeonManager.instance.currentRoom.GetTileDataGrid();
        TileData startNode = grid[startPos.x, startPos.y];
        TileData targetNode = grid[targetPos.x, targetPos.y];

        List<TileData> openSet = new List<TileData>();
        HashSet<TileData> closedSet = new HashSet<TileData>();

        //reset the nodes
        foreach(TileData node in grid)
        {
            node.gCost = int.MaxValue;
            node.parent = null;
        }
        startNode.gCost = 0;
        startNode.hCost = GetDistance(startNode, targetNode);

        openSet.Add(startNode);
        while(openSet.Count > 0)
        {
            TileData current = openSet[0];
            for(int i = 1; i < openSet.Count; i++)
            {
                if(openSet[i].fCost < current.fCost || 
                    (openSet[i].fCost == current.fCost &&
                     openSet[i].hCost < current.hCost))
                {
                    current = openSet[i];
                }
            }
            openSet.Remove(current);
            closedSet.Add(current);

            if (current == targetNode)
                return RetracePath(startNode, targetNode);

            foreach (TileData neighbour in GetNeighbours(current))
            {
                if (!neighbour.IsWalkable() || closedSet.Contains(neighbour))
                    continue;

                int newCost = current.gCost + 1;

                if (newCost < neighbour.gCost)
                {
                    neighbour.gCost = newCost;
                    neighbour.hCost = GetDistance(neighbour, targetNode);
                    neighbour.parent = current;

                    if (!openSet.Contains(neighbour))
                        openSet.Add(neighbour);
                }
            }
        }
        return null;
    }
    List<Vector2Int> RetracePath(TileData start, TileData end)
    {
        List<Vector2Int> path = new List<Vector2Int>();

        TileData current = end;

        while (current != start)
        {
            path.Add(current.gridPosition);
            current = current.parent;
        }

        path.Add(start.gridPosition);
        path.Reverse();

        return path;
    }
    List<TileData> GetNeighbours(TileData node)
    {
        List<TileData> neighbours = new List<TileData>();

        Vector2Int[] dirs =
        {
            Vector2Int.up,
            Vector2Int.down,
            Vector2Int.left,
            Vector2Int.right
        };

        foreach (var dir in dirs)
        {
            Vector2Int pos = node.gridPosition + dir;

            if (InBounds(pos))
                neighbours.Add(DungeonManager.instance.currentRoom.GetTileDataGrid()[pos.x, pos.y]);
        }

        return neighbours;
    }
    #region helpers
    bool InBounds(Vector2Int p)
    {
        return p.x >= 0 && p.y >= 0 &&
               p.x < DungeonManager.instance.currentRoom.width && p.y < DungeonManager.instance.currentRoom.height;
    }

    int GetDistance(TileData a, TileData b)
    {
        return Mathf.Abs(a.gridPosition.x - b.gridPosition.x) + Mathf.Abs(a.gridPosition.y - b.gridPosition.y);
    }
    #endregion
}
