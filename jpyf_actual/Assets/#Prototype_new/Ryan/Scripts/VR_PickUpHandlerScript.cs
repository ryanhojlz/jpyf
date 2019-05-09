using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VR_PickUpHandlerScript : MonoBehaviour
{
    public Stats_ResourceScript handler = null;
    public GameObject particles_prefeb = null;
    public GameObject particlesReference = null;
    // Use this for initialization
    void Start()
    {
        handler = GameObject.Find("Stats_ResourceHandler").GetComponent<Stats_ResourceScript>();
        particlesReference = Instantiate(particles_prefeb, transform.position, Quaternion.identity);
        particlesReference.transform.parent = this.transform;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Pickup_Scripts>())
        {
            particlesReference.GetComponent<ParticleSystem>().Play();
            // Put effect here
            handler.ProcessPickUp(other.GetComponent<Pickup_Scripts>());
            
            Destroy(other.gameObject);
        }
    }

    void spawnEffect(Vector3 position)
    {
        Instantiate(particles_prefeb, position, Quaternion.identity);
        //Debug.Log("Entered spawneffect");
    }
}
