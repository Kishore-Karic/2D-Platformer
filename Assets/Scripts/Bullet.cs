using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public PlayerController playerController;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "NormalEnemy")
        {
            if(collision.gameObject != null)
            {
                collision.gameObject.GetComponent<AiController>().TakeDamage(playerController.attackDamage);
            }
            Destroy(gameObject);
        }
        if (collision.tag == "PatrolEnemy")
        {
            if (collision.gameObject != null)
            {
                collision.gameObject.GetComponent<PatrolAiController>().TakeDamage(playerController.attackDamage);
            }
            Destroy(gameObject);
        }
    }
}
