using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GameEventsManager : MonoBehaviour
{
    // Game Time
    float time = 0;

    // Win Lose Conditions
    // Lose = false Win = true;
    bool WinLose = false;
    bool Draw = false;
    // End Game Boolean // Allows for double K.O
    bool EndGame = false;

    //GameObjects
    public GameObject ally_nexus = null;
    public GameObject enemy_nexus = null;
    public GameObject SpawnManager = null;

    // Demo Purposes
    public GameObject ally_wall = null;
    public GameObject enemy_wall = null;

    // Data for main nexus tower
    int Ally_NexusHP = 100;
    int Enemy_NexusHP = 100;
    // Let towers regenerate health slowly
    int Ally_NexusHPCap = 100;
    int Enemy_NexusHPCap = 100;

    // Gold
    int PlayerGold = 100;
    float GoldTimer = 0;
    float GoldTrigger = 1.5f;

    // Tier for upgrading each unit 
    int Tier1 = 150;
    int Tier2 = 300;
    int Tier3 = 500;

    // UI win lose
    public GameObject winlosetext;
    public GameObject winlosetext2;

    // timer for turning on spawner
    public float TimeToStartSpawning = 5.2f;
    public bool StartSpawning = false;
    
    // Use this for initialization
    void Start()
    {
        if (!SpawnManager)
            SpawnManager = GameObject.Find("SpawnManager");

        // Find walls and enemy walls
        ally_wall = GameObject.Find("wall");
        enemy_wall = GameObject.Find("enemyWall");
        // UI text 
        winlosetext = GameObject.Find("WinLoseText").gameObject;
        winlosetext2 = GameObject.Find("WinLoseText2").gameObject;
        // Spawner

        // Set the spawn manager to false first
        SpawnManager.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        UpdateWinLose();
        SpawnerEvents();
        UpdatePlayerGold();
    }

    // Update Win Lose Condition
    void UpdateWinLose()
    {
        //if (!EndGame)
        //{
        //    // If the same frame they somehow both have no health
        //    if (Ally_NexusHP <= 0 && Enemy_NexusHP <= 0)
        //    {
        //        Draw = true;
        //        EndGame = true;
        //    }
        //    // else if 
        //    if (Ally_NexusHP <= 0)
        //    {
        //        WinLose = false;
        //        EndGame = true;
        //    }
        //    else if (Enemy_NexusHP <= 0)
        //    {
        //        WinLose = true;
        //        EndGame = true;
        //    }
        //}

        // This is for the week 6 demo
        // If the gamne ended 
        if (!EndGame)
        {
            // If ally hp 0 lose
            if (ally_wall.GetComponent<BasicGameOBJ>().healthValue <= 0)
            {
                EndGame = true;
                WinLose = false;
                // Change Text
                winlosetext.GetComponent<TextMesh>().text = "PLAYER LOSE";
                winlosetext2.GetComponent<TextMesh>().text = "PLAYER LOSE";
            }
            // If enemy hp 0 win
            else if (enemy_wall.GetComponent<BasicGameOBJ>().healthValue <= 0)
            {
                EndGame = true;
                WinLose = true;
                // Change Text
                winlosetext2.GetComponent<TextMesh>().text = "PLAYER WIN";
                winlosetext.GetComponent<TextMesh>().text = "PLAYER WIN";
            }
            // If game ended render
            winlosetext.SetActive(false);
            winlosetext2.SetActive(false);
        }
        else
        {
            // If game ended render
            winlosetext2.SetActive(true);
            winlosetext.SetActive(true);
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
        // Gold tick timer
        GoldTimer += 0.5f * Time.deltaTime;
        // Capacity
        if (PlayerGold > 999)
            PlayerGold = 999;
    }


    // Spawner Events
    void SpawnerEvents()
    {
        TimeToStartSpawning -= 1 * Time.deltaTime;
        if (TimeToStartSpawning <= 0)
        {
            SpawnManager.SetActive(true);
            TimeToStartSpawning = 0;
        }
    }

    
    // These are planned when the nexus objects are in 
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
