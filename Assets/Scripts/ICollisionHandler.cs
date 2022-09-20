using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICollisionHandler
{
    void CollisionEnter(GameObject other);

    void CollisionExit(GameObject other);
}
