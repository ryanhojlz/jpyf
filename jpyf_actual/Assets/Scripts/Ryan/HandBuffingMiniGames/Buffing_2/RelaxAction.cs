using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RelaxAction : MonoBehaviour
{
    public GameObject playerReference;
    public Vector3 referencePosition;

    public float offset_1 = 0.9f;
    public float offset_2 = 2;

    public bool startMinigame = false;
    public bool firstrun = false;

    public GameObject hand_1;
    public GameObject hand_2;

    public Vector3 handpos;
    public Vector3 handpos2;

    public float minigameLength = 3.0f;
    public float offset_y = 0.32f;

    //public GameObject boxprint;
    public GameObject debugtext;

    bool stance = false;
    public float distancecheck = 0;

    // Use this for initialization
    void Start()
    {
        hand_1 = GameObject.Find("Left Hand");
        hand_2 = GameObject.Find("Right Hand");
        playerReference = GameObject.Find("Camera_player");
        
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.P))
        //    StartMiniGame();

        //distancecheck = (Vector3.Magnitude(hand_1.transform.position - hand_2.transform.position));
        float x_1 = hand_1.transform.position.x;
        float x_2 = hand_2.transform.position.x;
        distancecheck = (x_1 - x_2) * (x_1 - x_2);

        handpos = hand_1.transform.position;
        handpos2 = hand_2.transform.position;

        if (startMinigame)
        {
            if (!firstrun)
            {
                referencePosition = playerReference.transform.position;
                //GameObject text = Instantiate(boxprint) as GameObject;
                //GameObject text2 = Instantiate(boxprint) as GameObject;
                firstrun = true;
            }
            minigameLength -= 1 * Time.deltaTime;


            if (stance)
            {
                if (distancecheck < 0.2f)
                {
                    printText("SWISH");
                    stance = false;
                }
               
            }
            else if (!stance)
            {

                if (distancecheck > 0.5f)
                {
                    printText("SWOOSH");
                    stance = true;
                }
                
            }



            if (minigameLength < 0)
            {
                minigameLength = 3.0f;
                startMinigame = false;
                stance = false;
                firstrun = false;
            }
        }


    }

    public void printText(string text)
    {
        GameObject go = Instantiate(debugtext) as GameObject;
        go.GetComponent<TextMesh>().text = text;
        // Parent the text
        go.transform.SetParent(playerReference.gameObject.transform);
        // Reset Position
        go.transform.localPosition = Vector3.zero;
        var pos = go.transform.localPosition;
        pos.z += 2;
        // Add position
        go.transform.localPosition = pos;
        go.transform.LookAt(playerReference.transform);
        var rotation_y = go.transform.localEulerAngles;
        rotation_y.y = rotation_y.y * 2;
        rotation_y.z = 0;
        go.transform.localEulerAngles = rotation_y;
        go.GetComponent<TextMesh>().color = Color.black;
    }

    public void SetMiniGame(bool setminigame)
    {
        this.startMinigame = setminigame;
    }

    public void StartMiniGame2()
    {
        Debug.Log("Blo i got run leh");
        startMinigame = true;
        printText("Bloop");
    }
}