using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameMenuManager : MonoBehaviour
{
    #region Unity Variables
    [Header("Dependencies")]
    public AudioSource ambienceAudioSourceReference;
    
    
    [Header("MenuUi")]
    [SerializeField] GameObject mainMenu;
    [SerializeField] GameObject settingsMenu;
    
    #region MainMenuButtons
    [Header("MainMenuButtons")]
    [SerializeField] Button openSettingsButton;
    [SerializeField] Button exitGameButton;
    #endregion
    
    #region SettingsMenuButtons
    [Header("SettingsMenuButtons")]
    [SerializeField] Button returnToMainMenuButton;
    [SerializeField] Slider ambiantSoundSlider;
    #endregion
    
    #endregion
    
    
    void Awake()
    {
        openSettingsButton.onClick.AddListener(OpenSettings);
        returnToMainMenuButton.onClick.AddListener(ReturnToMainMenu);
    }

    private void Update()
    {
        ChangeAmbienceVolume();
    }

    #region SettingsFunctions

    void ReturnToMainMenu()
    {
        Debug.Log("Returning to mainMenu");
        mainMenu.SetActive(true);
        settingsMenu.SetActive(false);
    }

    void OpenSettings()
    {
        Debug.Log("Opening Settings");
        settingsMenu.SetActive(true);
    }

    void ChangeAmbienceVolume()  
    {
        ambienceAudioSourceReference.volume = ambiantSoundSlider.value;
    }
    #endregion
}