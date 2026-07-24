using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController instance;
    [SerializeField] private bool paused;

    [Header("GameOverScreen")]
    public GameObject gameOverScreenPrefab;
    private GameOverMenuController gameOverController;


    public CharacterData characterData;

    [Header("Canvas")]
    public Transform Canvas;

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
}
