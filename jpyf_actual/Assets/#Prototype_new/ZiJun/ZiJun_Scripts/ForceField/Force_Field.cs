using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Force_Field : MonoBehaviour
{
    [SerializeField]
    bool m_isActive = false;
    GameObject m_ForceField = null;
	// Use this for initialization
	void Start ()
    {
        m_ForceField = transform.Find("Force_Field").gameObject;
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
	}

    public void SetForceField(bool active)
    {
        m_isActive = active;
    }

    public bool GetIsActive()
    {
        return m_isActive;
    }
}
