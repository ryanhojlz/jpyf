using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextEffectControl : MonoBehaviour
{
    public GameObject possesor;
    public GameObject yokai;

	// Use this for initialization
	void Start ()
    {
        //transform.parent = yokai.transform;
        // transform.localPosition = Vector3.zero;
        //var localpos = transform.localPosition;
        //localpos.y += 3;
        //transform.localPosition = localpos;

    }

    // Update is called once per frame
    void Update ()
    {
        
        if (!possesor.GetComponent<Possesor>().startPossesing)
        {
            Destroy(this.gameObject);
        }

        this.GetComponent<Image>().fillAmount = 1 * 
            (possesor.GetComponent<Possesor>().possesionProgress / 
            possesor.GetComponent<Possesor>().possesionProgressCap);

        transform.position = yokai.transform.position;
        var changepos = transform.position;
        changepos.y += 1;
        transform.position = changepos;
        transform.LookAt(GameObject.Find("spec_cam").transform);
        transform.eulerAngles = new Vector3(90, 0, 0);
        
    }
}
