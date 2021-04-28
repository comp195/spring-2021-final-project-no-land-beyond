using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    public AudioClip[] clips;
    private AudioSource source;
    // Start is called before the first frame update
    void Start()
    {
        source = FindObjectOfType<AudioSource>();
        source.loop = false;
    }

    private AudioClip get_random_track()
    {
        return clips[Random.Range(0, clips.Length)];
    }

    // Update is called once per frame
    void Update()
    {
        if (!source.isPlaying)
        {
            source.clip = get_random_track();
            source.Play();
        }
    }
}
