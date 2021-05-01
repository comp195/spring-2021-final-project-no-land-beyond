using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    public KeyCode shoot_key = KeyCode.Space;
    public GameObject projectile;
    public GameObject coolerProjectile;
    public GameObject muzzleflash;
    public GameObject coolerFlash;
    public float projectile_speed;
    public float fire_rate;
    public bool automated; // is shooting done automatically (without input)?
    public bool fromPlayer;
    private float time_to_fire = 0;
    private int powerUps = 0;
    private bool powerUpCollision = false;
    private bool upgraded = false;
    public bool muted;

    // Update is called once per frame
    void Update()
    {
    	//TODO: find out how to set powerUpCollision true if collided with powerup (likely here or oncollision enter)

        if ((Input.GetButton("Fire1") || automated) && Time.time >= time_to_fire)
        {
            time_to_fire = Time.time + 1 / fire_rate;
            GameObject shot = GameObject.Instantiate(projectile, transform.position, transform.rotation);
            projectile.GetComponent<Projectile>().muted = muted;
            projectile.GetComponent<Projectile>().fromPlayer = fromPlayer;
            GameObject muzzle = GameObject.Instantiate(muzzleflash, transform.position, transform.rotation);
            shot.GetComponent<Rigidbody>().AddForce(transform.forward * projectile_speed);
            var psMuzzle = muzzle.GetComponent<ParticleSystem>();
            if (psMuzzle != null)
            {
                Destroy(muzzle, psMuzzle.main.duration);
            }
            else
            {
                var psChild = muzzle.transform.GetChild(0).GetComponent<ParticleSystem>();
                Destroy(muzzle, psChild.main.duration);
            }
        }
    }

    public void UpgradeWeapon(){
        projectile = coolerProjectile;
        coolerProjectile.GetComponent<Projectile>().muted = muted;
        muzzleflash = coolerFlash;
    }

    void OnCollisionEnter(Collision collision)
    {
        ContactPoint contact = collision.contacts[0];

        // Rotate the object so that the y-axis faces along the normal of the surface
        Quaternion rot = Quaternion.FromToRotation(Vector3.up, contact.normal);
        Vector3 pos = contact.point;
        //Instantiate(explosionPrefab, pos, rot);
        Destroy(gameObject);
    }
}
