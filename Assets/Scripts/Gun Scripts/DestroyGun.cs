using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyGun : MonoBehaviour
{
    [SerializeField] private GameObject currentGun;
    [SerializeField] private Renderer gunRenderer;
    [SerializeField] private BoxCollider2D gunCollider;

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.CompareTag("Player"))
        {
            gunRenderer.enabled = false;
            gunCollider.enabled = false;
            //If adding bullet impact noise, add it here

            Destroy(currentGun, .2f);
        }
    }
}