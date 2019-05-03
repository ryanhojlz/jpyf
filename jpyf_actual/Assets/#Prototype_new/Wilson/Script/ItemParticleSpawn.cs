using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemParticleSpawn : MonoBehaviour
{
    float timer = 0f;
    //private ParticleSystem ps;
    // Use this for initialization
    void Start()
    {
        //ps = GetComponent<ParticleSystem>();
        //timer = ps.duration;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer > 2f) 
        {
            //timer = 0;
            //Destroy(this.gameObject);
        }
    }
}
