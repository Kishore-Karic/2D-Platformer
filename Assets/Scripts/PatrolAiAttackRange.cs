using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolAiAttackRange : MonoBehaviour
{
    public bool TargetInRange;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            TargetInRange = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            TargetInRange = false;
        }
    }
}
