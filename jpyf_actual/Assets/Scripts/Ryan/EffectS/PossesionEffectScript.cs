using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PossesionEffectScript : MonoBehaviour
{
    public GameObject Spirit = null;
    public GameObject SelectedTarget = null;
    public GameObject Fill = null;
    
    // Use this for initialization
	void Start ()
    {
        Fill = transform.Find("FillAmount").gameObject;
        Fill.GetComponent<Image>().fillAmount = 0.25f;
        Fill.transform.parent = GameObject.Find("Canvas").transform;
        Fill.transform.localPosition = Vector3.zero;
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (Spirit != null)
        {
            Fill.GetComponent<Image>().fillAmount = 1 * (Spirit.GetComponent<NewPossesionScript>().possesProgression / Spirit.GetComponent<NewPossesionScript>().possesProgressionCap);
            //if (Input.GetKeyDown(KeyCode.T))
            //{
            //    Fill.GetComponent<Image>().fillAmount += 0.05f;
            //}
            //Fill.GetComponent<Image>().fillAmount -= 0.2f * Time.deltaTime;
            if (Spirit.GetComponent<NewPossesionScript>().possesProgression >= Spirit.GetComponent<NewPossesionScript>().possesProgressionCap)
            {
                Destroy(this.gameObject);
            }
        }
      
    }
}
