using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VictoryMenuController : MonoBehaviour
{
    public string GameScene;
    public ButtonSelectionBase buttonSelection;
    public bool finishedWithMenu = false;
    // Start is called before the first frame update
    private void Awake()
    {
        buttonSelection.SelectionAcceptedCallback.AddListener(() => VictoryMenuController_SelectionAcceptedCallback());
    }


    void Start()
    {
        buttonSelection.BuildButtonList();
        GameController.PauseGame(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (finishedWithMenu == false)
        {
            buttonSelection.HandleButtonCycle(InputManager.instance.move.y);
            buttonSelection.HandleButtonInputs();
        }
    }
    private void VictoryMenuController_SelectionAcceptedCallback()
    {
        switch (buttonSelection.selectedIndex)
        {
            case 0:
                GameObject.DestroyImmediate(this.gameObject);
                SceneManagerController.instance.LaunchScene(GameScene, PortalKey.None, SceneTransitionType.fade);
                break;
            case 1:
                GameObject.DestroyImmediate(this.gameObject);
                SceneManagerController.instance.LaunchTitleScene();
                break;
        }
        finishedWithMenu = true;


    }

}
