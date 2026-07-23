using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    [Header("InputManager Variables")]
    public static InputManager instance;
	public CharacterInput playerInput;

	[Header("Player Input Values")]
	public Vector2 move;
	public Vector2 look;
	public bool accept;
	public bool cancel;
	public bool jump;
	public bool map;
	public bool interact;

	public bool tabLeft;
	public bool tabRight;

	[Header("Mouse Cursor Settings")]
	public bool cursorLocked = true;
	public bool cursorInputForLook = true;


	public virtual void Awake()
	{
		if (InputManager.instance == null)
		{
			instance = this;
			DontDestroyOnLoad(this.gameObject);
		}
		else
		{
			Destroy(this.gameObject);
		}

	}

	public virtual void OnEnable()
	{
		if (playerInput == null)
		{
			playerInput = new CharacterInput();

			playerInput.Player.Move.performed += i => instance.move = i.ReadValue<Vector2>();
			playerInput.Player.Move.canceled += i => instance.move = i.ReadValue<Vector2>();

			playerInput.Player.Look.performed += i => instance.look = i.ReadValue<Vector2>();
			playerInput.Player.Look.canceled += i => instance.look = i.ReadValue<Vector2>();

			playerInput.Player.Accept.performed += i => accept = true;
			playerInput.Player.Accept.canceled += i => accept = false;

			playerInput.Player.Cancel.performed += i => cancel = true;
			playerInput.Player.Cancel.canceled += i => cancel = false;

			playerInput.Player.Jump.performed += i => jump = true;
			playerInput.Player.Jump.canceled += i => jump = false;

			playerInput.Player.Map.performed += i => map = true;
			playerInput.Player.Map.canceled += i => map = false;

			playerInput.Player.Interact.performed += i => interact = true;
			playerInput.Player.Interact.canceled += i => interact = false;

			playerInput.Player.TabLeft.performed += i => tabLeft = true;
			playerInput.Player.TabLeft.canceled += i => tabLeft = false;

			playerInput.Player.TabRight.performed += i => tabRight = true;
			playerInput.Player.TabRight.canceled += i => tabRight = false;


		}
		playerInput.Enable();
	}
	public virtual void OnDisable()
	{
		if (playerInput != null)
		{
			playerInput.Disable();
		}
	}
	private void OnApplicationFocus(bool hasFocus)
	{
		SetCursorState(cursorLocked);
	}

	private void SetCursorState(bool newState)
	{
		Cursor.lockState = newState ? CursorLockMode.Locked : CursorLockMode.None;
	}

	public bool AcceptInputRequested()
	{
		if (accept)
		{
			accept = false;
			return true;
		}

		return false;
	}
	public bool CanceledInputRequested()
	{
		if (cancel)
		{
			cancel = false;
			return true;
		}

		return false;
	}
	public bool InteractInputRequested()
	{
		if (interact)
		{
			interact = false;
			return true;
		}

		return false;
	}
}
