using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionTrigger : MonoBehaviour
{
    private ICollisionHandler handler;

    private void Start()
    {
        handler = GetComponentInParent<ICollisionHandler>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        handler.CollisionEnter(collision.gameObject);
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        handler.CollisionExit(collision.gameObject);
    }
}
