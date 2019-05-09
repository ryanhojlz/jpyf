using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Besides this script I know theres alot of handlers
// But as of now the general thinking is this script handle a specific e.g
// e.g this script handles the values and certain triggers of the game
// While scripts like Object Control is suppose to manage the controls of the object
// While ps4 controller script is just for checking axis and button presses

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

    public int m_MineralsCap = 150;
    public int m_SoulsCap = 150;

    public float m_spawnMultiplier = 0;

    // Some game logic / boolean
    public float m_LanternTimerTick = 1;
    float m_LanternTimerTickReference = 0;
    // Controls lantern ticking
    public bool m_StartLanternTick = false;


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
    
    public Transform digetic_mineral_ui = null;
    public Transform digetic_soul_ui = null;


    // Lantern Light
    public Transform LanternLight = null;

    public int i_num_enemies_spawn = 0;

    float m_startTicking = 6;

    // If player dies
    public bool playerDead = false;

    // Enemy Count
    public int EnemyCount = 0;

   
    //
    public static Stats_ResourceScript Instance = null;

    bool pauseGame = false;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

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

        // Digetic UI
        digetic_mineral_ui = GameObject.Find("DigeticMineralBar").transform;
        digetic_soul_ui = GameObject.Find("DigeticSoulBar").transform;


        // Lanter Light
        LanternLight = GameObject.Find("LanternLight").transform;

        //m_StartLanternTick = true;

        // Assign Values Initial game init
        m_P2_hp = m_P2_hpCap;
        //m_P2_hp = 0;
        m_CartHP = 0;
        m_LanternHp = 0;
        m_Minerals = 0;
        m_Souls = 0;
        m_LanternTimerTickReference = m_LanternTimerTick;
    }

    // Update is called once per frame
    void Update ()
    {
        //PauseGame();
        PS4_UI();
        PSVR_UI();
        
        // Its just one line for now be if expanded i will put in func
        LanternGameplay();
        //LanternLight.GetComponent<Light>().intensity = 2 * ((float)m_LanternHp / (float)m_LanternHpCap);
        // Check if play is dead
        CheckPlayer2Dead();

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

        // Update Mineral UI
        digetic_mineral_ui.GetComponent<Image>().fillAmount = 1 * ((float)m_Minerals / (float)m_MineralsCap);

        // Update Mineral UI
        digetic_soul_ui.GetComponent<Image>().fillAmount = 1 * ((float)m_Souls / (float)m_SoulsCap);


    }

    public void ResetStats()
    {
        m_P2_hp = 100;
        m_LanternHp = 100;
        m_CartHP = 100;

        m_Minerals = 50;
        m_Souls = 50;

    }

    void _DebugFunc()
    {
        GameObject.Find("Text2").GetComponent<Text>().text = "Number of enemies " + EnemyCount;
        if (Input.GetKeyDown(KeyCode.V))
        {
            ++m_Minerals;
            ++m_Souls;
            m_P2_hp += 35;

            if (m_P2_hp < 0)
            {
                m_P2_hp = 0;
            }
            else if (m_P2_hp > m_P2_hpCap)
            {
                m_P2_hp = m_P2_hpCap;
            }

        }

        if (Input.GetKeyDown(KeyCode.B))
        {
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

        // Debug function to kill the player
        if (Input.GetKeyDown(KeyCode.Delete))
        {
            m_P2_hp = 0;
            m_CartHP = 0;
        }

    }

    void PauseGame()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (pauseGame)
            {
                pauseGame = false;
                Time.timeScale = 0;
            }
            else if (!pauseGame)
            {
                pauseGame = true;
                Time.timeScale = 1;
            }
        }
        
    }

    void LanternGameplay()
    {


        // Lantern ticks
        if (m_StartLanternTick)
        {
            m_LanternTimerTick -= 5 * Time.deltaTime;
            if (m_LanternTimerTick <= 0)
            {
                Lantern_TakeDmg(1);
                m_LanternTimerTick = m_LanternTimerTickReference;
            }
        }
        //else if (!m_StartLanternTick)
        //{
        //    m_startTicking -= 1 * Time.deltaTime;
        //    if (m_startTicking <= 0)
        //    {
        //        m_StartLanternTick = true;
        //    }
        //}

        //m_spawnMultiplier = (m_LanternHpCap - m_LanternHp) * 0.01f;

        // hardcode
        if (m_LanternHp >= m_LanternHpCap * 0.7f)
        {
            m_spawnMultiplier = 0;
        }
        else if (m_LanternHp >= m_LanternHpCap * 0.5f)
        {
            m_spawnMultiplier = 0.1f;
        }
        else if (m_LanternHp >= m_LanternHpCap * 0.2f)
        {
            m_spawnMultiplier = 0.15f;
        }
        else if (m_LanternHp <= m_LanternHpCap * 0.0f)
        {
            m_spawnMultiplier = 0.2f;
        }


        if (m_LanternHp >= m_LanternHpCap * 0.9f)
        {
            i_num_enemies_spawn = 0;
        }
        else if (m_LanternHp >= m_LanternHpCap * 0.7)
        {
            i_num_enemies_spawn = 1;
        }
        else if (m_LanternHp >= m_LanternHpCap * 0.5)
        {
            i_num_enemies_spawn = 1;
        }
        else if (m_LanternHp >= m_LanternHpCap * 0.3)
        {
            i_num_enemies_spawn = 3;
        }
        




        // Update Light component
        LanternLight.GetComponent<Light>().intensity = 2.5f * ((float)m_LanternHp / (float)m_LanternHpCap);
    }
    
    public void Cart_TakeDmg(int damage)
    {
        GameObject.Find("FeedBackHandler").GetComponent<FeedbackHandler>().HitPayload();
        // if my supervisor was mr toh he would have just put a plus and parameter would be a minus but not in this house
        m_CartHP -= damage;
        if (m_CartHP < 0)
        {
            m_CartHP = 0;
        }

        if (m_CartHP > m_CartHpCap)
        {
            m_CartHP = m_CartHpCap;
        }
    }

    public void Lantern_TakeDmg(int damage)
    {
        m_LanternHp -= damage;
        if (m_LanternHp < 0)
        {
            m_LanternHp = 0;
        }
        else if (m_LanternHp > m_LanternHpCap)
        {
            m_LanternHp = m_LanternHpCap;
        }
    }

    public void Player2_TakeDmg(int damage)
    {
        m_P2_hp -= damage;
        if (m_P2_hp < 0)
        {
            m_P2_hp = 0;
        }
        else if (m_P2_hp > m_P2_hpCap)
        {
            m_P2_hp = m_P2_hpCap;
        }
    }

    public void ProcessPickUp(Pickup_Scripts item)
    {
        switch (item.id)
        {
            case 1: // Souls
                m_Souls += 20;
                //Destroy(item);
                break;
            case 2: // Minerals
                m_Minerals += 30;
                //Destroy(item);
                break;
            case 3: // Small Souls
                m_Souls += 10;
                //Destroy(item);
                break;
            case 4:
                m_Souls += 40;
                //Destroy(item);
                break;

        }

        //Debug.Log("Hey hey im being processed");
    }

    public void ConsumeSouls(int souls)
    {
        this.m_Souls -= souls;
        if (m_Souls < 0)
        {
            m_Souls = 0;
        }
        else if (m_Souls > m_SoulsCap)
        {
            m_SoulsCap = m_Souls;
        }
    }

    public void CheckPlayer2Dead()
    {
        //Debug.Log("Player dead " + playerDead);
        if (!playerDead)
        {
            // Half current lantern 
            if (m_P2_hp <= 0)
            {
                m_LanternHp = m_LanternHp / 2;
                playerDead = true;
            }
        }
        else if (playerDead)
        {
            if (m_P2_hp >= 100)
            {
                playerDead = false;
            }
        }
        
    }

   


   
}
