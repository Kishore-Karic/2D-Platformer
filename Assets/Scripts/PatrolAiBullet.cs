using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolAiBullet : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerController>().TakeBulletHit();
            Destroy(gameObject);
        }
    }
}
