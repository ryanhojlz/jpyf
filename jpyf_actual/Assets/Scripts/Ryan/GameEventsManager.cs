using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEventsManager : MonoBehaviour
{
    // Game Time
    float time = 0;

    // Win Lose Conditions
    // Lose = false Win = true;
    bool WinLose = false;
    bool Draw = false;
    // End Game Boolean // Allows for double K.O
    bool EndGame= false;

    //GameObjects
    public GameObject ally_nexus = null;
    public GameObject enemy_nexus = null;
    public GameObject SpawnManager = null;

    // Data for main nexus tower
    int Ally_NexusHP = 100;
    int Enemy_NexusHP = 100;
    // Let towers regenerate health slowly
    int Ally_NexusHPCap = 100;
    int Enemy_NexusHPCap = 100;

    int PlayerGold = 100;
    float GoldTimer = 0;
    float GoldTrigger = 1.5f;

    int Tier1 = 150;
    int Tier2 = 300;
    int Tier3 = 500;


    // Use this for initialization
	void Start ()
    {
        if (!SpawnManager)
            GameObject.Find("SpawnManager");
	}
	
	// Update is called once per frame
	void Update ()
    {
        UpdateWinLose();
        UpdatePlayerGold();
	}

    // Update Win Lose Condition
    void UpdateWinLose()
    {
        if (!EndGame)
        {
            // If the same frame they somehow both have no health
            if (Ally_NexusHP <= 0 && Enemy_NexusHP <= 0)
            {
                Draw = true;
                EndGame = true;
            }
            // else if 
            if (Ally_NexusHP <= 0)
            {
                WinLose = false;
                EndGame = true;
            }
            else if (Enemy_NexusHP <= 0)
            {
                WinLose = true;
                EndGame = true;
            }
        }   
    }

    // Update Player Gold
    void UpdatePlayerGold()
    {
        if (GoldTimer >= GoldTrigger)
        {
            GoldTimer = 0;
            PlayerGold++;
        }
        GoldTimer += 0.5f * Time.deltaTime;
    }


    // For readability instead of seeing hp -= damage;
    // Plus Minus function for damage
    void AffectAllyHp(int damage)
    {
        Ally_NexusHP += damage;
        if (Ally_NexusHP < 0)
        {
            Ally_NexusHP = 0;
        }
        if (Ally_NexusHP > Ally_NexusHPCap)
        {
            Ally_NexusHP = Ally_NexusHPCap;
        }
    }

    // Plus Minus function for damage to enemy
    void AffectEnemyHp(int damage)
    {
        Enemy_NexusHP += damage;
        if (Enemy_NexusHP < 0)
        {
            Enemy_NexusHP = 0;
        }
        if (Enemy_NexusHP > Enemy_NexusHPCap)
        {
            Enemy_NexusHP = Enemy_NexusHPCap;
        }
    }

    
    
    
}
