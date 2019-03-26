using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestShaderScript : MonoBehaviour
{
    Renderer Render;
    public float amount = 0;
    bool dissolve;
    // Use this for initialization
	void Start ()
    {
        Render = GetComponent<Renderer>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (!dissolve)
        {
            amount += 1 * Time.deltaTime;
            if (amount > 1.5)
                dissolve = true;
        }
        else if (dissolve)
        {
            amount -= 1 * Time.deltaTime;
            if (amount < 0)
                dissolve = false;
        }
        Render.material.SetFloat("_DissolveAmount", amount);
        //if (Input.GetKey(KeyCode.Plus))
        //{
        //    amount += 0.1f;
            
        //}
        //else if (Input.GetKey(KeyCode.Minus))
        //{
        //    amount -= 0.1f;
        //}

        //if (amount < 0)
        //{
        //    amount = 0;
        //}
        //else if (amount > 1)
        //{
        //    amount = 1;
        //}
        //Render.material.SetFloat("_DissolveAmount", Mathf.PingPong(Time.time, 1));  
    }
}
