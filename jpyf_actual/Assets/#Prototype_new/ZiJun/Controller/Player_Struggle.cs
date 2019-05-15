using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Struggle : MonoBehaviour
{
    Object_ControlScript Instance = null; 
    // Use this for initialization
    void Start()
    {
        Instance = Object_ControlScript.Instance;
    }

    // Update is called once per frame
    void Update()
    {
        if (Instance.Gropper)
        {
            if (Input.GetKeyDown(KeyCode.I))
            {
                Struggle();
            }
        }
    }

    public void Struggle()
    {
        if (Instance.Gropper)
        {
            //Debug.Log("Got come here");
            Instance.Gropper.GetComponent<Entity_Tengu>().editHoverSpeed = 1f;
        }
    }
}
