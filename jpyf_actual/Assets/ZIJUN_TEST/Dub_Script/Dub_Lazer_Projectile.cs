using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dub_Lazer_Projectile : MonoBehaviour {

    Transform UnitThatShoots;
	// Use this for initialization
	void Start ()
    {
        //this.transform.position = new Vector3(
        //   this.transform.position.x + (UnitThatShoots.transform.forward.x * (UnitThatShoots.transform.localScale.x + this.transform.localScale.x)) * (this.GetComponent<CapsuleCollider>().height),
        //   this.transform.position.y + (UnitThatShoots.transform.forward.y * (UnitThatShoots.transform.localScale.y + this.transform.localScale.y)) * (this.GetComponent<CapsuleCollider>().height),
        //   this.transform.position.z + (UnitThatShoots.transform.forward.z * (UnitThatShoots.transform.localScale.z + this.transform.localScale.z)) * (this.GetComponent<CapsuleCollider>().height)
        //   );
    }
	
	// Update is called once per frame
	void Update ()
    {
        //this.transform.position = new Vector3(
        //  UnitThatShoots.transform.position.x + (UnitThatShoots.transform.forward.x * (UnitThatShoots.transform.localScale.x + this.transform.localScale.y)) * ((this.GetComponent<CapsuleCollider>().height)),
        //  UnitThatShoots.transform.position.y + (UnitThatShoots.transform.forward.y * (UnitThatShoots.transform.localScale.y + this.transform.localScale.z)) * (this.GetComponent<CapsuleCollider>().radius),
        //  UnitThatShoots.transform.position.z + (UnitThatShoots.transform.forward.z * (UnitThatShoots.transform.localScale.z + this.transform.localScale.x)) * (this.GetComponent<CapsuleCollider>().radius)
        //  );

        this.transform.position = UnitThatShoots.position + (UnitThatShoots.transform.forward * (this.GetComponent<CapsuleCollider>().height));

        this.transform.LookAt(UnitThatShoots.transform.up + this.transform.position);
    }

    public void SeekUser(Transform user)
    {
        UnitThatShoots = user;
    }
}
