﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public CameraShake cameraShake;
    Vector3 originalPos;

    // Use this for initialization
    void Start()
    {
        originalPos = transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.O))
        {
            StartCoroutine(cameraShake.Shake(.4f, .4f));
        }
    }

    IEnumerator Shake (float duration, float magnitude)
    {
       

        float elasped = 0.0f;

        while (elasped < duration)
        {
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;

            transform.localPosition = new Vector3(x, y, originalPos.z);

            elasped += Time.deltaTime;

            yield return null;
        }
        transform.localPosition = originalPos;
    }
}
