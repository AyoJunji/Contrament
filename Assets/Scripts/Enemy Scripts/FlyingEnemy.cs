using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEnemy : MonoBehaviour
{
    public AnimationCurve myCurve;
    Rigidbody2D enemyRB;
    private Renderer enemyRenderer;
    private BoxCollider2D enemyCollider;
    private int life = 1;
    public float attackRange = 10f;
    public bool playerInAttackRange;
    public LayerMask whatIsPlayer;

    void Awake()
    {
        enemyRB = gameObject.GetComponent<Rigidbody2D>();
        enemyRenderer = gameObject.GetComponent<Renderer>();
        enemyCollider = gameObject.GetComponent<BoxCollider2D>();
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
            transform.position = new Vector3(transform.position.x,
            myCurve.Evaluate((Time.time % myCurve.length)), transform.position.z);
        }
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

    void FixedUpdate()
    {
        enemyRB.velocity = transform.right * -4;
    }

    void Death()
    {
        enemyRenderer.enabled = false;
        enemyCollider.enabled = false;
        Destroy(gameObject, .01f);
        GameObject.Find("Player").GetComponent<PlayerController>().GetScore(3000);
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}