using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEnemy : MonoBehaviour
{
    public AnimationCurve myCurve;
    Rigidbody2D enemyRB;
    Renderer enemyRenderer;
    BoxCollider2D enemyCollider;

    void Awake()
    {
        enemyRB = gameObject.GetComponent<Rigidbody2D>();
        enemyRenderer = gameObject.GetComponent<Renderer>();
        enemyCollider = gameObject.GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        transform.position = new Vector3(transform.position.x,
        myCurve.Evaluate((Time.time % myCurve.length)), transform.position.z);
    }

    void OnCollisionEnter2D(Collision2D coll)
    {

        if (coll.gameObject.CompareTag("FriendlyProjectiles"))
        {
            GameObject.Find("Player").GetComponent<PlayerController>().GetScore(1000);
            Death();
        }
    }

    void FixedUpdate()
    {
        enemyRB.velocity = transform.right * 4;
    }

    void Death()
    {
        enemyRenderer.enabled = false;
        Destroy(gameObject, .2f);
    }
}