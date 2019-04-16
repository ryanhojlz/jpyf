using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Stats_ResourceScript : MonoBehaviour
{
    // Values etc
    public int m_P2_hp = 0;
    public int m_P2_hpCap = 100;
    public int m_CartHealth = 0;
    public int m_Minerals = 0;
    public int m_Souls = 0;

    // Text ui debug etc
    public Text soulText = null;
    public Text mineralText = null;
    public Transform healthbar_ui = null;
    public Text healthbartext = null;

	// Use this for initialization
	void Start ()
    {
        soulText = GameObject.Find("Soul_Text").GetComponent<Text>();
        mineralText = GameObject.Find("Mineral_Text").GetComponent<Text>();
        healthbar_ui = GameObject.Find("P2_HealthBar").transform;
        healthbartext = GameObject.Find("HealthText").GetComponent<Text>();

        m_P2_hp = m_P2_hpCap;
    }

    // Update is called once per frame
    void Update ()
    {
        // Update UI Text Information
        soulText.text = "Souls : " + m_Souls;
        mineralText.text = "Minerals : " + m_Minerals;

        // Update HP UI
        healthbar_ui.GetComponent<Image>().fillAmount = 1 * ((float)m_P2_hp / (float)m_P2_hpCap);
        //Debug.Log("Fill amount " + healthbar_ui.GetComponent<Image>().fillAmount);
        healthbartext.text ="HP: " + m_P2_hp;
        _DebugFunc();
	}

    void _DebugFunc()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            ++m_Minerals;
            ++m_Souls;
        }

        if (Input.GetKey(KeyCode.B))
        {
            m_P2_hp++;
            if (m_P2_hp < 0)
            {
                m_P2_hp = 0;
            }
            else if (m_P2_hp > m_P2_hpCap)
            {
                m_P2_hp = m_P2_hpCap;
            }
        }
    }

    
}
