using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRayCast : MonoBehaviour
{
    RaycastHit raycastHit;
    Ray ray;
    public Shader outlineshader;
    private GameObject playerReference;
    float[] canSelect = new float[4];
    float[] cannotSelect = new float[4];
    float[] debugArray = new float[4];
    // Use this for initialization
    void Start ()
    {
        canSelect[0] = 1;
        canSelect[1] = 1;
        canSelect[2] = 0;

        cannotSelect[0] = 1;
        cannotSelect[1] = 0;
        cannotSelect[2] = 0;


        canSelect[3] = cannotSelect[3] = 1;
        playerReference = GameObject.Find("Player");	
	}
	
	// Update is called once per frame
	void Update ()
    {
        Physics.Raycast(this.transform.position, this.transform.forward, out raycastHit, 100);
        if (raycastHit.transform == null)
            return;
        //if (raycastHit.transform.name == "demo_ground")
        //{
        //    raycastHit.transform.GetComponent<Renderer>().material.color = Color.black;
        //}
        // If this object is a card
        if (raycastHit.transform.GetComponent<Card_ZiJun>())
        {
            //raycastHit.transform.GetComponent<Renderer>().material.shader = outlineshader; 
            //debugArray = transform.GetComponent<Renderer>().material.GetFloatArray("_OutlineColor");
            //Debug.Log("awdouwabdjiwabdjwidbwuaidbaw   " + debugArray);
            //raycastHit.transform.GetComponent<Renderer>().material.SetFloat("_OutlineWidth", 0.035f);
            if (playerReference.GetComponent<PlayerScript>().Mana >= raycastHit.transform.GetComponent<Card_ZiJun>().ManaCost)
            {
                //raycastHit.transform.GetComponent<Renderer>().material.SetFloatArray("_OutlineColor", canSelect);
            }
            else
            {
                //raycastHit.transform.GetComponent<Renderer>().material.SetFloatArray("_OutlineColor", cannotSelect);
            }
        }
    }
}
