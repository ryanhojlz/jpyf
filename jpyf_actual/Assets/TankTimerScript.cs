using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankTimerScript : MonoBehaviour
{
    public float lifetime = 2.0f;
    public GameObject owner = null;
    Vector3 eulerankles = Vector3.zero;
    float y_val = 0;
    public float spinSpeed = 1;
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
        if (owner != null)
        {
            this.transform.position = owner.transform.position;
        }

        y_val += 250 * Time.deltaTime;  
        eulerankles.y = y_val;
        this.transform.eulerAngles = eulerankles;

    }
}
