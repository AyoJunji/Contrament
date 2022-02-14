using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Capsule : MonoBehaviour
{
    public AnimationCurve myCurve;
    Rigidbody2D capRB;
    [SerializeField] private Renderer capRenderer;
    [SerializeField] private Collider2D capCollider;
    public GameObject shotgun;
    public GameObject lightningGun;
    public GameObject rapidGun;

    void Awake()
    {
        capRB = gameObject.GetComponent<Rigidbody2D>();
        capRenderer = gameObject.GetComponent<Renderer>();
        capCollider = gameObject.GetComponent<PolygonCollider2D>();
    }

    void Update()
    {
        if(LevelManager.gamestate == GameState.Game) {
            transform.position = new Vector3(transform.position.x, myCurve.Evaluate((Time.time % myCurve.length)), transform.position.z);
            Rigidbody2D thisRB = GetComponent<Rigidbody2D>();
            thisRB.gravityScale = 1;
        }else{
            Rigidbody2D thisRB = GetComponent<Rigidbody2D>();
            thisRB.gravityScale = 0;
        }
    }

    void OnCollisionEnter2D(Collision2D coll)
    {

        if (coll.gameObject.CompareTag("FriendlyProjectiles"))
        {
            Death();
        }
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("FriendlyProjectiles")){
            Death();
        }
    }

    void FixedUpdate()
    {
        if(LevelManager.gamestate == GameState.Game) {
            capRB.velocity = transform.right * 4;
        }else{
            capRB.velocity = transform.right * 0;
        }
    }

    void Death()
    {
        AudioSource audio = gameObject.GetComponent<AudioSource>();
        audio.PlayOneShot(PlayerController.playerControllerCS.clips[1]);
        capRenderer.enabled = false;
        capCollider.enabled = false;
        WeaponRandomizer();
        Destroy(gameObject, .2f);
    }

    void WeaponRandomizer()
    {
        int chance = Random.Range(1, 3);

        if (chance == 1)
        {
            Instantiate(shotgun, transform.position, Quaternion.identity);
        }

        if (chance == 2)
        {
            Instantiate(lightningGun, transform.position, Quaternion.identity);

        }

        if (chance == 3)
        {
            Instantiate(rapidGun, transform.position, Quaternion.identity);
        }

    }
}
