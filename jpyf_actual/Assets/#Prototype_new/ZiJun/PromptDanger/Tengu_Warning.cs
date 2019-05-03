using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tengu_Warning : MonoBehaviour
{
    Image tengu_warning = null;
    Color ToChange = Color.black;

    bool GoDown = true;

    float min_a = 0.0f;
    float max_a = 0.5f;

    [SerializeField]
    float speed = 1f;

    float OriginalSpeed = 0f;
    float DangerSpeed = 0f;

    float m_speed = 0f;

    bool StartWarning = false;

    public static Tengu_Warning instance = null;

    string targetTag = null;

    bool TestInput = false;


	// Use this for initialization
	void Start ()
    {
        if (!instance)
        {
            instance = this;
        }
        else if (instance)
        {
            Destroy(this.gameObject);
        }

        tengu_warning = this.GetComponent<Image>();
        ToChange = tengu_warning.color;
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (!TestInput)
        {
            targetTag = null;
        }

        if (targetTag == "Player2")
        {
            StartWarning = true;
        }
        else
        {
            StartWarning = false;
        }

        if (StartWarning)
        {
            m_speed = speed;
            OriginalSpeed = speed;
            DangerSpeed = speed * 2f;

            if (GoDown)
            {
                ToChange.a -= 0.1f * Time.deltaTime * m_speed;
                tengu_warning.color = ToChange;

                if (ToChange.a <= min_a)
                {
                    GoDown = false;
                }
            }
            else
            {
                ToChange.a += 0.1f * Time.deltaTime * m_speed;
                tengu_warning.color = ToChange;

                if (ToChange.a >= max_a)
                {
                    GoDown = true;
                }
            }
        }
        else
        {
            ToChange.a = 0;
            tengu_warning.color = ToChange;
        }

        TestInput = false;
    }

    public void SetToDanger()
    {
        m_speed = DangerSpeed;
    }

    public void SetToOriginal()
    {
        m_speed = OriginalSpeed;
    }

    public void SetStartWarning()
    {
        StartWarning = true;
    }

    public void SetStopWarning()
    {
        SetToOriginal();
        StartWarning = false;
    }

    public void SetTargeter(string targetertag)
    {
        targetTag = targetertag;
        TestInput = true;
    }
}
