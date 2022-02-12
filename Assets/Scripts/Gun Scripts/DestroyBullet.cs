using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyBullet : MonoBehaviour
{
    [SerializeField] private GameObject bullet;
    [SerializeField] private Renderer bulletRenderer;
    [SerializeField] private Collider2D bulletCollider;
    Rigidbody2D bulletRB;

    void Start()
    {
        bulletRB = gameObject.GetComponent<Rigidbody2D>();
        bulletCollider = gameObject.GetComponent<Collider2D>();
    }

    //Destroys bullets on impact with any collider
    void OnCollisionEnter2D(Collision2D coll)
    {
        bulletRenderer.enabled = false;
        bulletCollider.enabled = false;
        //If adding bullet impact noise, add it here

        Destroy(bullet, .05f);
    }
}