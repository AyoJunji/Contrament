using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretBullet : MonoBehaviour
{
    [SerializeField] private float speed = 20f;
    private Transform player;
    private Vector2 target;
    Rigidbody2D bulletRB;

    void Start()
    {
        bulletRB = gameObject.GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        target = new Vector2(player.position.x, player.position.y);
    }

    void Update()
    {
        bulletRB.AddForce((player.position - transform.position).normalized * speed * Time.smoothDeltaTime);
    }
}