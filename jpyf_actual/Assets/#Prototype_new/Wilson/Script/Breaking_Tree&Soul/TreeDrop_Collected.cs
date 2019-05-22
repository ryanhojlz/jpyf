using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeDrop_Collected : MonoBehaviour
{
    AudioSource source = null;
    AudioClip drop = null;
    AudioClip collected = null;

    GameObject holder = null;
    AudioSource holderAS = null;

    // Use this for initialization
    void Start()
    {
        source = this.GetComponent<AudioSource>();
        collected = AudioManager.Instance.Pickup_Item;
        drop = AudioManager.Instance.ItemDropSound;
    }

    // Update is called once per frame
    //void Update()
    //{

    //}
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "floor")
        {
            //Debug.Log("Hitto");
            source.clip = drop;
            source.Play();
        }
    }

    private void OnDestroy()
    {
        //source
        holder = Instantiate(AudioManager.Instance.AudioPrefeb, transform.position, Quaternion.identity);
        holderAS = holder.GetComponent<AudioSource>();
        holderAS.clip = collected;
        holderAS.Play();
    }
}
