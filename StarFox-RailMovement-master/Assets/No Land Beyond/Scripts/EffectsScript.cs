using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectsScript : MonoBehaviour
{

	[Header("Particles")]
    public ParticleSystem trail;
    public ParticleSystem circle;
    public ParticleSystem barrel;
    public ParticleSystem stars;
    public SoundFX sounds;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Boost(bool state)
    {

        if (state)
        {
            sounds.Boost(true);
            trail.Play();
            circle.Play();
        }
        else
        {
            sounds.Boost(false);
            trail.Stop();
            circle.Stop();
        }
        trail.GetComponent<TrailRenderer>().emitting = state;
        float starsVel = state ? -20 : -1;
        var pvel = stars.velocityOverLifetime;
        pvel.z = starsVel;
    }

    public void QuickSpin(int dir)
    {
        barrel.Play();
    }
}
