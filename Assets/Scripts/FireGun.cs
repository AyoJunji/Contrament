using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireGun : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private Rigidbody2D bulletForce;
    [SerializeField] private GameObject defaultBullet;
    [SerializeField] private GameObject shotgunBullet;
    [SerializeField] private GameObject collateralBullet;
    [SerializeField] private Transform barrel;

    [Header("Speed & Multipliers")]
    [SerializeField] private float bulletSpeed = 7f;
    [SerializeField] private float shotgunSpeed = 500f;
    [SerializeField] private float collateralSpeed = 10f;
    [SerializeField] private float nextFire = 0f;
    [SerializeField] private float fireRate = 0.3f;

    [Header("Weapon Checking")]
    private bool usingDefaultGun;
    private bool usingShotgun;
    private bool usingCollateralGun;

    void Start()
    {
        usingDefaultGun = true;
        usingShotgun = false;
        usingCollateralGun = false;
    }

    void Update()
    {
        if (Input.GetButton("Fire1")  && Time.time > nextFire)
        {
            WeaponFire();
        }
    }

    private void WeaponFire()
    {
        if (usingDefaultGun == true)
        {
            nextFire = Time.time + fireRate;
            var spawnedBullet = Instantiate(defaultBullet, barrel.position, barrel.rotation);
            spawnedBullet.GetComponent<Rigidbody2D>().AddForce(barrel.up * bulletSpeed, ForceMode2D.Impulse);
            Destroy(spawnedBullet, 1.5f);
        }

        if (usingCollateralGun == true)
        {
            nextFire = Time.time + (fireRate + .2f);
            var spawnedBullet = Instantiate(collateralBullet, barrel.position, barrel.rotation);
            spawnedBullet.GetComponent<Rigidbody2D>().AddForce(barrel.up * collateralSpeed, ForceMode2D.Impulse);
            Destroy(spawnedBullet, 2f);
        }

        if (usingShotgun == true)
        {
            nextFire = Time.time + (fireRate -.1f);
            for (int i = 0; i <= 4; i++)
            {
                var spawnedBullet = Instantiate(shotgunBullet, barrel.position, barrel.rotation);
                Destroy(spawnedBullet, 1.3f);
                switch (i)
                {
                    case 0:
                        spawnedBullet.GetComponent<Rigidbody2D>().AddForce(barrel.up * shotgunSpeed + new Vector3(0f, -90f, 0f));
                        break;
                    case 1:
                        spawnedBullet.GetComponent<Rigidbody2D>().AddForce(barrel.up * shotgunSpeed + new Vector3(0f, -45f, 0f));
                        break;
                    case 2:
                        spawnedBullet.GetComponent<Rigidbody2D>().AddForce(barrel.up * shotgunSpeed + new Vector3(0f, 0f, 0f));
                        break;
                    case 3:
                        spawnedBullet.GetComponent<Rigidbody2D>().AddForce(barrel.up * shotgunSpeed + new Vector3(0f, 45f, 0f));
                        break;
                    case 4:
                        spawnedBullet.GetComponent<Rigidbody2D>().AddForce(barrel.up * shotgunSpeed + new Vector3(0f, 90f, 0f));
                        break;
                }
            }
        }

    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.name == "Shotgun")
        {
            usingDefaultGun = false;
            usingShotgun = true;
            usingCollateralGun = false;
        }

        if (coll.gameObject.name == "DefaultGun")
        {
            usingDefaultGun = true;
            usingShotgun = false;
            usingCollateralGun = false;
        }

        if (coll.gameObject.name == "CollateralGun")
        {
            usingDefaultGun = false;
            usingShotgun = false;
            usingCollateralGun = true;
        }

    }
}