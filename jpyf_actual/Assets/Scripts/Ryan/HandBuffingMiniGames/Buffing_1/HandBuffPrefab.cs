using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class HandBuffPrefab : MonoBehaviour
{
    // Lifetime of object
    public float lifetime = 3.0f;
    private float a_lifetime;
    public float color = 0.0f;
    //
    public GameObject textPrefab;
    public GameObject reference;
    // Use this for initialization
    void Start ()
    {
        a_lifetime = lifetime;
        reference = GameObject.Find("Camera_player").gameObject;
        // this.gameObject.GetComponent<Rigidbody>().isKinematic = true;
    }
	
	// Update is called once per frame
	void Update ()
    {
        // Fade Effect
        a_lifetime -= 1 * Time.deltaTime;
        var _mat = this.gameObject.GetComponent<Renderer>().material.color;
        _mat.a = 1 * (a_lifetime / lifetime);
        color = _mat.a;
        if (_mat.a < 0)
        {
            _mat.a = 0;
        }
        this.gameObject.GetComponent<Renderer>().material.color = _mat;

        if (a_lifetime < 0)
        {
            //spawn_TextObj();
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Left Hand" || other.gameObject.name == "Right Hand")
        {
            spawn_TextObj();
            Destroy(this.gameObject);
            //GameObject.Find("minig_1Manager")
        }
    }

    private void spawn_TextObj()
    {
        GameObject go = Instantiate(textPrefab) as GameObject;
        go.GetComponent<TextMesh>().text = "Hitto";
        // Parent the text
        go.transform.SetParent(reference.gameObject.transform);
        // Reset Position
        go.transform.localPosition = Vector3.zero;
        var pos = go.transform.localPosition;
        pos.z += 2;
        // Add position
        go.transform.localPosition = pos;
        go.transform.LookAt(reference.transform); 
        var rotation_y = go.transform.localEulerAngles;
        rotation_y.y = rotation_y.y * 2;
        rotation_y.z = 0;
        go.transform.localEulerAngles = rotation_y;
        go.GetComponent<TextMesh>().color = Color.black;
    }


}
