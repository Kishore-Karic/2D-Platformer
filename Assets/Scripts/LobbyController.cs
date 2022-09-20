using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class LobbyController : MonoBehaviour
{
    public GameObject levelSelection, mainMenu, settingMenu, controlsMenu,  resetMenu, quitMenu;
    public LevelManager levelManager;

    private int loadNextScene;

    private int sceneToContinue;

    private void Start()
    {
        mainMenu.SetActive(true); levelSelection.SetActive(false); settingMenu.SetActive(false); controlsMenu.SetActive(false); resetMenu.SetActive(false); quitMenu.SetActive(false);
        Time.timeScale = 1;
        AudioManager.instance.Play("IntroTheme");
        loadNextScene = SceneManager.GetActiveScene().buildIndex + 1;
    }

    public void PlayGame()
    {
        AudioManager.instance.Play("ButtonClick"); AudioManager.instance.Stop("IntroTheme");
        sceneToContinue = PlayerPrefs.GetInt("SavedScene");

        if (sceneToContinue != 0)
            SceneManager.LoadScene(sceneToContinue);
        else
            SceneManager.LoadScene(loadNextScene);
    }

    public void LevelSelect()
    {
        AudioManager.instance.Play("ButtonClick");
        levelSelection.SetActive(true);
        mainMenu.SetActive(false);
    }

    public void LevelCancel()
    {
        AudioManager.instance.Play("ButtonClick");
        levelSelection.SetActive(false);
        mainMenu.SetActive(true);
    }

    public void Settings()
    {
        AudioManager.instance.Play("ButtonClick");
        settingMenu.SetActive(true);
        mainMenu.SetActive(false);
    }

    public void SettingsCancel()
    {
        AudioManager.instance.Play("ButtonClick");
        settingMenu.SetActive(false);
        mainMenu.SetActive(true);
    }

    public void Controls()
    {
        AudioManager.instance.Play("ButtonClick");
        settingMenu.SetActive(false);
        controlsMenu.SetActive(true);
    }

    public void ControlsCancel()
    {
        AudioManager.instance.Play("ButtonClick");
        controlsMenu.SetActive(false);
        settingMenu.SetActive(true);
    }

    public void reset()
    {
        AudioManager.instance.Play("ButtonClick");
        mainMenu.SetActive(false);
        resetMenu.SetActive(true);
    }

    public void ResetOkay()
    {
        AudioManager.instance.Play("ButtonClick");
        levelManager.SetLevelStatus("Level 1", LevelStatus.Unlocked);
        levelManager.SetLevelStatus("Level 2", LevelStatus.Locked);
        levelManager.SetLevelStatus("Level 3", LevelStatus.Locked);
        levelManager.SetLevelStatus("Level 4", LevelStatus.Locked);
        levelManager.SetLevelStatus("Level 5", LevelStatus.Locked);
        PlayerPrefs.SetInt("SavedScene", 1);
        resetMenu.SetActive(false); mainMenu.SetActive(true);
    }

    public void ResetCancel()
    {
        AudioManager.instance.Play("ButtonClick");
        resetMenu.SetActive(false);
        mainMenu.SetActive(true);
    }

    public void Quit()
    {
        AudioManager.instance.Play("ButtonClick");
        mainMenu.SetActive(false);
        quitMenu.SetActive(true);
    }

    public void QuitOk()
    {
        AudioManager.instance.Play("ButtonClick");
        Application.Quit();
    }

    public void QuitCancel()
    {
        AudioManager.instance.Play("ButtonClick");
        quitMenu.SetActive(false);
        mainMenu.SetActive(true);
    }
}