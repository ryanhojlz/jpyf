using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tengu : MonoBehaviour
{

    public GameObject rangeProjectile;
    public GameObject specialProjectile;
    float immunityTimer = 30f;
    public AudioClip attackSound;

    private void Start()
    {
        attackSound = GameObject.Find("AudioManager").GetComponent<AudioManager>().TNG_attack;
    }
}
