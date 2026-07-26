using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class ConfirmHud : MonoBehaviour
{
    public TMP_Text MessageText;
    public ButtonSelectionBase buttonSelection;
    public bool finishedWithMenu = false;
    public bool chooseYes = false;
    // Start is called before the first frame update
    void Start()
    {
        buttonSelection.SelectionAcceptedCallback.AddListener(() => ConfirmHud_SelectionAcceptedCallback());
        buttonSelection.BuildButtonList();
    }

    public void SetUp(string description)
    {
        MessageText.text = description;
    }

    // Update is called once per frame
    void Update()
    {
        if(finishedWithMenu == false)
        {
            HandleInput();
        }
    }
    public void HandleInput()
    {
        buttonSelection.HandleButtonCycle(InputManager.instance.move.x * -1);
        buttonSelection.HandleButtonInputs();
    }

    private void ConfirmHud_SelectionAcceptedCallback()
    {
        if(buttonSelection.selectedIndex == 0)
        {
            chooseYes = true;
        }
        finishedWithMenu = true;
    }
}
