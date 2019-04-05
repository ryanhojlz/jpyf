using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Immunity : MonoBehaviour
{

    float immunityTimer = 3;

	// Update is called once per frame
	void Update ()
    {
        immunityTimer -= Time.deltaTime;

        if (immunityTimer < 0)
        {
            Destroy(this);
        }

    }
}
