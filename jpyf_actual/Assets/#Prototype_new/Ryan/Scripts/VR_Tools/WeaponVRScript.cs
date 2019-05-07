using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponVRScript : MonoBehaviour {

    public int damage = 5;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if (transform.parent.GetComponent<WeaponHandleScript>())
        {
            if (!transform.parent.GetComponent<WeaponHandleScript>().b_ToolReleased)
            {
                if (other.GetComponent<Entity_Unit>())
                {
                    Debug.Log("Damage Taken");
                    other.GetComponent<Entity_Unit>().TakeDamage(damage);
                }
            }
        }
        else if (transform.parent.GetComponent<MultiToolScript>())
        {
            if (!transform.parent.GetComponent<MultiToolScript>().b_ToolReleased)
            {
                if (other.GetComponent<Entity_Unit>())
                {
                    Debug.Log("Damage Taken");
                    other.GetComponent<Entity_Unit>().TakeDamage(damage);
                }
            }
        }


    }
}
