using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioDestroy : MonoBehaviour
{
    AudioSource source = null;
    // Update is called once per frame
    private void Start()
    {
        source = this.GetComponent<AudioSource>();
    }
    void Update()
    {
        if (!@source.isPlaying)
        {
            Destroy(this.gameObject);
        }
    }
}
