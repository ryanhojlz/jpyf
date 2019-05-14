using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Force_Field : MonoBehaviour
{
    [SerializeField]
    bool m_isActive = false;
    GameObject m_ForceField = null;
    int i_ForceFieldCounter = 3;

    Color color1, color2;
    // Use this for initialization
	void Start ()
    {
        i_ForceFieldCounter = Random.Range(1, 3);
        m_ForceField = transform.GetChild(transform.childCount - 1).gameObject;
        //m_ForceField = transform.Find("Force_Field").gameObject;
    }

    // Update is called once per frame
    void Update ()
    {
        if (!m_ForceField)
            return;

        if (m_isActive && !m_ForceField.activeSelf)
        {
            m_ForceField.SetActive(true);
        }
        else if (!m_isActive && m_ForceField.activeSelf)
        {
            m_ForceField.SetActive(false);
        }

        var color = m_ForceField.GetComponent<Renderer>().material.color;
        // Update force field level
        switch (i_ForceFieldCounter)
        {
            case 0:
                SetForceField(false);
                break;
            case 1:
                color.r = 1.0f;
                color.g = 0.133f;
                color.b = 0;
                m_ForceField.GetComponent<Renderer>().material.color = color;
                break;
            case 2:
                color.r = 1.0f;
                color.g = 0.733f;
                color.b = 0;
                m_ForceField.GetComponent<Renderer>().material.color = color;
                break;
            case 3:                
                color.r = 0.134f;
                color.g = 1;
                color.b = 0;
                m_ForceField.GetComponent<Renderer>().material.color = color;
                break;
        }
	}

    public void SetForceField(bool active)
    {
        m_isActive = active;
    }

    public bool GetIsActive()
    {
        return m_isActive;
    }

    public void DecreaseFieldLevel()
    {
        --i_ForceFieldCounter;
    }


}
