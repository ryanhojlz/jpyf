using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteTHis : MonoBehaviour
{
	void Update ()
    {
        Debug.Log("From delete this : " + this.transform.parent.GetComponent<Entity_Unit>());
	}
}
