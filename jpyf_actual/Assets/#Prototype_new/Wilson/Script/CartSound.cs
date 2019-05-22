using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CartSound : MonoBehaviour
{
    Object_ControlScript Payload = null;

    public AudioClip cartSound = null;
    public AudioSource Psource = null;

    public AudioManager manager = null;

    Stats_ResourceScript Instance = null;

    // Use this for initialization
    void Start()
    {
        manager = AudioManager.Instance;
        cartSound = manager.CartSound;
        Payload = Object_ControlScript.Instance;
        Psource = this.GetComponent<AudioSource>();

        //Debug.Log(movestep);

        Psource.clip = cartSound;

        Instance = Stats_ResourceScript.Instance;
    }

    // Update is called once per frame
    void Update()
    {
        if (PayloadMovementScript.Instance.ReturnIsMoving() && !Psource.isPlaying) 
        {
            Psource.Play();
            //Debug.Log("Cartsound playing");
        }
        else if(!PayloadMovementScript.Instance.ReturnIsMoving())
        {
            Psource.Stop();
        }
    }
}
