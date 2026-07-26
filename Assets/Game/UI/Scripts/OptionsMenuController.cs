using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class OptionsMenuController : MonoBehaviour
{
    [Header("volume setting")]
    public ButtonSelectionBase buttonSelection;
    public List<ScallerUI> VolumeControlls;
    public UnityEvent LeaveMenuCallback;
    public bool Init = false;
    private bool resetToDefault;
    public void Start()
    {
        buttonSelection.SelectionAcceptedCallback.AddListener(() => ButtonSelection_SelectAcceptedCallBack());
        buttonSelection.BuildButtonList();
    }
    private void ButtonSelection_SelectAcceptedCallBack()
    {
        ResetSetting();
    }
    public void SetUp()
    {
        if (Init == false)
        {
            SetVolumeSliders();
            Init = true;
        }
    }

    private void Update()
    {
        HandleInput();
    }

    private void HandleInput()
    {
        if (InputManager.instance.CanceledInputRequested())
        {
            LeaveMenu();
            return;
        }
        buttonSelection.HandleButtonCycle(InputManager.instance.move.y);
        buttonSelection.HandleButtonInputs();
        if (InputManager.instance.move.x != 0)
        {
            VolumeControlls[buttonSelection.selectedIndex].addValue(InputManager.instance.move.x / (float)100);
        }
    }
    public void ValueChangedVolume(int index)
    {
        if (Init == false)
        {
            return;
        }
        if (resetToDefault) //it going to ask to be reset
        {
            return;
        }
        switch (index)
        {
            case 0:
                AudioController.Instance.masterVolmue = VolumeControlls[0].slider.value * 100; //slider 0-1. value requires 0-100
                AudioController.Instance.UpdateBGMVolume();
                break;
            case 1:
                AudioController.Instance.BGMVolume = VolumeControlls[1].slider.value * 100;
                AudioController.Instance.UpdateBGMVolume();
                break;
            case 2:
                AudioController.Instance.SFXVolume = VolumeControlls[2].slider.value * 100;
                break;
                /*
            case 3:
                AudioController.Instance.characterVolume = VolumeControlls[3].slider.value * 100;
                break;
                */
        }
        SetPlayerPrefab();
    }
    private void SetPlayerPrefab()
    {
        PlayerPrefs.SetFloat("masterVolume", AudioController.Instance.masterVolmue);
        PlayerPrefs.SetFloat("backgroundMusicVolume", AudioController.Instance.BGMVolume);
        PlayerPrefs.SetFloat("SFXVolume", AudioController.Instance.SFXVolume);
        //PlayerPrefs.SetFloat("CharacterVoiceVolume", AudioController.Instance.characterVolume);
    }

    private void SetVolumeSliders()
    {
        VolumeControlls[0].SetUp(AudioController.Instance.masterVolmue);
        VolumeControlls[1].SetUp(AudioController.Instance.BGMVolume);
        VolumeControlls[2].SetUp(AudioController.Instance.SFXVolume);
        //VolumeControlls[3].SetUp(AudioController.Instance.characterVolume);
    }
    private void ResetSetting()
    {
        PlayerPrefs.DeleteAll();
        resetToDefault = true;

        AudioController.Instance.masterVolmue = 100;
        AudioController.Instance.BGMVolume = 100;
        AudioController.Instance.SFXVolume = 100;
        //AudioController.Instance.characterVolume = 100;

        SetVolumeSliders();

        SetPlayerPrefab();

        resetToDefault = false;
    }
    public void LeaveMenu()
    {
        SetPlayerPrefab();
        PlayerPrefs.Save();
        Init = false;
        LeaveMenuCallback.Invoke();
        GameAudioManager.instance.playUIBack.Invoke();
    }
}
