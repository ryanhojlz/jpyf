using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushingCartV2 : MonoBehaviour
{

    GameObject player = null;
    GameObject cart = null;
    bool pushtru = false;
	// Use this for initialization
	void Start ()
    {
        player = GameObject.Find("PS4_Player");
        cart = PayloadMovementScript.Instance.payloadObject.gameObject;
    
    }

    // Update is called once per frame
    void Update()
    {
        if (pushtru)
        {
            cart.transform.position += Vector3.right * 5 * Time.deltaTime;
        }
        pushtru = false;
	}

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject == player)
        {
            pushtru = true;
        }
    }
}
