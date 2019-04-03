using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PossesionEffectScript : MonoBehaviour
{
    public GameObject Spirit = null;
    public GameObject SelectedTarget = null;
    public GameObject Fill = null;
    public GameObject Text = null;

    // Use this for initialization
    void Start()
    {
        Spirit = GameObject.Find("NewTestControllableUnit");
        Fill = transform.Find("Mask").transform.Find("FillAmount").gameObject;
        Text = transform.Find("Timer").gameObject;
    }

    // Update is called once per frame
    void Update()
    {
       

        if (Spirit.GetComponent<NewPossesionScript>().isPossesing)
        {
            Fill.GetComponent<Image>().fillAmount = 1 * (Spirit.GetComponent<NewPossesionScript>().possesProgression / Spirit.GetComponent<NewPossesionScript>().possesProgressionCap);
            Text.GetComponent<Text>().text = "" + Spirit.GetComponent<NewPossesionScript>().timeToPosses;
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
                child.transform.gameObject.SetActive(true);
                //child.GetComponent<Image>().enabled = true;
            }
        }
        else
        {
            GetComponent<Image>().enabled = false;
            foreach (Transform child in transform)
            {
                child.transform.gameObject.SetActive(false);
                //child.GetComponent<Image>().enabled = false;
            }
        }

    }
}
