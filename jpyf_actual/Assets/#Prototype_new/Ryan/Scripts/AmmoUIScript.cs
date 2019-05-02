using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AmmoUIScript : MonoBehaviour
{
    


	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        transform.GetChild(0).GetComponent<Image>().fillAmount = 1 * (
            (float)transform.parent.GetComponent<RangeAttackScript>().Ammo /
            (float)transform.parent.GetComponent<RangeAttackScript>().AmmoCap);	
	}
}
