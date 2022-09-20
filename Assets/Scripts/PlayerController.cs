using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public Animator animator;
    public Raycasting rayCast;

    public GameOverController gameOverController;
    public KnifeUIController knifeUIController;
    public ScoreController scoreController;

    private float dirX, dirY;
    public float moveSpeed, climbingSpeed;
    public float jump;
    public bool isFacingRight;
    public bool inLadder;

    private Rigidbody2D rb2d;

    public GroundCheck groundCheck;

    public Transform attackPoint;
    public LayerMask enemyLayer;

    public float attackRange = 0.5f;
    public int attackDamage = 60;

    public float attackRate = 2f;
    float nextAttackTime = 0f;
    public float nextShootTime = 0f;

    public GameObject bullet;
    public Transform bulletHole;
    public float force = 600f;

    public bool ClimbingAllowed { get; set; }

    [SerializeField]
    public int Lives = 6;
    public bool isAttackingCrate;

    void Start()
    {
        isFacingRight = true;
        Alive = true;
        UImanager.Instance.CreateLife(Lives);
    }
    private void Awake()
    {
        Debug.Log("Player Controller Awake");
        rb2d = gameObject.GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        if(rayCast.hitG == true)
        {
            animator.SetBool("Glide", true);
        }

        dirX = Input.GetAxisRaw("Horizontal");
        dirY = Input.GetAxisRaw("Vertical");

        if(groundCheck.isGrounded == true)
        {
            animator.SetBool("Glide", false);
            animator.SetFloat("MoveSpeed", 0);
        }

        if (Input.GetKeyDown(KeyCode.Space) && groundCheck.isGrounded == true)
        {
            Jump();
        }

        MoveCharacterHorizontal(dirX);
        PlayMovementAnimationHorizontal(dirX);

        if (Time.time >= nextAttackTime)
        {
            if (Input.GetKeyDown(KeyCode.RightControl))
            {
                Attack();
                nextAttackTime = Time.time + 1f / attackRate;
            }
        }

        if (Time.time >= nextShootTime)
        {
            if (Input.GetKeyDown(KeyCode.RightAlt))
            {
                Shoot();
                nextShootTime = Time.time + 1f / 1.5f;
            }
        }

        if (ClimbingAllowed)
        {
            MoveCharacterVertical(dirY);
            PlayMovementAnimationVertical();
        }
    }
    private void FixedUpdate()
    {
        if(ClimbingAllowed)
        {
            inLadder = true;
            animator.speed = dirY != 0 ? Mathf.Abs(dirY) : Mathf.Abs(dirX);
            rb2d.isKinematic = true;
            rb2d.velocity = new Vector2(dirX, dirY);
        }
        else
        {
            inLadder = false;
            rb2d.isKinematic = false;
            animator.SetBool("Climb", false);
            animator.SetFloat("MoveSpeed", 0);
            rb2d.velocity = new Vector2(dirX, rb2d.velocity.y);
        }
    }
    public int life() { return Lives; }
    public bool Alive { get; set; }
    public void TakeHit()
    {
        if (Lives > 0)
        {
            Alive = true;
            StartCoroutine("TimeX");   
        }
    }
    IEnumerator TimeX()
    {
        yield return new WaitForSeconds(1f);

        animator.SetTrigger("Hurt");
        UImanager.Instance.RemoveLife();
        Lives--;
        if (Lives <= 0)
        {
            Alive = false;
            animator.SetBool("Die", true);
            StartCoroutine("ienum");
        }
    }
    IEnumerator ienum()
    {
        yield return new WaitForSeconds(1.5f);
        gameOverController.PlayerDied();
    }
    public void TakeBulletHit()
    {
        animator.SetTrigger("Hurt");
        UImanager.Instance.RemoveLife();
        Lives--;

        if(Lives <= 0)
        {
            Alive = false;
            animator.SetBool("Die", true);
            StartCoroutine("BulletHit");
        }
    }
    IEnumerator BulletHit()
    {
        yield return new WaitForSeconds(1.5f);
        gameOverController.PlayerDied();
    }
    private void MoveCharacterHorizontal(float dirX)
    {
        if(Alive)
        {
            //player horizontal movement
            Vector3 position = transform.position;
            position.x += dirX * moveSpeed * Time.deltaTime;
            transform.position = position;
        }
    }
    private void MoveCharacterVertical(float dirY)
    {
        if (Alive)
        {
            //player vertical movement
            Vector3 position = transform.position;
            position.y += dirY * climbingSpeed * Time.deltaTime;
            transform.position = position;
        }
    }
    private void Jump()
    {
        if(Alive)
        {
            animator.SetTrigger("Jump");
            rb2d.velocity = new Vector2(rb2d.velocity.x, jump);
        }
    }
    private void PlayMovementAnimationHorizontal(float dirX)
    {
        if(Alive)
        {
            //Run
            animator.SetFloat("MoveSpeed", Mathf.Abs(dirX));

            Vector3 scale = transform.localScale;
            if (dirX < 0)
            {
                isFacingRight = false;
                Debug.Log("Left");
                scale.x = -1f * Mathf.Abs(scale.x);
            }
            else if (dirX > 0)
            {
                Debug.Log("Right");
                isFacingRight = true;
                scale.x = Mathf.Abs(scale.x);
            }
            transform.localScale = scale;
        }
    }
    private void PlayMovementAnimationVertical()
    {
        if(Alive)
        {
            if(Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.DownArrow))
            {
                animator.speed = 1;
                animator.SetBool("Climb", true);
            }
            else if(groundCheck.isGrounded)
            {
                animator.speed = 1;
                animator.SetBool("Climb", false);
            }
            else
            {
                animator.speed = 0;
            }
        }
    }
    private void Attack()
    {
        if(Alive)
        {
            animator.SetTrigger("Attack");

            Collider2D[] swordHit = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayer);
            
            foreach (Collider2D Enemy in swordHit)
            {
                if(Enemy.tag == "NormalEnemy")
                {
                    if (Enemy != null)
                    {
                        Enemy.GetComponentInParent<AiController>().TakeDamage(attackDamage);
                    }
                }
                if(Enemy.tag == "PatrolEnemy")
                {
                    if (Enemy != null)
                    {
                        Enemy.GetComponentInParent<PatrolAiController>().TakeDamage(attackDamage);
                    }
                }
                if(Enemy.tag == "Crate")
                {
                    AudioManager.instance.Play("CrateDestroy");
                    isAttackingCrate = true;
                }
            }
        }
    }
    public void Shoot()
    {
        if (Alive)
        {
            if (knifeUIController.knifeCount > 0 && !inLadder)
            {
                animator.SetTrigger("Shoot");
                AudioManager.instance.Play("Knife");
                if (isFacingRight)
                {
                    GameObject go = Instantiate(bullet, bulletHole.position, Quaternion.Euler(new Vector3(0, 0, -90)));
                    go.GetComponent<Rigidbody2D>().AddForce(Vector2.right * force);
                    Destroy(go, 1.2f);
                }
                else
                {
                    GameObject go = Instantiate(bullet, bulletHole.position, Quaternion.Euler(new Vector3(0, 0, 90)));
                    go.GetComponent<Rigidbody2D>().AddForce(Vector2.left * force);
                    Destroy(go, 1.2f);
                }
                UsingKnife();
            }
        }
    }
    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "FallDetector")
        {
            gameObject.SetActive(false);
            gameOverController.PlayerDied();
        }
    }
    public void PickUpCoin()
    {
        Debug.Log("Player Picked up the Coin");
        scoreController.IncreaseScore(1);
    }
    public void UsingKnife()
    {
        Debug.Log("Used a Knife");
        knifeUIController.DecreaseKnifeCount(1);
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "SaveScene")
        {
            PlayerPrefs.SetInt("SavedScene", SceneManager.GetActiveScene().buildIndex);
        }
    }
}
