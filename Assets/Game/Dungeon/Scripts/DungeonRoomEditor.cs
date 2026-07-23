using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(DungeonRoom))]
public class DungeonRoomEditor : Editor
{
    DungeonRoom room;

    private void OnEnable()
    {
        room = (DungeonRoom)target;
    }


    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();


        GUILayout.Space(10);


        if (GUILayout.Button("Create Grid"))
        {
            Undo.RecordObject(room, "Create Grid");

            room.CreateGrid();

            EditorUtility.SetDirty(room);
        }


        if (GUILayout.Button("Update Walls"))
        {
            Undo.RecordObject(room, "Update Walls");

            room.UpdateWalls();

            EditorUtility.SetDirty(room);
        }


        if (GUILayout.Button("Generate Room"))
        {
            room.GenerateRoom();

            EditorUtility.SetDirty(room);
        }


        GUILayout.Space(20);


        DrawGrid();
        DrawSelectedTile();
    }


    void DrawGrid()
    {
        if (room.tiles == null)
            return;

        if (room.tiles.Count == 0)
            return;


        for (int y = room.height - 1; y >= 0; y--)
        {
            GUILayout.BeginHorizontal();

            for (int x = 0; x < room.width; x++)
            {
                TileData tile = room.GetTile(x, y);

                string symbol = "X";


                if (tile.type == TileType.Floor)
                    symbol = "";

                if (tile.type == TileType.StartNorth)
                    symbol = "SN";
                if (tile.type == TileType.StartSouth)
                    symbol = "SS";
                if (tile.type == TileType.StartEast)
                    symbol = "SE";
                if (tile.type == TileType.StartWest)
                    symbol = "SW";

                if (tile.type == TileType.ExitNorth)
                    symbol = "EN";
                if (tile.type == TileType.ExitSouth)
                    symbol = "ES";
                if (tile.type == TileType.ExitEast)
                    symbol = "EE";
                if (tile.type == TileType.ExitWest)
                    symbol = "EW";


                if (GUILayout.Button(
                    symbol,
                    GUILayout.Width(30),
                    GUILayout.Height(30)))
                {
                    room.selectedTile = x + y * room.width;
                }
            }

            GUILayout.EndHorizontal();
        }
    }

    void DrawSelectedTile()
    {
        if (room.selectedTile < 0)
            return;


        TileData tile = room.tiles[room.selectedTile];


        GUILayout.Label(
            "Selected Tile: " + room.selectedTile);


        EditorGUI.BeginChangeCheck();

        EditorGUILayout.Vector2IntField("Grid postion", tile.gridPosition);

        tile.type = (TileType)EditorGUILayout.EnumPopup("Type",tile.type);

        tile.walls.north = EditorGUILayout.Toggle("North Wall",tile.walls.north);

        tile.walls.south = EditorGUILayout.Toggle("South Wall", tile.walls.south);

        tile.walls.east = EditorGUILayout.Toggle("East Wall", tile.walls.east);


        tile.walls.west = EditorGUILayout.Toggle("West Wall", tile.walls.west);


        if (EditorGUI.EndChangeCheck())
        {
            Undo.RecordObject(room, "Edit Tile");
            EditorUtility.SetDirty(room);
        }
    }
}
