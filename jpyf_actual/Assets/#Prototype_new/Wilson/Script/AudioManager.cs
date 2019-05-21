using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioClip TNK_attack;
    public AudioClip NRKB_attack;
    public AudioClip TNG_attack;
    public AudioClip Get_Damage;
    public AudioClip Pickup_Item;
    public AudioClip Use_Item;
    public AudioClip MenuSelectSound;
    public AudioClip MenuSelectedSound;
    public AudioClip SuccessSound;
    public AudioClip FailureSound;
    public AudioClip PlayerShoot;
    public AudioClip BombSound;
    public AudioClip TNK_spawn;
    public AudioClip NRKB_spawn;
    public AudioClip TNG_spawn;
    public AudioClip LanternSound;
    public AudioClip CartSound;
    public AudioClip VR_Drum;
    public AudioClip VR_Projectile;
    public AudioClip P2_movement;

    public static AudioManager Instance = null;
    // Use this for initialization
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);

        }
    }

}
