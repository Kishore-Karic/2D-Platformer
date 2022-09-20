using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    private static LevelManager instance;
    public static LevelManager Instance { get { return instance; } }

    public string[] Levels;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        } else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        if (GetLevelStatus(Levels[0]) == LevelStatus.Locked)
        {
            SetLevelStatus(Levels[0], LevelStatus.Unlocked);
        }
    }

    public void MarkcurrentLevelComplete()
    {
        Scene currentscene = SceneManager.GetActiveScene();

        // set Level status to completed
        SetLevelStatus(currentscene.name, LevelStatus.Completed);

        // unlock next Level
        int currentsceneIndex = Array.FindIndex(Levels, level => level == currentscene.name);
        int nextsceneIndex = currentsceneIndex + 1;
        if(nextsceneIndex < Levels.Length)
        {
            SetLevelStatus(Levels[nextsceneIndex], LevelStatus.Unlocked);
        }
    }
    public LevelStatus GetLevelStatus(string level)
    {
        LevelStatus levelStatus = (LevelStatus) PlayerPrefs.GetInt(level, 0);
        return levelStatus;
    }

    public void SetLevelStatus(string level, LevelStatus levelStatus)
    {
        PlayerPrefs.SetInt(level, (int)levelStatus);
        Debug.Log("Setting Level: " + level + "Status: " + levelStatus);
    }
}