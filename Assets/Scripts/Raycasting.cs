using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Raycasting : MonoBehaviour
{
    public bool hitG;
    public GroundCheck groundCheck;

    void Start()
    {
        hitG = true;
    }
    // Update is called once per frame
    void Update()
    {
        RaycastHit2D hitGround = Physics2D.Raycast(transform.position, -Vector2.up);
        
        Debug.DrawRay(transform.position, -Vector2.up * hitGround.distance, Color.red);

        if(hitGround.distance > 10)
        {
            hitG = true;
        }
        if(groundCheck.isGrounded == true)
        {
            hitG = false;
        }
    }
}
