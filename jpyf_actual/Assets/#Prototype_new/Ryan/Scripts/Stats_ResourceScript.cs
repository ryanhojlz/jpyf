using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Stats_ResourceScript : MonoBehaviour
{
    // Values etc I shoud Really Split them up into multiple scripts but really atm i no time
    // Like cart health handler or just like p2 health bar handler
    // Player
    public int m_P2_hp = 0;
    public int m_P2_hpCap = 100;

    // Cart health
    public int m_CartHP= 0;
    public int m_CartHpCap = 100;

    // Lamp Health
    public int m_LanternHp = 0;
    public int m_LanternHpCap = 100;

    /// Resources
    public int m_Minerals = 0;
    public int m_Souls = 0;

    /// Text ui debug etc
    /// Minerals
    public Text soulText = null;
    public Text mineralText = null;

    /// Health bar P2
    public Transform healthbar_ui = null;
    public Text healthbartext = null;

    /// Cart Health bar
    public Transform cart_healthbar_ui = null;
    public Text cart_healthbar_text = null;

    /// Lantern UI
    public Transform lantern_ui = null;
    public Text lantern_ui_text = null;

    // Digetic UI
    public Transform digetic_cart_healthbar_ui = null;
    public Transform digetic_lantern_ui = null;

    // Use this for initialization
    void Start ()
    {
        /// Resources
        soulText = GameObject.Find("Soul_Text").GetComponent<Text>();
        mineralText = GameObject.Find("Mineral_Text").GetComponent<Text>();

        // Health UI
        healthbar_ui = GameObject.Find("P2_HealthBar").transform;
        healthbartext = GameObject.Find("HealthText").GetComponent<Text>();

        // Cart Health UI
        cart_healthbar_ui = GameObject.Find("Cart_HealthBar").transform;
        cart_healthbar_text = GameObject.Find("CartText").GetComponent<Text>();

        // Lantern UI
        lantern_ui = GameObject.Find("LampBar").transform;
        lantern_ui_text = GameObject.Find("LanternText").GetComponent<Text>();

        // Digetic UI
        digetic_cart_healthbar_ui = GameObject.Find("DigeticHealthBar").transform;
        digetic_lantern_ui = GameObject.Find("DigeticLampBar").transform;



        // Assign
        m_P2_hp = m_P2_hpCap;
    }

    // Update is called once per frame
    void Update ()
    {
        PS4_UI();
        PSVR_UI();

        // Debug Function
        _DebugFunc();
	}


    void PS4_UI()
    {
        /// Update UI Text Information
        soulText.text = "Souls : " + m_Souls;
        mineralText.text = "Minerals : " + m_Minerals;

        /// Update HP UI
        healthbar_ui.GetComponent<Image>().fillAmount = 1 * ((float)m_P2_hp / (float)m_P2_hpCap);
        healthbartext.text = "HP: " + m_P2_hp;

        /// Update Cart UI
        cart_healthbar_ui.GetComponent<Image>().fillAmount = 1 * ((float)m_CartHP / (float)m_CartHpCap);
        cart_healthbar_text.text = "HP Cart: " + m_CartHP;

        /// Update Lamp UI
        lantern_ui.GetComponent<Image>().fillAmount = 1 * ((float)m_LanternHp / (float)m_LanternHpCap);
        lantern_ui_text.text = "Lantern: " + m_LanternHp;

    }

    void PSVR_UI()
    {
        /// Update Cart UI
        digetic_cart_healthbar_ui.GetComponent<Image>().fillAmount = 1 * ((float)m_CartHP / (float)m_CartHpCap);

        /// Update Lamp UI
        digetic_lantern_ui.GetComponent<Image>().fillAmount = 1 * ((float)m_LanternHp / (float)m_LanternHpCap);

    }

    void _DebugFunc()
    {

        if (Input.GetKeyDown(KeyCode.V))
        {
            ++m_Minerals;
            ++m_Souls;
        }

        if (Input.GetKeyDown(KeyCode.B))
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


            m_CartHP += 10;
            if (m_CartHP < 0)
            {
                m_CartHP = 0;
            }
            else if (m_CartHP > m_CartHpCap)
            {
                m_CartHP = m_CartHpCap;
            }

        }

        if (Input.GetKeyDown(KeyCode.N))
        {
            m_LanternHp += 10;
            if (m_LanternHp < 0)
            {
                m_LanternHp = 0;
            }
            else if (m_LanternHp > m_LanternHpCap)
            {
                m_LanternHp = m_LanternHpCap;
            }

        }

    }


    

    
}
