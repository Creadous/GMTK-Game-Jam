using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombatActionController : MonoBehaviour
{
    public List<PlayerCombatActionButtons> playerActionButtons;
    public static PlayerCombatActionController instance;
    // Start is called before the first frame update
    void Start()
    {
        UpdateActionButtons();
    }

    private void Awake()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameController.IsGamePaused()) return;

        if (InputManager.instance.combat01)
        {
            InputManager.instance.combat01 = false;
            playerActionButtons[0].PressButton();
        }
        else if (InputManager.instance.combat02)
        {
            InputManager.instance.combat02 = false;
            playerActionButtons[1].PressButton();
        }
        else if (InputManager.instance.combat03)
        {
            InputManager.instance.combat03 = false;
            playerActionButtons[2].PressButton();
        }
        else if (InputManager.instance.combat04)
        {
            InputManager.instance.combat04 = false;
            playerActionButtons[3].PressButton();
        }
    }
    public void UpdateActionButtons()
    {
        for(int i = 0; i< playerActionButtons.Count; i++)
        {
            if(PlayerController.instance.combatUnit.combatStats.inventory[i] != null)
            {
                playerActionButtons[i].SetUp(PlayerController.instance.combatUnit.combatStats.inventory[i]);
            }
            else
            {
                playerActionButtons[i].ClearButton();
            }
        }
        
    }
}
