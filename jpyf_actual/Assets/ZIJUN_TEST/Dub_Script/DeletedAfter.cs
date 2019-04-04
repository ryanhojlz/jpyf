using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeletedAfter : MonoBehaviour
{
    float timer = 0.1f;

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;

        if (timer < 0)
            Destroy(this.gameObject);
    }
}
