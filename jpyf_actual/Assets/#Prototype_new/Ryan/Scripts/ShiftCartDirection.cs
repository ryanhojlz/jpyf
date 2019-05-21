using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShiftCartDirection : MonoBehaviour
{
    public int leftright = 0;
    bool commitsudoku = false;


	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (commitsudoku)
        {
            if (leftright == 0)
            {
                PayloadMovementScript.Instance.moveSideways = false;
                PayloadMovementScript.Instance.cannotMove = false;
            }
            else
            {
                PayloadMovementScript.Instance.moveSideways = true;
                PayloadMovementScript.Instance.cannotMove = true;

            }

            if (leftright > 0)
                PayloadMovementScript.Instance.moveRight = true;
            else if (leftright < 0)
                PayloadMovementScript.Instance.moveRight = false;


            

            var thispos = transform.position;
            thispos.y = PayloadMovementScript.Instance.payloadObject.transform.position.y;
            PayloadMovementScript.Instance.payloadObject.transform.position = thispos;
            Destroy(this);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == PayloadMovementScript.Instance.payloadObject.gameObject)
        {
            commitsudoku = true;   
        }
    }
}
