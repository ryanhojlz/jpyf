using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp_Audio : MonoBehaviour
{
    Object_ControlScript Player = null;

    public AudioClip pickup = null;
    public AudioSource Psource = null;

    public AudioManager manager = null;
    // Use this for initialization
    void Start()
    {
        manager = AudioManager.Instance;
        pickup = manager.Pickup_Item;
        Player = Object_ControlScript.Instance;
        Psource = this.GetComponent<AudioSource>();

        //Debug.Log(pickup);

        Psource.clip = pickup;
    }

    // Update is called once per frame
    void Update()
    {
        //Player = Object_ControlScript.Instance;
        //Debug.Log(pickup);
        if (!Player)
        {
            Debug.Log("No player found");
            return;
        }
        if (Player.pickup)
        {
            Psource.clip = pickup;
            Debug.Log(Psource.clip);
            Psource.Play();
        }
        //else if (Psource.isPlaying)
        //{
        //    Psource.clip = movestep;
        //    Debug.Log(Psource.clip);
        //    Psource.Stop();
        //}
    }
}

