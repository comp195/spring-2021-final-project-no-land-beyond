using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundFX : MonoBehaviour
{
    public AudioSource boost;
    public AudioSource powerup;
    public AudioSource deaths;
    public AudioSource ouch;
    public AudioSource hits;
    public AudioSource kills;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Boost(bool soundOn){
      if(soundOn)
        boost.Play();
      else
        boost.Stop();

    }

    public void PowerUp(bool soundOn){
      if(soundOn)
        powerup.Play();
      else
        powerup.Stop();
    }

    public void Death(bool soundOn){
      if(soundOn)
        deaths.Play();
      else
        deaths.Stop();
    }

    public void Ouch(bool soundOn){
      if(soundOn)
        ouch.Play();
      else
        ouch.Stop();
    }

    public void Hit(bool soundOn){
      if(soundOn)
        hits.Play();
      else
        hits.Stop();
    }

    public void Kill(bool soundOn){
      if(soundOn)
        kills.Play();
      else
        kills.Stop();
    }
}
