using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverMenuController : MonoBehaviour
{
    public ButtonSelectionBase buttonSelection;
    public bool finishedWithMenu = false;
    // Start is called before the first frame update
    private void Awake()
    {
        buttonSelection.SelectionAcceptedCallback.AddListener(() => GameOverMenuController_SelectionAcceptedCallback());
    }

   
    void Start()
    {
        buttonSelection.BuildButtonList();
    }

    // Update is called once per frame
    void Update()
    {
        if(finishedWithMenu == false)
        {
            buttonSelection.HandleButtonInputs();
        }
    }
    private void GameOverMenuController_SelectionAcceptedCallback()
    {
        finishedWithMenu = true;
        GameObject.DestroyImmediate(this.gameObject);
        SceneManagerController.instance.LaunchTitleScene();

    }

}
