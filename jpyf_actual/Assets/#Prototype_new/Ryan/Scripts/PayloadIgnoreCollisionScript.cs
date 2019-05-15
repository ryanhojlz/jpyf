﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PayloadIgnoreCollisionScript : MonoBehaviour
{
	// Use this for initialization
	void Start ()
    {
        foreach (Transform obj in transform)
        {
            if (obj.GetComponent<Collider>())
                Physics.IgnoreCollision(this.GetComponent<Collider>(), obj.GetComponent<Collider>(),true);
        }

        GameObject go = Object_ControlScript.Instance.CurrentObj;
        Physics.IgnoreCollision(this.GetComponent<Collider>(), go.GetComponent<Collider>(),true);

    }
}
