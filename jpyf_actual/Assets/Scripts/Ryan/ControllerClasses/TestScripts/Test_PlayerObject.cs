using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PS4;
using System;

public class Test_PlayerObject : MonoBehaviour
{
    // Player id is seperate from stick id its a ps4 thing to me
    public int _playerId = -1;
    public int _stickid = 0;
    private Color lightbar;
    private bool hasSetupGamepad = false;

    Vector3 originalPos;
    // Use this for initialization
    void Start ()
    {
       // originalPos = this.gameObject.transform.position;
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (_playerId > -1)
        {
            //Debug.Log("\n" + "ID is currently" + _playerId + "\n");
            //ChangePadColor(_playerId);
            ThumbStick_Movement();
            ShoulderButtons();
        }
        if (this.transform.position.y > 150 || this.transform.position.y < -150)
        {
            this.gameObject.transform.position = originalPos;
            this.gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
        }
    }

    void ThumbStick_Movement()
    {
        // Camera Movement
        float cam_move_speed = 50.0f;
        float move_X = Input.GetAxis("leftstick" + _stickid + "horizontal") * cam_move_speed;
        float move_Y = Input.GetAxis("leftstick" + _stickid + "vertical") * cam_move_speed;

        //Vector3 movedir = new Vector3(move_X, 0, -move_Y);
        Vector3 movedir = gameObject.transform.GetComponent<Rigidbody>().velocity;
        movedir.x = move_X;
        movedir.z = -move_Y;

        //_Player.transform.TransformDirection(movedir.normalized);

        //movedir = this.gameObject.GetComponent<Camera>().transform.TransformDirection(movedir);
        // If 0 means no flying
        //movedir.y = 0;
        //this.gameObject.transform.position += movedir * Time.deltaTime;

        // Physics based
        //if (movedir == Vector3.zero)
        //{
        //    this.gameObject.GetComponent<Rigidbody>().velocity = new Vector3(0,GetComponent<Rigidbody>().velocity.y,0);
        //}
        //else
        //{
        //    this.gameObject.GetComponent<Rigidbody>().velocity = movedir * Time.deltaTime;
        //}


        if (movedir == Vector3.zero)
        {
            var force = gameObject.GetComponent<Rigidbody>().velocity;
            force.x = 0;
            force.z = 0;
            gameObject.GetComponent<Rigidbody>().velocity = force;
        }
        else
        {
            gameObject.GetComponent<Rigidbody>().AddForce(movedir);
        }
    }

    void ShoulderButtons()
    {
        if (Input.GetAxis("joystick" + _stickid + "_left_trigger") != 0)
        {
            GameObject.Find("ViewManager").GetComponent<ViewScript>().SetDeviceView(true);
        }
        if (Input.GetKey((KeyCode)Enum.Parse(typeof(KeyCode), "Joystick" + _stickid + "Button4", true)))
        {
            GameObject.Find("ViewManager").GetComponent<ViewScript>().SetDeviceView(false);
        }
    }


    public void ChangePadColor(int d_playerid)
    {
        //    PS4Input.PadResetLightBar(playerId);
        //    var p_color = loggedInUser.color;
        //    PS4Input.PadSetLightBar(playerId,p_color)
        //Debug.Log("Color change was ran ran ruuuuu");
        //Color var_color = Color.white;
        //switch (d_playerid)
        //{
        //    case 0:
        //        var_color = Color.blue;
        //        break;
        //    case 1:
        //        var_color = Color.red;
        //        break;
        //    case 2:
        //        var_color = Color.green;
        //        break;
        //    case 3:
        //        var_color = Color.magenta;
        //        break;
        //}
        //PS4Input.PadResetLightBar(d_playerid);
        //PS4Input.PadSetLightBar(d_playerid, (int)var_color.r, (int)var_color.g, (int)var_color.b);
        Vector3 _color = Vector3.zero;
        switch (d_playerid)
        {
            case 0:
                _color = new Vector3(255,0,255);
                break;
            case 1:
                _color = new Vector3(255, 0, 0);
                break;
            case 2:
                _color = new Vector3(0, 255, 0);
                break;
            case 3:
                _color = new Vector3(255, 0, 255);
                break;
        }

        PS4Input.PadSetLightBar(d_playerid, (int)_color.x, (int)_color.y, (int)_color.z);
        
    }
}
