using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class DungeonExitDoorTeleporter : MonoBehaviour
{
    public List<string> roomTypes;
    public List<Sprite> roomIcons;
    public Image doorTypeImage;
    public string nextRoomType;


    // Start is called before the first frame update
    void Start()
    {
        var choice = Random.Range(0, roomTypes.Count);
        nextRoomType = roomTypes[choice];
        doorTypeImage.sprite = roomIcons[choice];

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
