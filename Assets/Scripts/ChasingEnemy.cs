using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChasingEnemy : MonoBehaviour
{
    [SerializeField] Renderer enemyRenderer;
    [SerializeField] BoxCollider2D enemyCollider;
    [SerializeField] private float speed = 3f;
    Rigidbody2D enemyRB;
    

    void Start()
    {
        enemyRB = GetComponent<Rigidbody2D>();
    }

    void OnCollisionEnter2D(Collision2D coll)
    {        

        if (coll.gameObject.CompareTag("FriendlyProjectiles"))
        {
            GameObject.Find("Player").GetComponent<PlayerController>().GetScore(1000);
            Death();
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
        Destroy(gameObject, .5f);
    }
}
