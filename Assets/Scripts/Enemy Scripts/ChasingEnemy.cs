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
        enemyRB.velocity = -transform.right * speed;
    }

    void Death()
    {
        enemyCollider.enabled = false;
        enemyRenderer.enabled = false;

        // Add enemy death noise here

        Destroy(gameObject, .2f);
        GameObject.Find("Player").GetComponent<PlayerController>().GetScore(1000);

    }
}