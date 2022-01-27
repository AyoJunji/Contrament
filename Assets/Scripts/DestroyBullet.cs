using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyBullet : MonoBehaviour
{
    [SerializeField] private GameObject bullet;
    [SerializeField] private Renderer bulletRenderer;

    void OnCollisionEnter2D(Collision2D coll)
    {
        bulletRenderer.enabled = false;
        Destroy(bullet, .2f);
    }
}
