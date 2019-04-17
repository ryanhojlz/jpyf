using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nurikabe : MonoBehaviour
{

    //public GameObject meleeProjectile;//find melee projectile instead of dragging the object inside
    private AudioSource source;
    public AudioClip attackSound;
    //public GameObject specialEffect = null;

    float target_distance;

    private void Start()
    {
        attackSound = GameObject.Find("AudioManager").GetComponent<AudioManager>().NRKB_attack;
        source = GameObject.Find("AudioManager").GetComponent<AudioSource>();
    }

    private void Update()
    {
        //if (Input.GetKeyDown(KeyCode.A))
        //{
        //    source.clip = attackSound;
        //    source.Play();
        //    Debug.Log("play sound");
        //}
    }
}
