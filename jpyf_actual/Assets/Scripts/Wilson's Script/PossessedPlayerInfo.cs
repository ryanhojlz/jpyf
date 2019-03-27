using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PossessedPlayerInfo : MonoBehaviour
{
    GameObject playerReference;
    public Image healthBar;
    public Image manaBar;
    public List<Image> minions;
    float playerHealth;
    float playerStartHealth;
    float playerMana;
    float playerMaxMana;

    // Use this for initialization
    void Start()
    {
        playerReference = GameObject.Find("Player_object");
    }

    // Update is called once per frame
    void Update()
    {
        
        if (playerReference.gameObject.GetComponent<Minion>())
        {
            playerStartHealth = playerReference.gameObject.GetComponent<Minion>().startHealthvalue;
            playerHealth = playerReference.gameObject.GetComponent<Minion>().healthValue;
            playerMana = playerReference.GetComponent<PlayerScript>().Mana;
        }
        else
        {
            playerStartHealth = 1;
            playerHealth = 0;
            playerMaxMana = 1;
            playerMana = 0;
        }

        healthBar.fillAmount = playerHealth / playerStartHealth;
        manaBar.fillAmount = playerMana / playerMaxMana;
    }
}
