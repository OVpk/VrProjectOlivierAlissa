using System;
using UnityEngine;
using UnityEngine.UI;

public class GameMenuManager : MonoBehaviour
{
    #region Unity Variables
    [Header("MenuUi")]
    [SerializeField] GameObject gameMenu;
    [SerializeField] GameObject settingsMenu;

    enum GameState
    {
        MainMenu,
        InGame 
    };

    GameState gameState;

    #region MainMenuButtons
    [Header("MainMenuButtons")]
    [SerializeField] Button StartGameButton;
    [SerializeField] Button ExitGameButton;
    #endregion
    
    #region SettingsMenuButtons
    [Header("SettingsMenuButtons")]
    [SerializeField] Button ReturnToMainMenuButton;
    #endregion
    
    #endregion
    
    
    void Awake()
    {
        StartGameButton.onClick.AddListener(StartGame);
        ExitGameButton.onClick.AddListener(ExitGame);
        ReturnToMainMenuButton.onClick.AddListener(ReturnToMainMenu);
    }

    #region MainMenuFunctions
    void StartGame()
    {
        Debug.Log("Starting Game");
        gameState = GameState.InGame;
    }

    void ExitGame()
    {
        Debug.Log("Exiting Game");
        Application.Quit();
    }
    #endregion
    
    #region SettingsFunctions

    void ReturnToMainMenu()
    {
        Debug.Log("Returning to mainMenu");
        gameState = GameState.MainMenu;
    }
    #endregion

    private void Update()
    {
        if (gameState == GameState.InGame)
        {
            
        }
        else if (gameState == GameState.MainMenu)
        {
            
        }
    }
}
