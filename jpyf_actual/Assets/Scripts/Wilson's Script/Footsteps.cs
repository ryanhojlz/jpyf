using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Footsteps : MonoBehaviour
{
    public GameObject leftFootprint;
    public GameObject rightFootprint;

    public Transform leftFootLocation;
    public Transform rightFootLocation;

    public AudioSource leftFootAudioSource;
    public AudioSource rightFootAudioSource;

    float lifeTime;

    public float footprintOffset = 0.05f;

    void LeftFootstep()
    {
        leftFootAudioSource.Play();

        //Raycast out and create footprint
        RaycastHit hit;

        if(Physics.Raycast(leftFootLocation.position,leftFootLocation.forward,out hit))
        {
            Instantiate(leftFootprint, hit.point + hit.normal * footprintOffset, Quaternion.LookRotation(hit.normal, leftFootLocation.up));
        }
    }

    void RightFootstep()
    {
        rightFootAudioSource.Play();

        //Raycast out and create footprint
        RaycastHit hit;

        if (Physics.Raycast(rightFootLocation.position, rightFootLocation.forward, out hit))
        {
            Instantiate(rightFootprint, hit.point + hit.normal * footprintOffset, Quaternion.LookRotation(hit.normal,rightFootLocation.up));
        }
    }
}
