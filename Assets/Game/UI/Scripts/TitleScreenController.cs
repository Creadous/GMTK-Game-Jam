using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
public class TitleScreenController : MonoBehaviour
{
    public enum TitleScreenState 
    {
        VidoePlaying,
        Idle,
        LoadingGame,
        Options,
        Controls
    }

    public TitleScreenState state;

    public ButtonSelectionBase titleMenu;
    public string GameScene;
    public Canvas canvas;

    public VideoPlayer videoPlayer;
    // Start is called before the first frame update
    public GameObject controlMenu;
    public OptionsMenuController optionsMenu;
    public void Awake()
    {
        titleMenu.SelectionAcceptedCallback.AddListener(() => TitleMenu_SelectionAcceptedCallback());
        optionsMenu.LeaveMenuCallback.AddListener(() => TitleMenu_LeaveMenuCallback());

    }

   

    void Start()
    {
        videoPlayer.source = VideoSource.Url;

        videoPlayer.url =
            Application.streamingAssetsPath +
            "/Video/Path of Ruin Intro Sequence Cut w No Audio.mp4";

        videoPlayer.prepareCompleted += OnVideoPrepared;

        videoPlayer.Prepare();

        videoPlayer.SetDirectAudioMute(0, true);
        videoPlayer.loopPointReached += VideoEnded; //call when its finished
        videoPlayer.started += VideoStarted; //call when its finished
       // videoPlayer.Play();

        titleMenu.BuildButtonList();
        state = TitleScreenState.VidoePlaying;
        
    }
    void OnVideoPrepared(VideoPlayer vp)
    {
        GameAudioManager.instance.PlayTitleScreenBMG();
        vp.Play();
    }
    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            case TitleScreenState.VidoePlaying:
                if (InputManager.instance.AcceptInputRequested())
                {
                    VideoEnded(videoPlayer);
                }
                break;
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
                optionsMenu.gameObject.SetActive(true);
                optionsMenu.SetUp();
                state = TitleScreenState.Options;
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
    private void TitleMenu_LeaveMenuCallback()
    {
        optionsMenu.gameObject.SetActive(false);
        state = TitleScreenState.Idle;
    }
    void VideoStarted(VideoPlayer vp)
    {
        
    }
    void VideoEnded(VideoPlayer vp)
    {
        Debug.Log("Video is finished!");
        canvas.gameObject.SetActive(true);
        videoPlayer.gameObject.SetActive(false);
        state = TitleScreenState.Idle;
    }
}
