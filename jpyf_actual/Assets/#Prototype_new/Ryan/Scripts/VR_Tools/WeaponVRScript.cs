using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponVRScript : MonoBehaviour {

    float damage = 3;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Entity_Unit>())
        {
            Debug.Log("Damage Taken");
            other.GetComponent<Entity_Unit>().TakeDamage(damage);
        }
    }
}
