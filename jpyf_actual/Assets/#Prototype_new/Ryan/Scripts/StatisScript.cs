using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatisScript : MonoBehaviour
{

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
#if UNITY_PS4
        if (PS4_ControllerScript.Instance.ReturnCrossPress())
        {
            GameObject.Find("Sceneload").GetComponent<SceneLoad>().GoBackToMainMenu();
        }
#endif
    }
}
