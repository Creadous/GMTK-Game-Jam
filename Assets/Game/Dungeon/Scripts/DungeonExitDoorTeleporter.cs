using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class DungeonExitDoorTeleporter : MonoBehaviour
{
    public List<string> roomTypes;
    public List<Sprite> roomIcons;

    public Sprite bossIcon;

    public Image doorTypeImage;
    public string nextRoomType;
    private bool hasSetUp = false; // need because boss get overwriten by start

    // Start is called before the first frame update
    void Start()
    {
        if(hasSetUp == false) UpdateRoomChoice();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void UpdateRoomChoice()
    {
        var choice = Random.Range(0, roomTypes.Count);
        nextRoomType = roomTypes[choice];
        doorTypeImage.sprite = roomIcons[choice];
        hasSetUp = true;
    }
    public void SetRoomToBossRoom()
    {
        nextRoomType = "Boss";
        doorTypeImage.sprite = bossIcon;
        hasSetUp = true;
    }
}
