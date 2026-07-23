using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum PortalKey
{
    None, A, B, C, D, E, F, G, H, I, J, K, L, M, N, O, P, Q, R, S, T, U, V, W, X, Y, Z,DungeonStart, T1,T2,T3,T4,T5,T6,T7,T8,T9,T10,B1,B2,B3 //T series are for dungeon teleports
}

public class SceneManagerController : MonoBehaviour
{
    public static SceneManagerController instance;
    public bool isCurrentlyTeleporting;

    [Header("Transition")]
    public Transform Canvas;
    public TransitionController transitionController;

    private string sceneNameToLoad;
    private SceneTransitionType sceneTransition;
    private PortalKey scenePortalKey;

    private Teleporter localTeleporter;

    private void Awake()
    {
        if (instance == null)
        {
            DontDestroyOnLoad(this.gameObject);
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }
    public void LaunchTitleScene()
    {
        instance.LaunchScene("TitleScene", PortalKey.None, SceneTransitionType.fade);
    }
    public void LaunchScene(string scene, PortalKey portalKey, SceneTransitionType sceneTransitionType)
    {
        instance.sceneNameToLoad = scene;
        instance.scenePortalKey = portalKey;
        instance.sceneTransition = sceneTransitionType;
        StartCoroutine(LoadingSceneSequence());
    }
    public IEnumerator LoadingSceneSequence()
    {
        yield return TeleportStarting(instance.sceneTransition);// fade out

        yield return SceneManager.LoadSceneAsync(instance.sceneNameToLoad);
        yield return TeleportEnding(instance.sceneTransition); //fade in

        yield return null;
    }
    #region localTeleporting
    public void LaunchLocalTeleportation(PortalKey portalKey) //used for dungeon teleportation where you use a map.
    {
        Teleporter otherPortal = GetPortal(portalKey);
        StartCoroutine(instance.TeleportLocalSequence(otherPortal));
    }
    public void LaunchLocalTeleportation(Teleporter teleporter)
    {
        if (instance.isCurrentlyTeleporting) return;

        instance.localTeleporter = teleporter;
        Teleporter otherPortal = GetPortal();
        Debug.Log("Launch teleport local Sequence");
        StartCoroutine(instance.TeleportLocalSequence(otherPortal));
    }
    private IEnumerator TeleportLocalSequence(Teleporter otherTeleporter)
    {
        //var player = PlayerController.instance.playerObject;
        //if(player != null && player.GetComponent<CharacterController>()) player.GetComponent<CharacterController>().enabled = false;

        if (instance.localTeleporter != null)
        {
            yield return TeleportStarting(instance.localTeleporter.transitionType);
        }
        else
        {
            yield return TeleportStarting(SceneTransitionType.fade); //sometime we dont have local teleport. is you use a map
        }
        //MovePlayerLogic(otherTeleporter, player);

        yield return TeleportEnding(otherTeleporter.transitionType);

        /*
        if (player != null && player.GetComponent<CharacterController>()) player.GetComponent<CharacterController>().enabled = true;

        if (player != null)
        {
            PlayerController.instance.interactablePlayer.GetComponent<InteractablePlayerController>().interactingWithObject = false;
        }
        */
        yield return null;
    }

    private void MovePlayerLogic(Teleporter otherTeleporter, GameObject player)
    {
        PlayerSpawnLocation spawnlocation = otherTeleporter.playerSpawnLocation;
        GameObject camera = GameObject.Find("PlayerFollowCamera");
        if (camera == null) Debug.Log("Couldnt find PlayerFollowCamera when transitioning");
        camera.SetActive(false);

        //move player to correct location
        //PlayerController.instance.SetModelPostion(spawnlocation.spawnPoint);

        camera.SetActive(true); //to stop the pop
    }
    private Teleporter GetPortal(PortalKey portalKey)
    {
        foreach (Teleporter portal in FindObjectsOfType<Teleporter>())
        {
            if (portal.playerSpawnLocation.portalKey == portalKey)
            {
                return portal;
            }
        }
        return null;
    }
    private Teleporter GetPortal()
    {
        foreach (Teleporter portal in FindObjectsOfType<Teleporter>())
        {
            if (instance.localTeleporter != null)
            {
                //local
                if (portal.playerSpawnLocation == instance.localTeleporter.playerSpawnLocation)
                {
                    continue;
                }

                if (portal.playerSpawnLocation.portalKey != instance.localTeleporter.playerSpawnLocation.portalKey)
                {
                    continue;
                }
            }
            else
            {
                //scene
                if (portal.playerSpawnLocation.portalKey != instance.scenePortalKey)
                {
                    continue;
                }
            }

            return portal;
        }
        return null;
    }


    #endregion
    #region transtions
    private IEnumerator TeleportStarting(SceneTransitionType transitionType)
    {
        isCurrentlyTeleporting = true;
        //GameController.PauseGame(true);
        //transition
        yield return PlayTrantion(transitionType, false);
        yield return new WaitForFixedUpdate();
    }
    //helper
    private IEnumerator TeleportEnding(SceneTransitionType transitionType)
    {
        yield return new WaitForSeconds(0.25f);
        yield return PlayTrantion(transitionType, true);
        //GameController.UnPauseGame();
        isCurrentlyTeleporting = false;
    }

    public IEnumerator PlayTrantion(SceneTransitionType transitionType, bool open)
    {
        if (instance.transitionController.finished) //stop double play
        {
            instance.transitionController.PlayTransition(transitionType, open);
            yield return transitionController.FinishedTransitionCoroutine();
        }
        else
        {
            yield return null;
        }
    }
    #endregion
}
