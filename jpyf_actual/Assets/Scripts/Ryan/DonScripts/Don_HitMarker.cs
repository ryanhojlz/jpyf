using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Don_HitMarker : MonoBehaviour
{
    public GameObject don_drum;
    private Vector3 dirVector = Vector3.zero;
    float movespeed = 3;
    
    // Use this for initialization
	void Start ()
    {
        this.gameObject.name = "DonHitMarker";
        don_drum = GameObject.Find("Don").gameObject;
        dirVector = don_drum.transform.position - this.transform.position;
    }
	
	// Update is called once per frame
	void Update ()
    {
        MoveTowards();
	}

    void MoveTowards()
    {
        
        this.transform.position += Vector3.Normalize(dirVector) * 3 * Time.deltaTime;
    }
}
