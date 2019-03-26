using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rain : MonoBehaviour
{
    public bool rainToggle = false;
    
    public ParticleSystem rainParticle;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (rainToggle == true)
        {
            if (!rainParticle.isPlaying)
            {
                rainParticle.Play();
                //rainParticle.enableEmission = true;
                Debug.Log("Raining");
            }
        }
        else if (rainToggle == false)
        {
            if (rainParticle.isPlaying)
            {
                rainParticle.Stop();
                //rainParticle.Clear();
                //rainParticle.enableEmission = false;
                Debug.Log("Stop Raining");
            }
        }

        //if (rainParticle.isPlaying)
        //{
        //    Debug.Log("Is playing");
        //}

        //if (rainParticle.isStopped)
        //{
        //    Debug.Log("Is stopped");
        //}

    }
}
