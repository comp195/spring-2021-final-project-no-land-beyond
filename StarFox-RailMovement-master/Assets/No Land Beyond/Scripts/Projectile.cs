using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public GameObject impact;
    public bool fromPlayer;
    public AudioSource hit_sound;
    public bool muted;
    // Start is called before the first frame update
    void Start()
    {
        if(muted)
            hit_sound.volume = 0.0F;
    }

    // Update is called once per frame
    void Update()
    {
        if(muted)
            hit_sound.volume = 0.0F;
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
        hit_sound.Play();
        Destroy(gameObject);
    }
}
