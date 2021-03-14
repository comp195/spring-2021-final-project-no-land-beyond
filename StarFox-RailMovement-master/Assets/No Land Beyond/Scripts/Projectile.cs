using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public GameObject impact;
    public bool fromPlayer;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnCollisionEnter(Collision collision)
    {
        ContactPoint contact = collision.contacts[0];
        Quaternion rot = Quaternion.FromToRotation(Vector3.up, contact.normal);
        Vector3 pos = contact.point;

        if (impact != null)
        {
            var impactVFX = Instantiate(impact, pos, rot);
            var psImpact = impactVFX.GetComponent<ParticleSystem>();
            if (psImpact != null)
                Destroy(impactVFX, psImpact.main.duration);
            else
            {
                var psChild = impactVFX.transform.GetChild(0).GetComponent<ParticleSystem>();
                Destroy(impactVFX, psChild.main.duration);
            }
        }

        Destroy(gameObject);
    }
}
