using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleScreenController : MonoBehaviour
{
    public enum TitleScreenState 
    {
        Idle,
        LoadingGame,
        Options,
        Controls
    }

    public TitleScreenState state;

    public ButtonSelectionBase titleMenu;
    public string GameScene;
    // Start is called before the first frame update
    public GameObject controlMenu;
    public void Awake()
    {
        titleMenu.SelectionAcceptedCallback.AddListener(() => TitleMenu_SelectionAcceptedCallback());

    }

    void Start()
    {
        GameAudioManager.instance.PlayTitleScreenBMG();
        titleMenu.BuildButtonList();
        state = TitleScreenState.Idle;
    }

    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            case TitleScreenState.Idle:
                titleMenu.HandleButtonCycle(InputManager.instance.move.y);
                titleMenu.HandleButtonInputs();
                break;
            case TitleScreenState.Controls:
                if(InputManager.instance.AcceptInputRequested() || InputManager.instance.CanceledInputRequested())
                {
                    controlMenu.SetActive(false);
                    titleMenu.gameObject.SetActive(true);
                    state = TitleScreenState.Idle;
                }
                break;
        }
    }
    public void TitleMenu_SelectionAcceptedCallback()
    {
        switch (titleMenu.selectedIndex)
        {
            case 0:
                //load game screen
                //PlayerController.instance.combatUnit.ResetStats(); // player controller doesnt continue to title screne
                SceneManagerController.instance.LaunchScene(GameScene, PortalKey.None, SceneTransitionType.fade);
                state = TitleScreenState.LoadingGame;
                break;
            case 1:
                break;
            case 2:
                controlMenu.SetActive(true);
                titleMenu.gameObject.SetActive(false);
                state = TitleScreenState.Controls;
                break;
            case 3:
                //exit
                Application.Quit();
                break;
        }
    }
}
