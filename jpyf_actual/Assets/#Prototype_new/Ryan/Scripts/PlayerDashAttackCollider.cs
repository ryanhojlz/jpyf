using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDashAttackCollider : MonoBehaviour
{
    ParticleSystem particle = null;
    Collider myCollider = null;
    bool bounce = false;
    Rigidbody rb = null;
    Transform parent = null;
    Vector3 force = Vector3.zero;
    Vector3 refObj = Vector3.zero;
    private void Start()
    {
        particle = transform.GetChild(0).GetComponent<ParticleSystem>();
        myCollider = this.GetComponent<Collider>();
        rb = transform.parent.GetComponent<Rigidbody>();
        parent = this.transform.parent;
    }

    // Update is called once per frame
    void Update ()
    {
        if (Object_ControlScript.Instance.dashAtk)
        {
           // GetComponent<Collider>().enabled = true;
            myCollider.enabled = true;
        }
        else
        {
            //GetComponent<Collider>().enabled = false;
            myCollider.enabled = false;
        }

        if (bounce)
        {
            //var force2 = this.transform.position - force;
            //force2.y = 0;
            //force = -this.transform.forward;
            //force.y = 1;
            //rb.AddForce(force2 * 200000);
            //rb.AddForce(parent.transform.up * 50000);
            //rb.AddExplosionForce(130000, refObj, 10);
            var velo = -(refObj - parent.transform.position) * 34;
            velo.y = 4;
            rb.velocity = velo;
            bounce = false;
        }

	}

    private void OnTriggerEnter(Collider other)
    {
        if (Object_ControlScript.Instance.dashAtk)
        {
            if (other.gameObject.GetComponent<Force_Field>())
            {
                other.gameObject.GetComponent<Force_Field>().DecreaseFieldLevel();
                particle.Play();
                Object_ControlScript.Instance.dashAtk = false;
                //force = other.gameObject.transform.position;
                refObj = other.gameObject.transform.position;                
                if (!bounce)
                    bounce = true;
                
            }
        }
        
      
    }


}
