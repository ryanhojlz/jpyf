using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathEffectScript : MonoBehaviour
{

    float lifetime = 1.0f;

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        lifetime -= 1 * Time.deltaTime;
        if (lifetime <= 0)
        {
            Destroy(this.gameObject);
        }
	}
}
