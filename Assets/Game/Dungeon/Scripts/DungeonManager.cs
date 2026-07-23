using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonManager : MonoBehaviour
{
    public static DungeonManager instance;
    // Start is called before the first frame update
    public DungeonRoom currentRoom;

    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        PlayerController.instance.SetModelPostion(currentRoom.startingPosition.spawnPoint);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
