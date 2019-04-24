using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallEventTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (this.transform.parent)
        {
            if (this.transform.parent.GetComponent<WallEventManager>())
            {
                if (other.tag == "Payload")
                {
                    Debug.Log("From : StopEventTrigger - " + other.name);
                    this.transform.parent.GetComponent<WallEventManager>().SetEvent(true);
                }
            }
        }
    }
}
