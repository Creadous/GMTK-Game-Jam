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
    }

    public TitleScreenState state;

    public ButtonSelectionBase titleMenu;
    public string GameScene;
    // Start is called before the first frame update

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
        }
    }
    public void TitleMenu_SelectionAcceptedCallback()
    {
        switch (titleMenu.selectedIndex)
        {
            case 0:
                //load game screen
                SceneManagerController.instance.LaunchScene(GameScene, PortalKey.None, SceneTransitionType.fade);
                state = TitleScreenState.LoadingGame;
                break;
            case 1:
                break;
            case 2:
                //exit
                Application.Quit();
                break;
        }
    }
}
