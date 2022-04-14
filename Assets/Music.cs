using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Music : MonoBehaviour
{
    public GameObject normalMusic;
    public GameObject spookyMusic;

    private AudioSource normalSource;
    private AudioSource spookySource;

    private void Start()
    {
        normalSource = normalMusic.GetComponent<AudioSource>();
        spookySource = spookyMusic.GetComponent<AudioSource>();
    }
    public void PlayNewMusic()
    {
        if(normalSource.isPlaying)
        {
            normalSource.Pause();
            spookySource.Play();
        }

        else
        {
            spookySource.Pause();
            normalSource.Play();
        }
    }
}
