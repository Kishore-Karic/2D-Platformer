using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Assets.Script
{
    [RequireComponent(typeof(Button))]
    public class LevelLoader : MonoBehaviour
    {
        private Button button;

        public string LevelName;

        private void Awake()
        {
            button = GetComponent<Button>();
            button.onClick.AddListener(onClick);
        }

        private void onClick()
        {
            AudioManager.instance.Play("ButtonClick");
            LevelStatus levelStatus = LevelManager.Instance.GetLevelStatus(LevelName);
            switch (levelStatus)
            {
                case LevelStatus.Locked:
                    Debug.Log("can't paly till you unlock it!");
                    break;

                case LevelStatus.Unlocked:
                    SceneManager.LoadScene(LevelName);
                    AudioManager.instance.Stop("IntroTheme");
                    break;

                case LevelStatus.Completed:
                    SceneManager.LoadScene(LevelName);
                    AudioManager.instance.Stop("IntroTheme");
                    break;
            }
        }
    }
}