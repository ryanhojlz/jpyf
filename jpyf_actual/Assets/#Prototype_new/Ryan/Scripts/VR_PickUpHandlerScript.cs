using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VR_PickUpHandlerScript : MonoBehaviour
{
    public Stats_ResourceScript handler = null;
    public GameObject particles_prefeb = null;
    // Use this for initialization
    void Start()
    {
        handler = GameObject.Find("Stats_ResourceHandler").GetComponent<Stats_ResourceScript>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Pickup_Scripts>())
        {
            // Put effect here
            spawnEffect(this.transform.position);

            //
            handler.ProcessPickUp(other.GetComponent<Pickup_Scripts>());
            Destroy(other.gameObject);
        }
    }

    void spawnEffect(Vector3 position)
    {
        Instantiate(particles_prefeb, position, Quaternion.identity);
        Debug.Log("Entered spawneffect");
    }
}
