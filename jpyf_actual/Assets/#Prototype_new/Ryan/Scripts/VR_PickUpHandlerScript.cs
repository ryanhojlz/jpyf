using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VR_PickUpHandlerScript : MonoBehaviour
{
    public Stats_ResourceScript handler = null;
    public GameObject particles_prefeb = null;
    public GameObject particlesReference = null;

    //private AudioClip UseItemSound;
    //private AudioSource playSound;
    

    // Use this for initialization
    void Start()
    {
        handler = GameObject.Find("Stats_ResourceHandler").GetComponent<Stats_ResourceScript>();
        particlesReference = Instantiate(particles_prefeb, transform.position, Quaternion.identity);
        particlesReference.transform.parent = this.transform;
        //UseItemSound = GameObject.Find("AudioManager").GetComponent<AudioManager>().Use_Item;
        //playSound = GameObject.Find("Pickup_mineral").GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Pickup_Scripts>())
        {
            if (other.GetComponent<Pickup_Scripts>().id != 5)
            {
                particlesReference.GetComponent<ParticleSystem>().Play();
                // Put effect here
                handler.ProcessPickUp(other.GetComponent<Pickup_Scripts>());
                // Play the sound here 
                //playSound.clip = UseItemSound;
                //playSound.Play();


                //
                Destroy(other.gameObject);
            }
        }
    }



    void spawnEffect(Vector3 position)
    {
        Instantiate(particles_prefeb, position, Quaternion.identity);
        //Debug.Log("Entered spawneffect");
    }
}
