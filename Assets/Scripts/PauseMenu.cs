using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public GameObject lives, coins, knifes, pauseButton, pauseMenu, settingsMenu, controlsMenu;

    public void ResumeLevel()
    {
        AudioManager.instance.Play("ButtonClick");
        Time.timeScale = 1;
        pauseMenu.SetActive(false);
        AudioManager.instance.Play("GameTheme");
        lives.SetActive(true);
        coins.SetActive(true);
        knifes.SetActive(true);
        pauseButton.SetActive(true);
    }

    public void Mainmenu()
    {
        AudioManager.instance.Play("ButtonClick");
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
        AudioManager.instance.Stop("GameTheme");
    }
    public void Pausemenu()
    {
        AudioManager.instance.Play("ButtonClick");
        Time.timeScale = 0;
        pauseMenu.SetActive(true);
        lives.SetActive(false);
        coins.SetActive(false);
        knifes.SetActive(false);
        pauseButton.SetActive(false);
        AudioManager.instance.Stop("GameTheme");
    }
    public void SettingsMenu()
    {
        AudioManager.instance.Play("ButtonClick");
        pauseMenu.SetActive(false);
        settingsMenu.SetActive(true);
    }
    public void SettingsCancel()
    {
        AudioManager.instance.Play("ButtonClick");
        settingsMenu.SetActive(false);
        pauseMenu.SetActive(true);
    }

    public void ControlsMenu()
    {
        AudioManager.instance.Play("ButtonClick");
        settingsMenu.SetActive(false);
        controlsMenu.SetActive(true);
    }

    public void ControlsCancel()
    {
        AudioManager.instance.Play("ButtonClick");
        controlsMenu.SetActive(false);
        settingsMenu.SetActive(true);
    }
}
