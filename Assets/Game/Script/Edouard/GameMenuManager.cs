using System;
using UnityEngine;
using UnityEngine.UI;

public class GameMenuManager : MonoBehaviour
{
    #region Unity Variables
    [Header("MenuUi")]
    [SerializeField] GameObject mainMenu;
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
    [SerializeField] Button OpenSettingsButton;
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
        OpenSettingsButton.onClick.AddListener(OpenSettings);
        ReturnToMainMenuButton.onClick.AddListener(ReturnToMainMenu);
    }

    #region MainMenuFunctions
    void StartGame()
    {
        Debug.Log("Starting Game");
        gameState = GameState.InGame;
        mainMenu.SetActive(false);
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
        mainMenu.SetActive(true);
        settingsMenu.SetActive(false);
    }

    void OpenSettings()
    {
        Debug.Log("Opening Settings");
        gameState = GameState.MainMenu;
        settingsMenu.SetActive(true);
    }

    void CloseSettings()
    {
        Debug.Log("Closing Settings");
        settingsMenu.SetActive(false);
    }
    #endregion
}
