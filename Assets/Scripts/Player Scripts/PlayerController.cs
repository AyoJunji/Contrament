using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
public class PlayerController : MonoBehaviour
{
    [Header("Ammo Tracker")]
    public int ammo;

    [Header("Speed & Forces")]
    private static float moveSpeed = 5f;
   // private float maxSpeed = 4.5f;
    private float jumpForce = 15f;

    [Header("Lives")]
    private int playerLives = 3;

    [Header("Components")]
    Rigidbody2D playerRB;
    Renderer playerRenderer;
    Animator playerAnim;

    [Header("Checks")]
    [SerializeField] private BoxCollider2D collCheck;
    [SerializeField] private LayerMask jumpableGround;

    [Header("Inputs")]
    private Vector2 input;

    [Header("Bools & Logic")]
    private bool isInvincible = false;
    private bool isFacingRight = true;

    [Header("Timers")]
    [SerializeField] private float invincibilityDurationSeconds = 2f;

    void Start()
    {
        
        playerRenderer = gameObject.GetComponent<Renderer>();
        playerRB = gameObject.GetComponent<Rigidbody2D>();
        collCheck = gameObject.GetComponent<BoxCollider2D>();
        playerAnim = gameObject.GetComponent<Animator>();
        
    }

    void Update()
    {
        IsGrounded();
        ReadInputs();
        SetUIText();
        Debug.Log(IsGrounded());
        if (playerLives <= 0)
        {
            Death();
        }
        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded() && !Input.GetKey(KeyCode.S))
        {
            Jump();
        }
        if(Input.GetKey(KeyCode.Escape)) {
            SceneManager.LoadScene(0);
        }
    }

    void FixedUpdate()
    {
        
#region GUI MOVEMENT
        if(input.x != 0 ) 
        {
            transform.Translate(Vector3.right * input.x * moveSpeed * Time.deltaTime);
        }

        if(IsGrounded() && input.x != 0) 
        {
            playerAnim.SetBool("isRunning", true);
        }
        else
        {
            playerAnim.SetBool("isRunning", false);
        }

        if(!IsGrounded())
        {
           playerAnim.SetBool("isJumping", true);
        }
        else
           {
            playerAnim.SetBool("isJumping", false);

           }

#endregion
        
#region JUNJI MOVEMENT
        /* playerRB.AddForce(new Vector2(input.x * moveSpeed, 0), ForceMode2D.Impulse);
         if (playerRB.velocity.x > maxSpeed)
         {
             playerRB.velocity = new Vector2(maxSpeed, playerRB.velocity.y);
         }
         else if (playerRB.velocity.x < -maxSpeed)
         {
             playerRB.velocity = new Vector2(-maxSpeed, playerRB.velocity.y);
         }
         */
#endregion
        if (input.x > 0 && !isFacingRight)
        {
            FlipCharacter();
        }

        if (input.x < 0 && isFacingRight)
        {
            FlipCharacter();
        }
    }

    void ReadInputs()
    {
        input.x = Input.GetAxisRaw("Horizontal");
        input.y = Input.GetAxisRaw("Vertical");
        playerAnim.SetFloat("inputX", input.x);
        playerAnim.SetFloat("inputY", input.y);
    }

    private void FlipCharacter()
    {
        Vector3 currentScale = gameObject.transform.localScale;
        currentScale.x *= -1;
        gameObject.transform.localScale = currentScale;
        isFacingRight = !isFacingRight;
    }

    private bool IsGrounded()
    {
        RaycastHit2D raycastHit2D = Physics2D.BoxCast(collCheck.bounds.center, collCheck.bounds.size, 0f, Vector2.down, .1f, jumpableGround);

        return raycastHit2D.collider != null;
    }

    void Jump()
    {
        //Add Jumping noise right here
        playerRB.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("EnemyProjectiles") && !isInvincible)
        {
            // Put a sound here to signify player being hit

            playerLives -= 1;
            StartCoroutine(BecomeTemporarilyInvincible());
        }
    }

    public void LoseLife(int damage)
    {
        playerLives -= damage;
    }

    public void GetScore(int points)
    {
        ammo += points;
    }

    public void SetUIText()
    {
        //Change/update****
        TextMeshProUGUI scoreText = GameObject.Find("ScoreTracker").GetComponent<TextMeshProUGUI>();
        scoreText.text = "Ammo: <br>" + ammo.ToString();

        TextMeshProUGUI livesText = GameObject.Find("LivesTracker").GetComponent<TextMeshProUGUI>();
        livesText.text = "Lives: " + playerLives.ToString();
        //add ui "hearts"
    }

    void Death()
    {
        //Put player's death noise here
        //gameObject.SetActive(false);
        collCheck.enabled = false;
        playerRenderer.enabled = false;
        Destroy(gameObject, .2f);
    }

    private IEnumerator BecomeTemporarilyInvincible()
    {
        Debug.Log("Player turned invincible!");
        isInvincible = true;

        // IF POSSIBLE SOMEHOW MAKE THE PLAYER SPRITE BLINK SO IT SHOWS HIM BEING INVINCIBLE

        yield return new WaitForSeconds(invincibilityDurationSeconds);

        Debug.Log("Player is no longer invincible!");
        isInvincible = false;
    }
}