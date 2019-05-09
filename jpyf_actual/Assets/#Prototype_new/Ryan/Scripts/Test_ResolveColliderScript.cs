using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_ResolveColliderScript : MonoBehaviour
{
    public Collider[] collider_list = new Collider[2];
	// Use this for initialization
	void Start ()
    {
        int i = 0;
        foreach (Collider c in GetComponents<Collider>())
        {
            collider_list[i] = c;
            ++i;
        }

	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
