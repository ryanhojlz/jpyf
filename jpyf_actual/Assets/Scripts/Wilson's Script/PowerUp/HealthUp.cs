using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthUp : PowerUp
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerStay(Collider other)
    {
        if (other.GetComponent<Minion>())
        {
            //Debug.Log("Collided");
            other.GetComponent<Minion>().startHealthvalue += 50f;
            Destroy(gameObject);
        }
    }
}
