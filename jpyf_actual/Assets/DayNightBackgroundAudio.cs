using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayNightBackgroundAudio : MonoBehaviour
{
    [SerializeField]
    AudioClip DayTime = null;

    [SerializeField]
    AudioClip NightTime = null;

    AudioSource source = null;

    DayNightCycle daynight = null;

    // Use this for initialization
    void Start()
    {
        daynight = DayNightCycle.Instance;
        source = this.GetComponent<AudioSource>();

        NightTime = AudioManager.Instance.NightTimeBGaudio;
        DayTime = AudioManager.Instance.DayTimeBGaudio;
    }

    // Update is called once per frame
    void Update()
    {
        if (!daynight)
            return;

        if (daynight.isDaytime
            && source.clip != DayTime)
        {
            //Debug.Log("Day time");
            //source.Stop();
            if (source.volume <= 0f)
            {
                source.volume = 1.0f;
                source.clip = DayTime;
            }
            else
            {
                source.volume -= Time.deltaTime * 0.3f;
            }
            //source.Play();
        }
        else if (!daynight.isDaytime 
            && source.clip != NightTime)
        {
            //Debug.Log("Night time");
            //source.Stop();
            if (source.volume <= 0f)
            {
                source.volume = 1.0f;
                source.clip = NightTime;
            }
            else
            {
                source.volume -= Time.deltaTime * 0.3f;
            }
            //source.Play();
        }

        if (!source.isPlaying)
        {
            source.Play();
        }
    }
}
