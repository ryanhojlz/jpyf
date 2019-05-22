using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LanternSound : MonoBehaviour
{
    Object_ControlScript Payload = null;

    public AudioClip lanternSound = null;
    public AudioSource Psource = null;

    public AudioManager manager = null;

    Stats_ResourceScript Instance = null;
    // Use this for initialization
    void Start()
    {
        manager = AudioManager.Instance;
        lanternSound = manager.LanternSound;
        Payload = Object_ControlScript.Instance;
        Psource = this.GetComponent<AudioSource>();

        //Debug.Log(movestep);

        Psource.clip = lanternSound;

        Instance = Stats_ResourceScript.Instance;
    }

    // Update is called once per frame
    void Update()
    {
        //Player = Object_ControlScript.Instance;
        //Debug.Log(lanternSound);
        //if (!Payload)
        //{
        //    Debug.Log("No player found");
        //    return;
        //}
        //if (Instance.m_LanternHp > Instance.m_LanternHpCap * 0.7f && !Psource.isPlaying)
        //{
        //    Psource.clip = lanternSound;
        //    Psource.volume = 0.7f;
        //    Debug.Log(Psource.clip);

        //    Debug.Log("Play ");

        //    Psource.Play();

        //}
        //else if (Instance.m_LanternHp < Instance.m_LanternHpCap * 0.69f && Instance.m_LanternHp > Instance.m_LanternHpCap * 0.3f)
        //{
        //    //Psource.clip = lanternSound;
        //    Psource.volume = 0.4f;
        //    //Debug.Log(Psource.clip);
        //    //Psource.Play();
        //}
        //else if(Instance.m_LanternHp < Instance.m_LanternHpCap * 0.29f)
        //{
        //    Psource.volume = 0.2f;
        //}
        ////else if (Psource.isPlaying)
        ////{
        ////    Psource.clip = movestep;
        ////    Debug.Log(Psource.clip);
        ////    Psource.Stop();
        ////}
        //else
        //{
        //    //Psource.Stop();
        //}

        if (Instance.m_LanternHp > Instance.m_LanternHpCap * 0.7f)
        {
            Debug.Log("Full");
            Psource.volume = 0.5f;
        }
        else if (Instance.m_LanternHp > Instance.m_LanternHpCap * 0.5f)
        {
            Psource.volume = 0.3f;
        }
        else if (Instance.m_LanternHp > Instance.m_LanternHpCap * 0.3f)
        {
            Psource.volume = 0.1f;
        }
        else if (Instance.m_LanternHp >= Instance.m_LanternHpCap * 0.0f)
        {
            Debug.Log("Empty");
            Psource.volume = 0.0f;
        }

        if (!Psource.isPlaying)
        {
            Debug.Log("Play");
            Psource.Play();
        }

    }
}
