using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clip1 : MonoBehaviour
{
    public List<GameObject> obj;
	// Use this for initialization
	void Start ()
    {
        //foreach (GameObject g in this.transform)
        //{
        //    obj.Add(g.gameObject);
        //}

        for (int i = 0; i < transform.childCount; ++i)
        {
            obj.Add(transform.GetChild(i).gameObject);
        }

        foreach (GameObject h in obj)
        {
            h.transform.GetChild(0).GetComponent<Entity_Unit>().SetinstantChasePlayer(false);
            h.transform.GetChild(0).GetComponent<Entity_Unit>().SetisIdle(true);
        }
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetKeyDown(KeyCode.Home))
        {
            foreach (GameObject h in obj)
            {
                h.transform.GetChild(0).GetComponent<Entity_Unit>().SetinstantChasePlayer(true);
                h.transform.GetChild(0).GetComponent<Entity_Unit>().SetisIdle(false);
            }
        }

#if UNITY_PS4
        if (PS4_ControllerScript.Instance.ReturnL1Press())
        {
            foreach (GameObject h in obj)
            {
                h.transform.GetChild(0).GetComponent<Entity_Unit>().SetinstantChasePlayer(true);
                h.transform.GetChild(0).GetComponent<Entity_Unit>().SetisIdle(false);
            }
        }

#endif
    }
}
