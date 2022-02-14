using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEnemy : MonoBehaviour
{
    public GameObject bullet;
    public Transform flyingBUTT;
    Rigidbody2D enemyRB;
    private Renderer enemyRenderer;
    private BoxCollider2D enemyCollider;

    private int life = 1;

    public float attackRange = 15f;
    public float timeBetweenAttacks;
    public bool playerInAttackRange;
    public LayerMask whatIsPlayer;

    public float bulletSpeed = 2f;
    bool alreadyAttacked;

    [Header("Bouncing Settings")]
    Vector3 startPos;
    public float travelDistance;
    public float speed = 3f;

    void Awake()
    {
        enemyRB = gameObject.GetComponent<Rigidbody2D>();
        enemyRenderer = gameObject.GetComponent<Renderer>();
        enemyCollider = gameObject.GetComponent<BoxCollider2D>();
        startPos = transform.position;
    }

    void Update()
    {
        playerInAttackRange = Physics2D.OverlapCircle(transform.position, attackRange, whatIsPlayer);

        if (playerInAttackRange && LevelManager.gamestate == GameState.Game)
        {
            transform.position = new Vector3(transform.position.x, startPos.y + Mathf.PingPong(Time.time * speed, travelDistance), transform.position.z);
            enemyRB.velocity = -transform.right * speed;
            AttackPlayer();
        }
    }

    void AttackPlayer()
    {
        if (!alreadyAttacked)
        {
            GameObject bulletInstance = Instantiate(bullet, flyingBUTT.position, Quaternion.identity);
            bulletInstance.GetComponent<Rigidbody2D>().AddForce(-flyingBUTT.up * bulletSpeed, ForceMode2D.Impulse);
            alreadyAttacked = true;
            Invoke(nameof(Reset), timeBetweenAttacks + 2f);
        }
    }

    private void Reset()
    {
        alreadyAttacked = false;
    }

    void OnCollisionEnter2D(Collision2D coll)
    {

        if (coll.gameObject.CompareTag("FriendlyProjectiles"))
        {
            life -= 1;

            if (life <= 0)
            {
                Death();
            }
        }
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.CompareTag("FriendlyProjectiles"))
        {
            life -= 3;

            if (life <= 0)
            {
                Death();
            }
        }
    }

    void Death()
    {
        AudioSource audio = gameObject.GetComponent<AudioSource>();
        audio.PlayOneShot(PlayerController.playerControllerCS.clips[1]);
        enemyRenderer.enabled = false;
        enemyCollider.enabled = false;
        Destroy(gameObject, .01f);
        GameObject.Find("Player").GetComponent<PlayerController>().GetScore(5);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}