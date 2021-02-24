using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{

    public KeyCode shoot_key = KeyCode.Space;
    public GameObject projectile;
    public GameObject muzzleflash;
    public float projectile_speed;
    public float fire_rate;

    private float time_to_fire = 0;

    // Start is called before the first frame update
    void Start()
    {
        //if (muzzleflash != null)
        //{
        //    var muzzleVFX = Instantiate(muzzleflash, transform.position, Quaternion.identity);
        //    muzzleVFX.transform.forward = gameObject.transform.forward;
        //}
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(shoot_key) && Time.time >= time_to_fire)
        {
            time_to_fire = Time.time + 1 / fire_rate;
            GameObject shot = GameObject.Instantiate(projectile, transform.position, transform.rotation);
            GameObject muzzle = GameObject.Instantiate(muzzleflash, transform.position, transform.rotation);
            shot.GetComponent<Rigidbody>().AddForce(transform.forward * projectile_speed);
            var psMuzzle = muzzle.GetComponent<ParticleSystem>();
            if (psMuzzle!= null)
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
}
