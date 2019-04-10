using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SPindicator : MonoBehaviour
{
    public GameObject mainObject;
    public float speed = 10f;

    // Use this for initialization
    void Start()
    {
        mainObject = transform.parent.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        OrbitAround();
    }

    void OrbitAround()
    {
        transform.RotateAround(mainObject.transform.position, Vector3.up, speed * Time.deltaTime);
    }
}
