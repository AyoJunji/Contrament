using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyGun : MonoBehaviour
{
    [SerializeField] private GameObject currentGun;
    [SerializeField] private Renderer gunRenderer;

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.CompareTag("Player"))
        {
            gunRenderer.enabled = false;
            //If adding bullet impact noise, add it here

            Destroy(currentGun, .2f);
        }
    }
}