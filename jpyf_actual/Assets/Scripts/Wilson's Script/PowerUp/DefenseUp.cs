using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefenseUp : PowerUp
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Minion>())
        {
            //Debug.Log("Collided");
            other.GetComponent<Minion>().defenceValue += 50f;
            Destroy(gameObject);
        }
    }
}
