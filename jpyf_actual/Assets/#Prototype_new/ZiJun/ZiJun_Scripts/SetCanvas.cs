using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetCanvas : MonoBehaviour
{
    GlobalAchievementManager handler = null;
	// Use this for initialization
	void Start ()
    {
        handler = GlobalAchievementManager.Instance;
        if(handler)
            handler.SetCanvas(this.transform);
    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}
}
