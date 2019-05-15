using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDashAttackCollider : MonoBehaviour
{
    ParticleSystem particle = null;
    Collider myCollider = null;
    private void Start()
    {
        particle = transform.GetChild(0).GetComponent<ParticleSystem>();
        myCollider = this.GetComponent<Collider>();
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
            }
        }
        
      
    }


}
