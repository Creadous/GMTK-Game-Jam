using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class InteractablePlayerController : MonoBehaviour
{
    public PlayerAimController aimObject;
    public bool interactingWithObject;
    public IInteractable objectCurrentyInteractingWith;

    [Header("popUp")]
    public GameObject popUpUI;
    public Image icon;
    public TMP_Text popUpDescription;

    private void FixedUpdate()
    {
        if (GameController.IsGamePaused())
        {
            HidePopUp();
            return;
        }

        if(interactingWithObject == false)
        {
            HandleUI();
            HandleInput();

        }
        /*
        if(PlayerController.instance.movementController.Grounded == false)
        {
            popUpUI.SetActive(false);
        }
        */
    }
    public void HandleUI()
    {
        if (aimObject.selected != null && aimObject.selected.GetComponent<IInteractable>().AbleToInteractWith())
        {
            popUpUI.SetActive(true);
            popUpDescription.text = aimObject.selected.GetComponent<IInteractable>().InteracatDescription();
        }
        else
        {
            HidePopUp();
        }
    }
    public void HandleInput()
    {
        if (InputManager.instance.InteractInputRequested() && aimObject.selected != null)
        {
            if (aimObject.selected.GetComponent<IInteractable>().InteractWithObject(this))
            {
                interactingWithObject = true;
            }
        }
    }
    public void StopInteractingWithObject()
    {
        interactingWithObject = false;
    }
    public void HidePopUp()
    {
        popUpUI.SetActive(false);
    }
}
