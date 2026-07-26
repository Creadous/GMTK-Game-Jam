using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController instance;
    [SerializeField] private bool paused;

    [Header("ShopMenu")]
    public GameObject mainMenuPrefab;
    private MainMenuController mainMenuController;

    [Header("GameOverScreen")]
    public GameObject gameOverScreenPrefab;
    private GameOverMenuController gameOverController;

    [Header("LootDropMenu")]
    public GameObject lootMenuPrefab;
    private LootDropMenuController lootDropMenuController;

    [Header("DialogueSystem")]
    public GameObject dialogueSystemPrefab;
    private SimpleDialogueSystem dialogueSystem;

    [Header("ShopMenu")]
    public GameObject shopMenuPrefab;
    private ShopMenuController shopmenu;

    [Header("Canvas")]
    public Transform Canvas;

    [Header("ItemSwapMenu")]
    public GameObject itemSwapMenuPrefab;
    private ItemSwapMenuController itemSwapMenuController;

    private void Awake()
    {
        if (instance == null)
        {
            DontDestroyOnLoad(this.gameObject);
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }
    #region Pause logic
    public static bool IsGamePaused()
    {
        return instance.paused;
    }
    public static void PauseGame(bool isGameplayTimerPaused)
    {
        instance.paused = true;
        //Debug.Log("Paused set on GameController instance: " + instance.GetInstanceID());
    }
    public static void UnPauseGame()
    {
        instance.paused = false;
    }
    #endregion

    public void LaunchGameOverScreen()
    {
        if (gameOverController == null)
        {
            PauseGame(true);
            GameObject gameOverObject = Instantiate(gameOverScreenPrefab, Canvas);
            gameOverController = gameOverObject.GetComponent<GameOverMenuController>();
            //gameOverController.SetUp();
            //SaveGame();
        }
    }
    #region main menu
    public void LaunchMainMenu()
    {
        GameAudioManager.instance.PlaySoundEffect("menu_open");
        PauseGame(true);
        GameObject mainmenuObject = Instantiate(mainMenuPrefab, Canvas);
        mainMenuController = mainmenuObject.GetComponent<MainMenuController>();
    }
    public void CloseMainMenu()
    {
        GameAudioManager.instance.PlaySoundEffect("menu_close");
        UnPauseGame();
        GameObject.Destroy(mainMenuController.gameObject);
    }
    #endregion

    #region treasure
    public void OpenLootDropMenu(List<ItemStatsBase> loot, Vector2Int goldRange)
    {
        PauseGame(true);
        var menuObject = Instantiate(lootMenuPrefab, Canvas);
        lootDropMenuController = menuObject.GetComponent<LootDropMenuController>();
        lootDropMenuController.SetUp(loot, goldRange);
    }
    public void CloseLootDropMenu()
    {
        Destroy(lootDropMenuController.gameObject);
        UnPauseGame();
    }
    #endregion

    #region Dialogue
    public void LaunchDialogueSystem(Dialogue dialogue, InteractableObject_NPC talker)
    {
        PauseGame(false);
        var menuObject = Instantiate(dialogueSystemPrefab, Canvas);
        dialogueSystem = menuObject.GetComponent<SimpleDialogueSystem>();
        dialogueSystem.SetUp(dialogue, talker);
    }
    public void CloseDialogueSystem()
    {
        GameObject.Destroy(dialogueSystem.gameObject);
        UnPauseGame();
    }
    #endregion

    #region shop
    public void LaunchShopMenu(InteractableObject_NPC talker)
    {
        var menuObject = Instantiate(shopMenuPrefab, Canvas);
        shopmenu = menuObject.GetComponent<ShopMenuController>();
        shopmenu.SetUp(talker);
    }
    
    public void CloseShoppingMenu()
    {
        GameObject.Destroy(shopmenu.gameObject);
    }
    #endregion
}
