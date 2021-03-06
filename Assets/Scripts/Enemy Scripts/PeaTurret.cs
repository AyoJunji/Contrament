using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeaTurret : MonoBehaviour
{
    private int life = 5;

    public float attackRange = 10f;
    public bool playerInAttackRange;
    public LayerMask whatIsPlayer;

    public float bulletSpeed = 10f;
    public float timeBetweenAttacks;
    bool alreadyAttacked;

    public Transform player;
    private Vector2 playerDirection;

    public Transform turretBarrel;
    public GameObject bullet;

    [SerializeField] private Renderer enemyRenderer;
    [SerializeField] private Collider2D enemyCollider;

    void Start()
    {
        enemyRenderer = gameObject.GetComponent<Renderer>();
        enemyCollider = gameObject.GetComponent<Collider2D>();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("FriendlyProjectiles"))
        {
            life -= 1;

            if (life <= 0)
            {
                BlowUp();
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
                BlowUp();
            }
        }
    }

    void Update()
    {
        playerInAttackRange = Physics2D.OverlapCircle(transform.position, attackRange, whatIsPlayer);

        Vector3 targ = player.transform.position;
        targ.z = 0f;

        Vector3 objectPos = transform.position;
        targ.x = targ.x - objectPos.x;
        targ.y = targ.y - objectPos.y;

        float angle = Mathf.Atan2(targ.y, targ.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

        if (playerInAttackRange)
        {
            AttackPlayer();
        }
    }

    void AttackPlayer()
    {
        if (!alreadyAttacked)
        {
            GameObject bulletInstance = Instantiate(bullet, turretBarrel.position, Quaternion.identity);

            alreadyAttacked = true;
            Invoke(nameof(Reset), timeBetweenAttacks + .75f);
        }
    }

    private void Reset()
    {
        alreadyAttacked = false;
    }

    void BlowUp()
    {
        //Add turret death noise here
        //Add turret explosion FX
        AudioSource audio = gameObject.GetComponent<AudioSource>();
        audio.PlayOneShot(PlayerController.playerControllerCS.clips[1]);
        enemyRenderer.enabled = false;
        enemyCollider.enabled = false;

        Destroy(gameObject, .2f);
        GameObject.Find("Player").GetComponent<PlayerController>().GetScore(5);

    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
