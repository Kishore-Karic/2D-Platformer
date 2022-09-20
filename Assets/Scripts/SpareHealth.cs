using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpareHealth : MonoBehaviour
{
    public PlayerController playerController;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            if(playerController.Lives < 7)
            {
                AudioManager.instance.Play("HealthPickUp");
                UImanager.Instance.AddLife();
                Destroy(gameObject);
            }
        }
    }
}
