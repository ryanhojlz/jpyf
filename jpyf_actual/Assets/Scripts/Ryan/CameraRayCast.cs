using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRayCast : MonoBehaviour
{
    RaycastHit raycastHit;
    Ray ray;
    public Shader outlineshader;
    private GameObject playerReference;

    // Use this for initialization
    void Start ()
    {

        playerReference = GameObject.Find("Player");	
	}
	
	// Update is called once per frame
	void Update ()
    {
        Physics.Raycast(this.transform.position, this.transform.forward, out raycastHit, 100);
        if (raycastHit.transform == null)
            return;

        // If this object is a card
        if (raycastHit.transform.GetComponent<Card_ZiJun>())
        {
            if (playerReference.GetComponent<PlayerScript>().Mana >= raycastHit.transform.GetComponent<Card_ZiJun>().ManaCost)
            {

            }
            else
            {

            }
        }
    }
}
