using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{

    public KeyCode shoot_key = KeyCode.Space;
    public GameObject projectile;
    public float projectile_speed;
    public float fire_rate;
    public int destroy_after = 2;

    private float time_to_fire = 0;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(shoot_key) && Time.time >= time_to_fire)
        {
            time_to_fire = Time.time + 1 / fire_rate;
            GameObject shot = GameObject.Instantiate(projectile, transform.position, transform.rotation);
            shot.GetComponent<Rigidbody>().AddForce(transform.forward * projectile_speed);
            Destroy(shot, destroy_after);
        }
    }
}
