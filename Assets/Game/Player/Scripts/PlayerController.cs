using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;
    private DungeonCrawlerMovment movment;
    private InteractablePlayerController interactablePlayer;
    private void Awake()
    {
        instance = this;
        movment = GetComponent<DungeonCrawlerMovment>();
        interactablePlayer = GetComponent<InteractablePlayerController>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GameController.IsGamePaused() == true) return;
        HandleMovement();


        //stamina damage
        if (movment.hasMoved)
        {
            movment.hasMoved = false;
            GameController.instance.characterData.UpdateCurrentStamina(-1);
        }

        CheckStamina();
    }
    void HandleMovement()
    {
        if (InputManager.instance.move.x> 0)
        {
            //right
            movment.RotateRight();
        }
        else if(InputManager.instance.move.x < 0)
        {
            //left
            movment.RotateLeft();
        }
        else if(InputManager.instance.move.y > 0)
        {
            //forwards
            movment.MoveForward();
        }
        else if(InputManager.instance.move.y < 0)
        {
            //backwards
            movment.MoveBackwards();
        }
    }
    public void SetModelPostion(Transform newTransform)
    {
        //movementController.controller.enabled = false;
        this.transform.position = newTransform.position;
        this.transform.rotation = newTransform.rotation;// Quaternion.Euler(newTransform.rotation.x, newTransform.rotation.y, newTransform.rotation.z);
        //movementController.controller.enabled = true;
    }
    public void ResetInteractablePlayer()
    {
        interactablePlayer.StopInteractingWithObject();
        interactablePlayer.HidePopUp();
    }
    private void CheckStamina()
    {
        if( GameController.instance.characterData.GetCurrentStamina() == 0)
        {
            GameController.instance.LaunchGameOverScreen();
        }
    }
}
