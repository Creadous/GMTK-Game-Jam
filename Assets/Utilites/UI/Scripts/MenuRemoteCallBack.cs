using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuRemoteCallBack : MonoBehaviour
{
    public GameObject menuPrefab;
    public void LaunchMenu()
    {
        var menuObject = Instantiate(menuPrefab, GameController.instance.Canvas);
    }
}
