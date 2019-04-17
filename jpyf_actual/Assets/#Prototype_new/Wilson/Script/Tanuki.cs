using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tanuki : MonoBehaviour
{
    public GameObject bulletPrefab;
    public GameObject laserPrefeb;

    Dub_Lazer_Projectile laser;
    bool isLazer;
    //public AudioSource attackSound;
    public AudioClip attackSound;

    private void Start()
    {
        attackSound = GameObject.Find("AudioManager").GetComponent<AudioManager>().TNK_attack;
    }
}
