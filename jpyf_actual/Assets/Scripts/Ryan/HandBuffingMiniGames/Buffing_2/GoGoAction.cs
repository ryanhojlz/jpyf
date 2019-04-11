using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoGoAction : MonoBehaviour
{
    public GameObject playerReference;
    public Vector3 referencePosition;

    public Vector3 interact_pos_1;
    public Vector3 interact_pos_2;

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

    int trigger = 0;

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
        if (Input.GetKeyDown(KeyCode.P))
            StartMiniGame();

        handpos = hand_1.transform.position;
        handpos2 = hand_2.transform.position;

        if (startMinigame)
        {
            if (!firstrun)
            {
                referencePosition = playerReference.transform.position;
                interact_pos_1 = interact_pos_2 = referencePosition;
                interact_pos_1.y += 0.15f;
                interact_pos_2.y -= 0.05f;

                Debug.Log("Oi boi ah i running lo");
                //GameObject text = Instantiate(boxprint) as GameObject;
                //GameObject text2 = Instantiate(boxprint) as GameObject;
                //text.transform.position = interact_pos_1;
                //text2.transform.position = interact_pos_2;
                firstrun = true;
            }
            minigameLength -= 1 * Time.deltaTime;


            if (stance)
            {

                if (hand_1.transform.position.y > interact_pos_1.y
                    && hand_2.transform.position.y < interact_pos_2.y)
                {
                    printText("AAAAA");
                    stance = false;
                    ++trigger;
                }
            }
            else if (!stance)
            {

                if (hand_1.transform.position.y < interact_pos_2.y
                    && hand_2.transform.position.y > interact_pos_1.y)
                {
                    printText("BBB");
                    stance = true;
                    ++trigger;
                }
            }


            if (minigameLength < 0)
            {
                minigameLength = 3.0f;
                startMinigame = false;
                firstrun = false;
                stance = false;
            }
        }

        if (trigger >= 2)
        {
            if (GameObject.Find("Player_Object").GetComponent<BasicGameOBJ>())
            {
                GameObject.Find("Player_Object").GetComponent<BasicGameOBJ>().healthValue += 2;
            }
            trigger = 0;
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

    public void StartMiniGame()
    {
        Debug.Log("Blo i got run leh");
        startMinigame = true;
        printText("beep");
    }
}