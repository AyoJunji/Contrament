using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEnemy : MonoBehaviour
{
    public AnimationCurve myCurve;
    Rigidbody2D enemyRB;
    [SerializeField] private Renderer enemyRenderer;
    [SerializeField] private BoxCollider2D enemyCollider;
    private int life = 1;
    private bool playerNearby;

    void Awake()
    {
        enemyRB = gameObject.GetComponent<Rigidbody2D>();
        enemyRenderer = gameObject.GetComponent<Renderer>();
        enemyCollider = gameObject.GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        if(playerNearby) {
            transform.position = new Vector3(transform.position.x,
            myCurve.Evaluate((Time.time % myCurve.length)), transform.position.z);
        }
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Player")){
            playerNearby = true;
        }
    }
    private void OnTriggerExit2D(Collider2D other) {
        if(other.CompareTag("Player")){
            playerNearby = false;
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
        GameObject.Find("Player").GetComponent<PlayerController>().GetScore(50);
    }
}