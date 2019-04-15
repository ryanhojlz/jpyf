using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rain : MonoBehaviour
{
    public bool rainToggle = false;
    float humidityLevel = 0;
    float humidityToRain = 50f;
    float prevTime = -1.0f;
    int random = Random.Range(1, 10);


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

        if (prevTime == -1)
        {
            prevTime = Time.time;
        }
        Debug.Log(humidityLevel);
        float currentTime = Time.time;


        //if (humidityLevel > 100)
        //{
        //    humidityLevel = 100;
        //}

        if (Random.Range(0.00f, 1.00f) < (humidityLevel / 100f) && !rainToggle && currentTime > (prevTime + 1.0f))
        {
            Debug.Log("Start raining");
            if (humidityLevel > humidityToRain)
                rainToggle = true;
        }
        else if (rainToggle)
        {
            if (currentTime > (prevTime + 1.0f))
            {
                humidityLevel--;
                prevTime = Time.time;
            }
            if (humidityLevel <= 0)
            {
                rainToggle = false;
            }
        }
        else
        {
            if (currentTime > (prevTime + 1.0f))
            {
                humidityLevel++;
                prevTime = Time.time;
            }
        }



        //if(humidityLevel > 30 && humidityLevel < 50)
        //{

        //    Debug.Log("Rand value " + random);
        //    if(random == 4)
        //    {
        //        rainToggle = true;
        //        humidityLevel--;
        //    }
        //    if(humidityLevel == 0)
        //    {
        //        rainToggle = false;
        //        humidityLevel++;
        //    }
        //}

        //if (humidityLevel > 60 && humidityLevel < 80)
        //{
        //    int random = Random.Range(1, 5);
        //    Debug.Log("Rand value " + random);
        //    if (random == 3)
        //    {
        //        rainToggle = true;
        //        humidityLevel--;
        //    }
        //    if (humidityLevel == 0)
        //    {
        //        rainToggle = false;
        //        humidityLevel++;
        //    }
        //}

        //if (humidityLevel > 90 && humidityLevel < 100)
        //{
        //    int random = Random.Range(1, 3);
        //    Debug.Log("Rand value " + random);
        //    if (random == 2)
        //    {
        //        rainToggle = true;
        //        humidityLevel--;
        //    }
        //    if (humidityLevel == 0)
        //    {
        //        rainToggle = false;
        //        humidityLevel++;
        //    }
        //}
    }
}
