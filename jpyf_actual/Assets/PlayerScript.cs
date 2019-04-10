using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{
    public int Mana = 0;
    float timer;
    
    // Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        GameObject.Find("DebugMana").GetComponent<Text>().text = "Mana cost " + Mana;
        GameObject.Find("ManaText").GetComponent<TextMesh>().text = "Mana " + Mana;
        timer += 1 * Time.deltaTime;
        if (timer > 4)
        {
            Mana++;
            if (Mana > 10)
            {
                Mana = 10;
            }
            timer = 0;
        }
    }
}
