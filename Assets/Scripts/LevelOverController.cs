using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelOverController : MonoBehaviour
{
    private int loadnextscene;
    public GameObject uiObject;

    private void Start()
    {
        uiObject.SetActive(false);
        loadnextscene = SceneManager.GetActiveScene().buildIndex + 1; 
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            uiObject.SetActive(true);
            Debug.Log("Level Completed");
            StartCoroutine("WaitForSec");
        }
    }

    IEnumerator WaitForSec()
    {
        yield return new WaitForSeconds(1.5f);
        Destroy(uiObject);
        SceneManager.LoadScene(loadnextscene);
        LevelManager.Instance.MarkcurrentLevelComplete();
    }
}