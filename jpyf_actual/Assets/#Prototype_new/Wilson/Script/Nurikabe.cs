using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nurikabe : MonoBehaviour {

    public GameObject meleeProjectile;//find melee projectile instead of dragging the object inside
    private AudioSource source;
    public AudioClip attackSound;
    public GameObject specialEffect = null;

    float target_distance;
}
