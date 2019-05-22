using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveAudio : MonoBehaviour
{
    Object_ControlScript Player = null;

    public AudioClip movestep = null;
    public AudioSource Psource = null;

    public AudioManager manager = null;
    // Use this for initialization
    void Start()
    {
        manager = AudioManager.Instance;
        movestep = manager.P2_movement;
        Player = Object_ControlScript.Instance;
        Psource = this.GetComponent<AudioSource>();

        //Debug.Log(movestep);

        Psource.clip = movestep;
    }

    // Update is called once per frame
    void Update()
    {
        //Player = Object_ControlScript.Instance;
        Debug.Log(movestep);
        if (!Player)
        {
            Debug.Log("No player found");
            return;
        }
        if (Player.objectRb.velocity.magnitude > 0.1f && !Psource.isPlaying)
        {
            Psource.clip = movestep;
            Psource.volume = 0.03f;
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
