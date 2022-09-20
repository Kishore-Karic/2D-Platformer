using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiController : MonoBehaviour, ICollisionHandler
{
    [SerializeField]
    Transform player;

    [SerializeField]
    Animator animator;

    [SerializeField]
    float agroRange;

    [SerializeField]
    float playerRange;

    [SerializeField]
    float moveSpeed;

    public PlayerController playerController;

    private int curHealth;
    public int maxHealth;
    Rigidbody2D rb2d;
    public GroundCheck groundCheck;
    public GameObject damageArea;
    public float MyAttacktime { get; set; }
    public bool IsAttacking { get; set; }
    public bool InRange { get; set; }

    bool Alive;
    void Start()
    {
        curHealth = maxHealth;
        Alive = true;
        rb2d = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        if(!playerController.Alive)
        {
            animator.speed = 0;
        }
        if(playerController.Alive)
        {
            if (Alive)
            {
                float distToPlayer = Vector2.Distance(transform.position, player.transform.position);

                if (distToPlayer < agroRange && groundCheck.isGrounded == true)
                {
                    if (Vector2.Distance(transform.position, player.transform.position) > playerRange)
                    {
                        ChasePlayer();
                    }
                    else
                    {
                        StopChasePlayer();
                    }
                }
                else if (groundCheck.isGrounded == true)
                {
                    StopChasePlayer();
                }

                if (!IsAttacking)
                {
                    MyAttacktime += Time.deltaTime;
                }
                if (MyAttacktime >= 1 && !IsAttacking && InRange && player.GetComponent<PlayerController>().Alive)
                {
                    MyAttacktime = 0;
                    StartCoroutine("Attack");
                }

                if (groundCheck.isGrounded == false)
                {
                    animator.SetFloat("Speed", 0);
                    rb2d.velocity = new Vector2(0, -10);
                }
            }
        }
    }
    public void TakeDamage(int damage)
    {
        Debug.Log("Player attacked Enemy");
        animator.SetTrigger("Hurt");
        AudioManager.instance.Play("AttackZombie");
        curHealth -= damage;

        if (curHealth < 0)
        {
            Die();
        }
    }
    void Die()
    {
        Alive = !Alive;
        Debug.Log("Enemy Dead");
        animator.SetBool("Dead", true);
        AudioManager.instance.Play("ZombieDead");

        StartCoroutine("ienum");
    }
    IEnumerator ienum()
    {
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }
    void ChasePlayer()
    {
        AudioManager.instance.Play("ZombieRoar");
        if (transform.position.x < player.transform.position.x)
        {
            animator.SetFloat("Speed", moveSpeed);
            rb2d.velocity = new Vector2(moveSpeed, 0);
            transform.localScale = new Vector2(0.4f, 0.4f);
        }
        else
        {
            animator.SetFloat("Speed", moveSpeed);
            rb2d.velocity = new Vector2(-moveSpeed, 0);
            transform.localScale = new Vector2(-0.4f, 0.4f);
        }
    }
    void StopChasePlayer()
    {
        animator.SetFloat("Speed", 0);
        rb2d.velocity = new Vector2(0, 0);
    }
    public void CollisionEnter(GameObject other)
    {
        if(Alive)
        {
            if (other.gameObject.tag == "Player")
            {
                InRange = true;
                StartCoroutine("Attack");
                Debug.Log("Collide");
            }
        }
    }
    public void CollisionExit(GameObject other)
    {
        if(Alive)
        {
            if (other.gameObject.tag == "Player")
            {
                InRange = false;
                Debug.Log("Not Collide");
            }
        }
    }
    IEnumerator Attack()
    {
        MyAttacktime = 0;
        IsAttacking = true;

        if(Alive)
        {
            Debug.Log("Collide");
            animator.SetTrigger("Attack");
            Debug.Log("Enemy attacked Player");
        }

        player.GetComponent<PlayerController>().TakeHit();
        yield return new WaitForSeconds(1);

        IsAttacking = false;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "FallDetector")
        {
            Debug.Log("Enemy Dead");
            Destroy(gameObject);
        }
    }
}