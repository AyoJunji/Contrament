using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChasingEnemy : MonoBehaviour
{
    [SerializeField] Renderer enemyRenderer;
    [SerializeField] BoxCollider2D enemyCollider;
    [SerializeField] private float speed = 3f;
    Rigidbody2D enemyRB;
    private int life = 2;
    public float attackRange = 10f;
    public bool playerInAttackRange;
    public LayerMask whatIsPlayer;


    void Start()
    {
        enemyRB = GetComponent<Rigidbody2D>();
        enemyCollider = GetComponent<BoxCollider2D>();
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


    void Update()
    {
        playerInAttackRange = Physics2D.OverlapCircle(transform.position, attackRange, whatIsPlayer);

        if (playerInAttackRange && LevelManager.gamestate == GameState.Game)
        {
            enemyRB.velocity = -transform.right * speed;
        }

    }

    void Death()
    {
        gameObject.SetActive(false);

        // Add enemy death noise here
        AudioSource audio = gameObject.GetComponent<AudioSource>();
        audio.PlayOneShot(PlayerController.playerControllerCS.clips[1]);
        Destroy(gameObject, .2f);
        GameObject.Find("Player").GetComponent<PlayerController>().GetScore(5);

    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}