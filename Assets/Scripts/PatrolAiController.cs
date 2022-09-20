using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolAiController : MonoBehaviour
{
    [SerializeField]
    Transform player;

    public bool isFacingRight;

    public float speed = 0.8f;
    public float range = 3;
    float startingX;
    private int dir = 1;

    bool Alive;
    int curHealth;
    public int maxHealth;

    public PatrolAiAttackRange patrolAiAttackRange;

    public Animator animator;

    public GameObject bullet;
    public Transform bulletHole;
    float force = 600f;

    bool IsAttacking { get; set; }
    float MyAttacktime { get; set; }

    void Start()
    {
        MyAttacktime = 2;
        Alive = true;
        curHealth = maxHealth;
        startingX = transform.position.x;
    }
    void Update()
    {
        if (patrolAiAttackRange.TargetInRange == false)
        {
            Debug.Log("Player not in Range");
            Patrol();
        }
        else
        {
            Debug.Log("Player in Range");
            StopPatrol();
            if (!IsAttacking)
            {
                MyAttacktime += Time.deltaTime;
            }
            if (MyAttacktime >= 1.5f && !IsAttacking && player.GetComponent<PlayerController>().Alive)
            {
                MyAttacktime = 0;
                Shoot();
            }
        }

        if (transform.localScale.x < 0)
        {
            isFacingRight = false;
            Debug.Log("Left");
        }
        else if (transform.localScale.x > 0)
        {
            Debug.Log("Right");
            isFacingRight = true;
        }
    }

    void Patrol()
    {
        animator.SetFloat("Speed", 1);
        transform.Translate(Vector2.right * speed * Time.deltaTime * dir);
        if (transform.position.x < startingX || transform.position.x > startingX + range)
        {
            dir *= -1;
            transform.localScale = new Vector2(transform.localScale.x * -1, transform.localScale.y);
        }
    }

    void StopPatrol()
    {
        animator.SetFloat("Speed", 0);
        Debug.Log("Stop Patrol");
        transform.Translate(Vector2.right * 0 * Time.deltaTime);
    }
    public void TakeDamage(int damage)
    {
        Debug.Log("Player attacked Enemy");
        animator.SetTrigger("Hurt");
        AudioManager.instance.Play("AttackRobot");
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

        StartCoroutine("ienum");
    }
    IEnumerator ienum()
    {
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }

    private void Shoot()
    {
        if (Alive)
        {
            IsAttacking = true;
            animator.SetTrigger("Shoot");
            AudioManager.instance.Play("GunShot");
            if (isFacingRight)
            {
                GameObject go = Instantiate(bullet, bulletHole.position, Quaternion.Euler(new Vector3(0, 0, 0)));
                go.GetComponent<Rigidbody2D>().AddForce(Vector2.right * force);
                Destroy(go, 1.2f);
            }
            else
            {
                GameObject go = Instantiate(bullet, bulletHole.position, Quaternion.Euler(new Vector3(0, 0, -180)));
                go.GetComponent<Rigidbody2D>().AddForce(Vector2.left * force);
                Destroy(go, 1.2f);
            }
            IsAttacking = false;
        }
    }
}
