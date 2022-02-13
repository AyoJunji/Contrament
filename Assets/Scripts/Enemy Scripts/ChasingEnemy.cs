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
    private bool playerNearby = false;
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


    void Update()
    {
        playerInAttackRange = Physics2D.OverlapCircle(transform.position, attackRange, whatIsPlayer);

        Vector3 targ = PlayerController.playerPOS.transform.position;
        targ.z = 0f;

        Vector3 objectPos = transform.position;
        targ.x = targ.x - objectPos.x;
        targ.y = targ.y - objectPos.y;

        float angle = Mathf.Atan2(targ.y, targ.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

        if (playerInAttackRange && LevelManager.gamestate == GameState.Game)
        {
            enemyRB.velocity = -transform.right * speed;
        }

        if(playerNearby && LevelManager.gamestate == GameState.Game) {
            
        }
    }

    void Death()
    {
        gameObject.SetActive(false);

        // Add enemy death noise here

        Destroy(gameObject, .2f);
        GameObject.Find("Player").GetComponent<PlayerController>().GetScore(5);

    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}