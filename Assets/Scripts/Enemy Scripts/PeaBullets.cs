using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeaBullets : MonoBehaviour
{
    [SerializeField] private GameObject bullet;
    [SerializeField] private Renderer bulletRenderer;
    Rigidbody2D bulletRB;

    float moveSpeed = 5f;
    Transform player;
    Vector2 moveDirection;

    void Start()
    {
        bulletRB = gameObject.GetComponent<Rigidbody2D>();
        player = GameObject.Find("Player").transform;
        moveDirection = (player.transform.position - transform.position).normalized * moveSpeed;
        bulletRB.velocity = new Vector2(moveDirection.x, moveDirection.y);
    }

    //Destroys bullets on impact with any collider
    void OnCollisionEnter2D(Collision2D coll)
    {
        bulletRenderer.enabled = false;

        //If adding bullet impact noise, add it here

        Destroy(bullet, .2f);
    }
}
