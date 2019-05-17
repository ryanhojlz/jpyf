using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CartRotateScript : MonoBehaviour
{
    [Header("true = right / false = left")]
    public bool rotate = false;
    GameObject cart = null;
    public float rotamt = 0;
    bool endtrue = false;
    bool toggleonce = false;
    // Code only allows one rotation 
    // When 90 degrees rotation is done end 

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        //CartRotation();

    }

    private void OnTriggerEnter(Collider other)
    {
        // If cart come inside
        if (other.gameObject == PayloadMovementScript.Instance.payloadObject.gameObject)
        {
            cart = other.gameObject;            
        }
    }

    void CartRotation()
    {
        if (cart)
        {
            if (!toggleonce)
            {
                var payloadmovement = cart.transform.position;
                payloadmovement.x = this.transform.position.x;
                payloadmovement.z = this.transform.position.z;
                cart.transform.position = payloadmovement;
                PayloadMovementScript.Instance.isRotating = true;
                toggleonce = true;
            }
            // Rotate cart if enter in radius
            var euler = cart.transform.eulerAngles;
            if (rotate)
            {
                rotamt += 10 * Time.deltaTime;
                if (rotamt > 90)
                {
                    rotamt = 90;
                    if (!endtrue)
                        endtrue = true;
                }

            }
            else
            {
                rotamt -= 10 * Time.deltaTime;
                if (rotamt < -90)
                {
                    rotamt = -90;
                    if (!endtrue)
                        endtrue = true;
                }
            }
            euler.y = rotamt;
            cart.transform.eulerAngles = euler;
            if (endtrue)
            {
                PayloadMovementScript.Instance.isRotating = false;
                Destroy(this.gameObject);
            }
        }
    }
}
