using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UImanager : MonoBehaviour
{
    private static UImanager instance;
    
    private void Start()
    {
        AudioManager.instance.Play("GameTheme");
    }

    public static UImanager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<UImanager>();
            }

            return instance;
        }
    }

    [SerializeField]
    private Transform lifeParent;

    [SerializeField]
    private GameObject lifePrefab;

    public Stack<GameObject> lives = new Stack<GameObject>();

    public void CreateLife(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            lives.Push(Instantiate(lifePrefab, lifeParent));
        }
    }

    public void RemoveLife()
    {
        Destroy(lives.Pop());
    }

    public void AddLife()
    {
        lives.Push(Instantiate(lifePrefab, lifeParent));
    }
}
