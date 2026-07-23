using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Game/Dungeon/Layout")]
public class DungeonTileLayout : ScriptableObject
{
    public Vector2Int RanngeOfRoomBeforeBoss;
    public GameObject startingDungeonRoomPrefab;
    public List<GameObject> CombatRoomPrefabs;
    public List<GameObject> ShopRoomPrefabs;
    public List<GameObject> TrapRoomPrefabs;
    public GameObject bossDungeonRoomPrefab;
}
