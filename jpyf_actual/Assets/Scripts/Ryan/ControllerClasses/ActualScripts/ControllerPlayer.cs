using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
#if UNITY_PS4
using UnityEngine.PS4;
#endif 
using System;

public class ControllerPlayer : MonoBehaviour
{
    public int playerId = -1;
    public int stickID;

    // ButtonBuffer
    private bool buffer_o = false;
    private bool buffer_x = false;
    private bool buffer_square = false;
    private bool buffer_triangle = false;


    public GameObject PlayerControllerObject = null;
    public GameObject Spirit = null;

    public float move_speed = 30;

    public float x_input, y_input;
    public Vector3 direction;


    
    // Use this for initialization
    void Start()
    {
        stickID = playerId + 1;
        
    }

    // Update is called once per frame
    void Update()
    {
        Controls();
        UpdateSpirit();
    }

    void Controls()
    {
        ThumbSticks();
        Buttons();
        DPad();
    }

    void ThumbSticks()
    {
        if (!Spirit)
        {
            move_speed = 4;
        }
        else
        {
            move_speed = 5;
        }
        x_input = Input.GetAxis("leftstick" + stickID + "horizontal");
        y_input = Input.GetAxis("leftstick" + stickID + "vertical");
        x_input = Mathf.Clamp(x_input, -0.8f, 0.8f);
        y_input = Mathf.Clamp(y_input, -0.8f, 0.8f);

        GameObject.Find("DebugText").GetComponent<Text>().text = "Xinput " + x_input;
        GameObject.Find("DebugText3").GetComponent<Text>().text = "Yinput " + y_input;
        
        direction = new Vector3(x_input, 0, -y_input);
        if (direction == Vector3.zero)
        {
            this.PlayerControllerObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
        }
        else
        {
            this.PlayerControllerObject.GetComponent<Rigidbody>().velocity = direction * move_speed;
        }

        //PlayerControllerObject.transform.position += direction * move_speed * Time.deltaTime;
    }

    void Buttons()
    {


        if (Input.GetKeyDown(KeyCode.Z))
        {
            IfSpirit();
        }

        //======================================================================================================
        // X Button
        //======================================================================================================
        if (Input.GetKey((KeyCode)Enum.Parse(typeof(KeyCode), "Joystick" + stickID + "Button0")) && !buffer_x)
        {
            IfSpirit();
            // hi
            buffer_x = true;
        }
        else if (!Input.GetKey((KeyCode)Enum.Parse(typeof(KeyCode), "Joystick" + stickID + "Button0")) && buffer_x)
        {
            buffer_x = false;
        }

        //======================================================================================================
        // O Button
        //======================================================================================================

        if (Input.GetKey((KeyCode)Enum.Parse(typeof(KeyCode), "Joystick" + stickID + "Button1")) && !buffer_o)
        {
            // Special Attack
            if (Spirit)
            {
                Debug.Log("attacked");
                PlayerControllerObject.GetComponent<Attack_Unit>().SpecialAttack();
            }
            buffer_o = true;
        }
        else if (!Input.GetKey((KeyCode)Enum.Parse(typeof(KeyCode), "Joystick" + stickID + "Button1")) && buffer_o)
        {
            buffer_o = false;
        }

        //======================================================================================================
        // Square Button
        //======================================================================================================
        if (Input.GetKey((KeyCode)Enum.Parse(typeof(KeyCode), "Joystick" + stickID + "Button2")) && !buffer_square)
        {
            // Normal Attack
            
            buffer_square = true;
        }
        else if (!Input.GetKey((KeyCode)Enum.Parse(typeof(KeyCode), "Joystick" + stickID + "Button2")) && buffer_square)
        {
            buffer_square = false;

        }

        //======================================================================================================
        // Triangle Button
        //======================================================================================================
        if (Input.GetKey((KeyCode)Enum.Parse(typeof(KeyCode), "Joystick" + stickID + "Button3")) && !buffer_triangle)
        {

            if (Spirit)
            {
                
                Debug.Log("attacked");
                PlayerControllerObject.GetComponent<BasicGameOBJ>().attackValue = 1;
                PlayerControllerObject.GetComponent<Attack_Unit>().PlayerAutoAttack();
            }
            buffer_triangle = true;
        }
        else if (!Input.GetKey((KeyCode)Enum.Parse(typeof(KeyCode), "Joystick" + stickID + "Button4")) && buffer_triangle)
        {

            buffer_triangle = false;
        }
    }



    void DPad()
    {

    }


    void IfSpirit()
    {
        // Possesion
        if (!Spirit)
        {
            //Debug.Log("\npreessseed\n");
            if (PlayerControllerObject.GetComponent<Possesor>().startPossesing)
            {
                PlayerControllerObject.GetComponent<Possesor>().possesionProgress += 1;
            }
            else if (!PlayerControllerObject.GetComponent<Possesor>().startPossesing)
            {
                if (PlayerControllerObject.GetComponent<Possesor>().canPosses)
                {
                    PlayerControllerObject.GetComponent<Possesor>().startPossesing = true;
                    var kleur = PlayerControllerObject.GetComponent<Renderer>().material;
                    kleur.color = Color.green;
                    PlayerControllerObject.GetComponent<Renderer>().material = kleur;
                    PlayerControllerObject.GetComponent<Possesor>().Text_Instantiate();
                }
            }
        }


    }

    void UpdateSpirit()
    {
        if (Spirit)
        {
            if (PlayerControllerObject.GetComponent<BasicGameOBJ>().healthValue <= 0)
            {
                Spirit.SetActive(true);
                PlayerControllerObject = Spirit;
                Spirit = null;
            }
        }
    }

}
