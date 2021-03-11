/*************************************************************************
 * AUTHOR: Claryse Adams
 * DESCRIPTION: Script attached to laser gun. Handles shooting and its effects
 *  on orbs and player
 *  
 *  Stater code for laser logic obtained from "3. Fun With Lasers" Unity tutorial
 *  https://learn.unity.com/tutorial/live-sessions-on-graphics#5c7f8528edbc2a002053b58d
 ************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserScript : MonoBehaviour
{
    public LineRenderer laser; //LineRenderer object stores info about objects it touches

    // Start is called before the first frame update
    void Start()
    {
        laser = gameObject.GetComponent<LineRenderer>();
        laser.enabled = false; //make laser invisible
    }

    // Update is called once per frame
    void Update()
    {
        //shoot laser when user left clicks
        if (Input.GetButtonDown("Fire1"))
        {
            StopCoroutine("FireLaser");
            StartCoroutine("FireLaser");
        }
    }

    //handles laser interactions
    IEnumerator FireLaser()
    {
        laser.enabled = true;
        bool shot = false; //boolean to ensure that orbsShot in PlayerScript is only incremented once per shot

        //keeps laser visible while user is holding left click
        while (Input.GetButton("Fire1"))
        {
            Ray ray = new Ray(transform.position, transform.forward); //make 100-ft ray from end of gun
            RaycastHit hit; //stores information about what laser touches

            //laser.SetPosition(0, ray.origin);
/*
            //handles laser collision with object
            if (Physics.Raycast(ray, out hit, 100))
            {
                OrbScript hitOrb = hit.collider.gameObject.GetComponent<OrbScript>(); //orb that laser is touching
                laser.SetPosition(1, hit.point);

                //if an orb is shot
                if ( hitOrb != null)
                {
                    if(!shot && !hitOrb.IsShot()) //increment orbsShot only if orb wasn't shot already
                        transform.root.GetComponent<PlayerScript>().IncrementOrbs(); //gets PlayerScript as grandparent of gameObject laser comes from
                    hitOrb.disappear(); //make orb disappear
                    shot = true;
                }
            }
            else*/
                laser.SetPosition(1, ray.GetPoint(100));
            laser.SetPosition(1, ray.GetPoint(100));

            yield return null;
        }

        laser.enabled = false; //make laser invisible when left click is released
    }
}