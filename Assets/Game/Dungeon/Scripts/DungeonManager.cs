using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonManager : MonoBehaviour
{
    public static DungeonManager instance;
    // Start is called before the first frame update
    [Header("DungeonLayout")]
    public List<DungeonTileLayout> dungeonTileLayouts;
    public DungeonTileLayout currentSelectLayout;
    
    [Space]
    [Header("DungeonRoom")]
    public DungeonRoom currentRoom;
    public string lastRoomID;

    public int currentRoomVisitCount;
    public int numberOfRoomBeforeBoss;

    [Header("UI")]
    public BossRoomCountDownHud bossCountDownHud;

    [Header("bullets")]
    public List<GameObject> activeBullets;

    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        currentSelectLayout = dungeonTileLayouts[Random.Range(0, dungeonTileLayouts.Count)];
        numberOfRoomBeforeBoss = Random.Range(currentSelectLayout.RanngeOfRoomBeforeBoss.x, currentSelectLayout.RanngeOfRoomBeforeBoss.y);
        var roomObject = Instantiate(currentSelectLayout.startingDungeonRoomPrefab,this.transform);
        currentRoom = roomObject.GetComponent<DungeonRoom>();

        PlayerController.instance.SetModelPostion(currentRoom.startingPosition.spawnPoint);
        UpdateBossCountDownUI();
        GameAudioManager.instance.PlayDungeonTrack();

        activeBullets = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ChangeRoom(string newRoomType)
    {
        Debug.Log("Change to room " + newRoomType);
        StartCoroutine(RoomTransition(newRoomType));
    }
    public IEnumerator RoomTransition(string newRoomType)
    {
        GameAudioManager.instance.PlaySoundEffect("door_open");
        yield return SceneManagerController.instance.PlayTrantion(SceneTransitionType.fade, false);
        LoadNewRoom(newRoomType);
        yield return new WaitForFixedUpdate();
        yield return SceneManagerController.instance.PlayTrantion(SceneTransitionType.fade, true);
        GameAudioManager.instance.PlaySoundEffect("door_close");
    }
    private void LoadNewRoom(string newRoomType)
    {
        ClearAllBullets();
        GameObject selectRoom = null;
        List<GameObject> roomChoice = new List<GameObject>();
        switch (newRoomType)
        {
            case "Mystery":
                for (int i = 0; i < currentSelectLayout.CombatRoomPrefabs.Count; i++)
                {
                    roomChoice.Add(currentSelectLayout.CombatRoomPrefabs[i]);
                }
                for (int i = 0; i < currentSelectLayout.ShopRoomPrefabs.Count; i++)
                {
                    roomChoice.Add(currentSelectLayout.ShopRoomPrefabs[i]);
                }
                for (int i = 0; i < currentSelectLayout.TrapRoomPrefabs.Count; i++)
                {
                    roomChoice.Add(currentSelectLayout.TrapRoomPrefabs[i]);
                }
                break;
            case "Combat":
                for(int i = 0; i  < currentSelectLayout.CombatRoomPrefabs.Count; i++)
                {
                    roomChoice.Add(currentSelectLayout.CombatRoomPrefabs[i]);
                }
                
                break;
            case "Shop":
                for (int i = 0; i < currentSelectLayout.ShopRoomPrefabs.Count; i++)
                {
                    roomChoice.Add(currentSelectLayout.ShopRoomPrefabs[i]);
                }
                break;
            case "Trap":
                for (int i = 0; i < currentSelectLayout.TrapRoomPrefabs.Count; i++)
                {
                    roomChoice.Add(currentSelectLayout.TrapRoomPrefabs[i]);
                }
                break;
            case "Boss":
                roomChoice.Add(currentSelectLayout.bossDungeonRoomPrefab);
                break;
        }

        //cycle room selection unitl you get New one
        lastRoomID = currentRoom.roomID;
        string newRoomID = "";
        do
        {
            selectRoom = roomChoice[Random.Range(0, roomChoice.Count)];
            newRoomID = selectRoom.GetComponent<DungeonRoom>().roomID;
        } 
        while (lastRoomID == newRoomID);


        //remove all room
        currentRoom.DestroyRoom();
        GameObject.Destroy(currentRoom.gameObject);

        //create new room
        var roomObject = Instantiate(selectRoom, this.transform);
        currentRoom = roomObject.GetComponent<DungeonRoom>();

        //set player postion
        PlayerController.instance.SetModelPostion(currentRoom.startingPosition.spawnPoint);
        PlayerController.instance.ResetInteractablePlayer();

        currentRoomVisitCount++;
        SpawnBossDoor(); // turn all exit into boss doors
        UpdateBossCountDownUI();
    }

    private void ClearAllBullets()
    {
        foreach(GameObject bullet in activeBullets)
        {
            if(bullet != null)
            {
                GameObject.DestroyImmediate(bullet);
            }
        }
        activeBullets.Clear();
    }
    public void SpawnBossDoor()
    {
        if((currentRoomVisitCount +1) == numberOfRoomBeforeBoss) //room just before boss room
        {
            Debug.Log("Spawn boss room");
            foreach(DungeonExitDoorTeleporter doorExit in currentRoom.exits)
            {
                doorExit.SetRoomToBossRoom();//boss room index
            }
        }
    }
    private void UpdateBossCountDownUI()
    {
        int roomsLeft = numberOfRoomBeforeBoss - currentRoomVisitCount;
        bossCountDownHud.UpateCountDown(roomsLeft);
    }
    public bool DisableStaminaDrain()
    {
        if (currentRoom.disableStaminaDrain)
        {
            return true;
        }
        return false;
    }
}
