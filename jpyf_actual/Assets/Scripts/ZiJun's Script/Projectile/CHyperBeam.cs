using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CHyperBeam : MonoBehaviour {

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        //this.GetComponentInParent<Transform>().forward;
        //Debug.Log("HIIIII");
	}

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.GetComponent<BasicGameOBJ>())//Uses this line of if statement to ignore collision between ally && other projectiles
        {
            Physics.IgnoreCollision(other.gameObject.GetComponent<Collider>(), this.GetComponent<Collider>());
        }
    }

    private void OnCollisionStay(Collision other)
    {
        if (other.gameObject.GetComponent<BasicGameOBJ>())//Uses this line of if statement to ignore collision between ally && other projectiles
        {
            Physics.IgnoreCollision(other.gameObject.GetComponent<Collider>(), this.GetComponent<Collider>());
        }
        //Debug.Log(other.gameObject.name);
    }
}
