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
    void Start()
    {
        Spirit = GameObject.Find("NewTestControllableUnit");
        Fill = transform.Find("FillAmount").gameObject;
        //Fill.GetComponent<Image>().fillAmount = 0.25f;
        //Fill.transform.parent = GameObject.Find("Canvas").transform;
        //Fill.transform.localPosition = Vector3.zero;
        //SetRender(false);
    }

    // Update is called once per frame
    void Update()
    {
        //if (Spirit != null)
        //{
        //    if (!Spirit.GetComponent<NewPossesionScript>().isPossesing)
        //    {

        //    }
        //    Fill.GetComponent<Image>().fillAmount = 1 * (Spirit.GetComponent<NewPossesionScript>().possesProgression / Spirit.GetComponent<NewPossesionScript>().possesProgressionCap);
        //    //if (Input.GetKeyDown(KeyCode.T))
        //    //{
        //    //    Fill.GetComponent<Image>().fillAmount += 0.05f;
        //    //}
        //    //Fill.GetComponent<Image>().fillAmount -= 0.2f * Time.deltaTime;
        //    if (Spirit.GetComponent<NewPossesionScript>().possesProgression >= Spirit.GetComponent<NewPossesionScript>().possesProgressionCap)
        //    {
        //        SetRender(false);
        //    }
        //}
        //Debug.Log("Swdawdawdwad " + Spirit.GetComponent<NewPossesionScript>().isPossesing);


        if (Spirit.GetComponent<NewPossesionScript>().isPossesing)
        {
            Fill.GetComponent<Image>().fillAmount = 1 * (Spirit.GetComponent<NewPossesionScript>().possesProgression / Spirit.GetComponent<NewPossesionScript>().possesProgressionCap);
            SetRender(true);
        }
        else if (!Spirit.GetComponent<NewPossesionScript>().isPossesing)
        {
            SetRender(false);
        }


    }

    public void SetRender(bool render)
    {
        // Setting on and off
        if (render)
        {
            GetComponent<Image>().enabled = true;
            foreach (Transform child in transform)
            {
                child.GetComponent<Image>().enabled = true;
            }
        }
        else
        {
            GetComponent<Image>().enabled = false;
            foreach (Transform child in transform)
            {
                child.GetComponent<Image>().enabled = false;
            }
        }

    }
}
