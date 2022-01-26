using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Score Tracker")]
    private static int score;

    [Header("Speed & Forces")]
    private static float moveSpeed = 1.5f;
    private float maxSpeed = 7f;
    private float jumpForce = 10f;

    [Header("Lives")]
    private int playerLives = 3;

    [Header("Components")]
    Rigidbody2D playerRB;
    Renderer playerRenderer;

    [Header("Checks")]
    private BoxCollider2D collCheck;
    [SerializeField] private LayerMask jumpableGround;

    [Header("Inputs")]
    private float xInp;

    [Header("Bools & Logic")]
    private bool isInvincible = false;


    [Header("Timers")]
    [SerializeField] private float invincibilityDurationSeconds = 2f;

    void Start()
    {
        score = 0;
        playerRenderer = gameObject.GetComponent<Renderer>();
        playerRB = gameObject.GetComponent<Rigidbody2D>();
        collCheck = gameObject.GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        IsGrounded();
        ReadInputs();


        if (playerLives <= 0)
        {
            Death();
        }

        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded() && !Input.GetKey(KeyCode.S))
        {
            Jump();
        }
    }

    void FixedUpdate()
    {
        playerRB.AddForce(new Vector2(xInp * moveSpeed, 0), ForceMode2D.Impulse);
        if (playerRB.velocity.x > maxSpeed)
        {
            playerRB.velocity = new Vector2(maxSpeed, playerRB.velocity.y);
        }
        else if (playerRB.velocity.x < -maxSpeed)
        {
            playerRB.velocity = new Vector2(-maxSpeed, playerRB.velocity.y);
        }


    }

    void ReadInputs()
    {
        xInp = Input.GetAxisRaw("Horizontal");
    }

    private bool IsGrounded()
    {
        RaycastHit2D raycastHit2D = Physics2D.BoxCast(collCheck.bounds.center, collCheck.bounds.size, 0f, Vector2.down, .1f, jumpableGround);

        return raycastHit2D.collider != null;
    }

    void Jump()
    {
        playerRB.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("EnemyProjectiles") && !isInvincible)
        {
            playerLives -= 1;
            StartCoroutine(BecomeTemporarilyInvincible());
        }
    }

    void Death()
    {
        Destroy(gameObject, .2f);
    }

    private IEnumerator BecomeTemporarilyInvincible()
    {
        Debug.Log("Player turned invincible!");
        isInvincible = true;

        yield return new WaitForSeconds(invincibilityDurationSeconds);

        Debug.Log("Player is no longer invincible!");
        isInvincible = false;
    }
}