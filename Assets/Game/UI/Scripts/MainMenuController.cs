using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuController : MonoBehaviour
{
    public ButtonSelectionBase buttonSelection;
    // Start is called before the first frame update
    private void Awake()
    {
        buttonSelection.SelectionAcceptedCallback.AddListener(() => MainMenuController_SelectionAcceptedCallback());
    }

    void Start()
    {
        buttonSelection.BuildButtonList();
    }

    private void MainMenuController_SelectionAcceptedCallback()
    {
        switch (buttonSelection.selectedIndex)
        {
            case 0:
                SceneManagerController.instance.LaunchTitleScene();
                GameController.instance.CloseMainMenu();
                break;
            case 1:
                break;
        }
    }


    // Update is called once per frame
    void Update()
    {
        HandleInput();
    }
    private void HandleInput()
    {
        buttonSelection.HandleButtonCycle(InputManager.instance.move.y);
        buttonSelection.HandleButtonInputs();

        if(InputManager.instance.cancel == true)
        {
            InputManager.instance.cancel = false;
            GameController.instance.CloseMainMenu();
        }
    }
}
