using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallEventManager : MonoBehaviour
{
    List<GameObject> MaterialSpawnPoint = new List<GameObject>();

    bool EventStart = true;
	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        Debug.Log(EventStart);
	}

    public void SetEvent(bool trigger)
    {
        EventStart = trigger;

        //Need to put stop moving payload
    }
}
