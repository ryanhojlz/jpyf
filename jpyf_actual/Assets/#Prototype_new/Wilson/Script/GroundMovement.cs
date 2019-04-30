using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundMovement : MonoBehaviour
{
    public Camera mainCamera;
    Vector3 dir;
    Vector3 currButtonPos;
    Vector3 originalPosition;

    public GameObject offsetMaterial;
    public float speed;

    // Use this for initialization
    void Start()
    {
        currButtonPos = gameObject.transform.position;
        originalPosition = currButtonPos;
        dir = -Camera.main.transform.forward;  // back
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.position = new Vector3(currButtonPos.x, currButtonPos.y, currButtonPos.z -= speed * Time.deltaTime);

        Debug.Log(gameObject.transform.position.z);
        Debug.Log("Camera position " + mainCamera.transform.position.z);
        if (gameObject.transform.position.z + offsetMaterial.transform.lossyScale.z * 0.5f < mainCamera.transform.position.z)
        {
            //gameObject.transform.position = originalPosition;
            currButtonPos = originalPosition;
            Debug.Log("Current position " + gameObject.transform.position);
        }
    }
}
