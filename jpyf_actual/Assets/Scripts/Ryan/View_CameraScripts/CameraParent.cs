using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraParent : MonoBehaviour
{
    public GameObject playerRef = null;
    public GameObject _camera = null;

    public Vector3 rotation;
	// Use this for initialization
	void Start ()
    {
        _camera = transform.GetChild(0).gameObject;
        playerRef = GameObject.Find("Player_object");

        var pos = _camera.transform.localPosition;
        pos.z -= 3.0f;
        _camera.transform.localPosition = pos;

        rotation = new Vector3(0, 0, 0);
        rotation.x += 35;
        transform.eulerAngles = rotation;
    }

    // Update is called once per frame
    void Update ()
    {
        UpdateObjects();
        UpdateCamera();
    }

    void UpdateObjects()
    {
        transform.position = playerRef.GetComponent<ControllerPlayer>().CurrentUnit.transform.position;
    }

    void UpdateCamera()
    {
    }
}
