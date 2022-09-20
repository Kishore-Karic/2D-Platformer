using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrateController : MonoBehaviour
{
    public PlayerController playerController;
    public GameObject health;
    public Transform healthPosition;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "PlayerAttackPoint")
        {
            if (playerController.isAttackingCrate)
            {
                playerController.isAttackingCrate = false;
                Instantiate(health, healthPosition.position, healthPosition.rotation);
                Destroy(gameObject);
            }
        }
    }
}
