using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ButtonSelectionBase : MonoBehaviour
{
    [Header("UI elements")]
    public List<MenuButtonBase> buttonBaseList;
    public int selectedIndex = 0;

    [Header("Input delay")]
    public float delayAmount = 0.25f;
    public float currentDelay;

    [Header("Events")]
    public UnityEvent SelectionAcceptedCallback;
    public UnityEvent SelectionChangedCallback;

    public void BuildButtonList()
    {

        for(int i = 0; i< buttonBaseList.Count; i++)
        {
            buttonBaseList[selectedIndex].ToggleSelect(false);
        }
        if(buttonBaseList.Count != 0) buttonBaseList[selectedIndex].ToggleSelect(true); //if not empty list turn
    }

    void Update()
    {
        currentDelay = currentDelay - Time.deltaTime;
    }
    public void HandleButtonCycle(float move)
    {
        if (buttonBaseList.Count == 0) return; //no list no cycle;
        if (move != 0.0f && currentDelay < 0)
        {
            currentDelay = delayAmount;
            int direction = 0;
            if (move > 0.8f) direction = 1;
            if (move < -0.8f) direction = -1;

            buttonBaseList[selectedIndex].ToggleSelect(false);

            selectedIndex = UtilitesHelper.CycleNumber(selectedIndex, buttonBaseList.Count, direction * -1); //reversted

            buttonBaseList[selectedIndex].ToggleSelect(true);

            SelectionChangedCallback.Invoke();
            GameAudioManager.instance.playUIMovement.Invoke();
        }
    }
    public void HandleButtonInputs()
    {
        if (InputManager.instance.AcceptInputRequested())
        {
            //inputString = OptionText();
            SelectionAcceptedCallback.Invoke();
            GameAudioManager.instance.playUISelect.Invoke();
            //UIAudioController.Instance.playUISelect.Invoke();
        }
        /*
        else if(InputManager.instance.CanceledInputRequested())
        {
            GameAudioManager.instance.playUIBack.Invoke();
        }
        */
    }
    public void Restart()
    {
        /*
        if (buttonBackgrounds == null) return;

        for (int i = 0; i < buttonBackgrounds.Count; i++)
        {
            buttonBackgrounds[i].color = defaultColor;
            textColor = ColorConstantManager.GetDefualtColorText();
        }
        if (buttonBackgrounds.Count > 0)
        {
            buttonBackgrounds[0].color = selectColor;
            selectedIndex = 0;
        }
        */
    }
    public void ClearList()
    {

        for (int i = buttonBaseList.Count - 1; i > -1; i--)
        {
            GameObject.Destroy(buttonBaseList[i].gameObject);
        }
        buttonBaseList.Clear();
        selectedIndex = 0;
    }

    //helper function to remove button hight
    public void SetSelectionVisiblity(int index, bool selected)
    {
        /*
        if (buttonBackgrounds.Count > index)
        {
            if (selected)
            {
                buttonBackgrounds[index].color = selectColor;
            }
            else
            {
                buttonBackgrounds[index].color = defaultColor;
            }
        }
        */
    }
}
