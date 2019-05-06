using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UI_FeedbackScript : MonoBehaviour
{
    // UI feedback
    // Grey out text when cannot do action
    // Color text when action can be taken
    public static UI_FeedbackScript Instance = null;

    // Triangle
    // Square
    // Circle
    // Cross
    // List of text UI
    public List<Text> TextUI;
    public List<bool> InteractTrue;
    private Color color_default;
    private Color greyed_out;


    private void Awake()
    {
        if (!Instance)
        {
            Instance = this;
        }
        else if (Instance)
        {
            Destroy(this.gameObject);
        }

    }

    // Use this for initialization
    void Start ()
    {
        // Grey text for when cannot interact
        greyed_out = Color.black;
        // Getting child list
        for (int i = 0; i < transform.childCount; ++i)
        {
            TextUI.Add(transform.GetChild(i).GetChild(0).GetComponent<Text>());
        }
        color_default = TextUI[0].color;

        
    }

    // Update is called once per frame
    void Update ()
    {
        
        if (InteractTrue[0])
            TextUI[0].color = color_default;
        else if (!InteractTrue[0])
            TextUI[0].color = greyed_out;

        if (InteractTrue[1])
            TextUI[1].color = color_default;
        else if (!InteractTrue[1])
            TextUI[1].color = greyed_out;

        if (InteractTrue[2])
            TextUI[2].color = color_default;
        else if (!InteractTrue[2])
            TextUI[2].color = greyed_out;

        if (InteractTrue[3])
            TextUI[3].color = color_default;
        else if (!InteractTrue[3])
            TextUI[3].color = greyed_out;

    }


}
