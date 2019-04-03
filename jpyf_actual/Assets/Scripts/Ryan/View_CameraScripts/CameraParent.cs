using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraParent : MonoBehaviour
{
    public GameObject playerRef = null;
    public GameObject _camera = null;
    public GameObject _unit2Follow = null;
    public Vector3 rotation;
	// Use this for initialization
	void Start ()
    {
        _camera = transform.GetChild(0).gameObject;
        playerRef = GameObject.Find("Player_object");

        var pos = _camera.transform.localPosition;
        pos.x += 0.8f;
        pos.y -= 0.4f;
        pos.z -= 2.0f;

        _camera.transform.localPosition = pos;

        rotation = new Vector3(0, 0, 0);
        rotation.x += 5;
        transform.eulerAngles = rotation;

       // _camera.transform.localEulerAngles = new Vector3(0, -10, 0);
    }

    // Update is called once per frame
    void Update ()
    {
        UpdateObjects();
        UpdateCamera();
    }

    void UpdateObjects()
    {
        _unit2Follow = playerRef.GetComponent<ControllerPlayer>().CurrentUnit;
        if (!_unit2Follow)
            _unit2Follow = playerRef.GetComponent<ControllerPlayer>().SpiritUnit;
        var pos_offset = _unit2Follow.transform.position;
        //pos_offset.x += 2;
        this.transform.position = pos_offset;
    }

    void UpdateCamera()
    {
    }
}
