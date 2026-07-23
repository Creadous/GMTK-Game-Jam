using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : MonoBehaviour
{
    public bool newScene;
    public string sceneName;
    public PlayerSpawnLocation playerSpawnLocation;
    public SceneTransitionType transitionType;
    [SerializeField] private List<GameObject> OnActiveWhenEntering;


    public virtual void TeleportPlayer()
    {
        Debug.Log("TeleportPlayer - teleporter");
        if (newScene)
        {
            // SceneManagerController.instance.LaunchScene(this);
        }
        else
        {
            Debug.Log("TeleportPlayer - Asking sceneManager to launchLocalTeleportation");
            SceneManagerController.instance.LaunchLocalTeleportation(this);
        }
    }
}
