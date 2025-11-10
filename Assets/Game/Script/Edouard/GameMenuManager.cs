using System.Collections.Generic;
using UnityEngine;
using TMPro;
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
        ReturnToMainMenuButton.onClick.AddListener(ReturnToMenu);
    }

    #region MainMenuFunctions
    void StartGame()
    {
        gameState = GameState.InGame;
    }

    void ExitGame()
    {
        Application.Quit();
    }
    #endregion
    
    #region SettingsFunctions

    void ReturnToMenu()
    {
        gameState = GameState.MainMenu;
    }
    #endregion
}
