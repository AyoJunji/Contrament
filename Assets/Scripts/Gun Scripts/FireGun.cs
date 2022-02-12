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
    [SerializeField] private PlayerController playerController;

    [Header("Speed & Multipliers")]
    [SerializeField] private float bulletSpeed = 8.5f;
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

        //Input for shooting all guns
        if (Input.GetButton("Fire1") && Time.time > nextFire && playerController.ammo > 0)
        {
            WeaponFire();
        }
    }

    //How the different guns shoot
    private void WeaponFire()
    {
        playerController.ammo -= 1;
        if (usingDefaultGun == true)
        {
            nextFire = Time.time + fireRate;
            var spawnedBullet = Instantiate(defaultBullet, barrel.position, barrel.rotation);

            //Add Gun shot noise here

            spawnedBullet.GetComponent<Rigidbody2D>().AddForce(barrel.up * bulletSpeed, ForceMode2D.Impulse);
            Destroy(spawnedBullet, 1.5f);
        }

        if (usingCollateralGun == true)
        {
            nextFire = Time.time + (fireRate + .2f);
            var spawnedBullet = Instantiate(collateralBullet, barrel.position, barrel.rotation);

            //Add Gun shot noise here

            spawnedBullet.GetComponent<Rigidbody2D>().AddForce(barrel.up * collateralSpeed, ForceMode2D.Impulse);
            Destroy(spawnedBullet, 2f);
        }

        if (usingShotgun == true)
        {
            nextFire = Time.time + (fireRate - .1f);
            for (int i = 0; i <= 4; i++)
            {
                var spawnedBullet = Instantiate(shotgunBullet, barrel.position, barrel.rotation);

                //Add Gun shot noise here

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

    //Player collision with weapons to pick them up
    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.name == "Shotgun")
        {
            //If adding pick-up noises, put it here
            usingDefaultGun = false;
            usingShotgun = true;
            usingCollateralGun = false;
        }

        if (coll.gameObject.name == "DefaultGun")
        {
            //If adding pick-up noises, put it here
            usingDefaultGun = true;
            usingShotgun = false;
            usingCollateralGun = false;
        }

        if (coll.gameObject.name == "CollateralGun")
        {
            //If adding pick-up noises, put it here
            usingDefaultGun = false;
            usingShotgun = false;
            usingCollateralGun = true;
        }

    }
}