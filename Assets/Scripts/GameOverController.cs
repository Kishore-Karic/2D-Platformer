using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverController : MonoBehaviour
{
    public Button buttonRestart, buttonMainmenu;

    public GameObject lives, coins, knifes, pauseButton;

    private void Awake()
    {
        AudioManager.instance.Stop("GameTheme");
        AudioManager.instance.Play("GameOver");
        buttonRestart.onClick.AddListener(ReloadLevel);
        buttonMainmenu.onClick.AddListener(Mainmenu);
    }
    public void PlayerDied()
    {
        lives.SetActive(false);
        coins.SetActive(false);
        knifes.SetActive(false);
        pauseButton.SetActive(false);
        gameObject.SetActive(true);
    }

    public void ReloadLevel()
    {
        AudioManager.instance.Play("ButtonClick");
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.buildIndex);
        AudioManager.instance.Stop("GameOver");
    }

    public void Mainmenu()
    {
        AudioManager.instance.Play("ButtonClick");
        SceneManager.LoadScene(0);
        AudioManager.instance.Stop("GameTheme");
        AudioManager.instance.Stop("GameOver");
    }
}