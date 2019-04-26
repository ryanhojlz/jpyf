using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public CameraShake cameraShake;
    Vector3 originalPos;
    bool initing = false;

    // Use this for initialization
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!initing)
        {
            initing = true;
            originalPos = cameraShake.transform.localPosition;
        }

        if (Input.GetKeyDown(KeyCode.O))
        {
            StartCoroutine(cameraShake.Shake(.4f, .4f));
        }
    }

    public void ShakeCam()
    {
        StartCoroutine(cameraShake.Shake(.4f, .5f));
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
