using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChasingEnemy : MonoBehaviour
{
    [SerializeField] Renderer enemyRenderer;
    [SerializeField] BoxCollider2D enemyCollider;
    [SerializeField] private float speed = 3f;
    Rigidbody2D enemyRB;
    

    void OnCollisionEnter2D(Collision2D coll)
    {
        PlayerController getScript = GetComponent<PlayerController>();        
        if (coll.gameObject.CompareTag("Player"))
        {
            getScript.LoseLife(1);
            getScript.GetScore(1000);
        }

        if (coll.gameObject.CompareTag("FriendlyProjectiles"))
        {
            Death();
        }
    }

    void Start()
    {
        enemyRB = GetComponent<Rigidbody2D>();
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
