using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicalNote : MonoBehaviour
{
    AudioSource source;
    AudioClip clip;

    bool alreadyPlayed = false;
    // Use this for initialization
    void Start()
    {
        source = this.GetComponent<AudioSource>();
        clip = AudioManager.Instance.HittingDrum;

        source.clip = clip;
        source.Play();
    }

    // Update is called once per frame
    void Update()
    {
        //if()
    }
}
