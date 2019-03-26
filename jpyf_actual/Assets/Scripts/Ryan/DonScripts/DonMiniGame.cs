using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DonMiniGame : MonoBehaviour
{
    public bool play_minigame = false;
    public float countdown_timer = 3;
    public bool actual_play_boolean = false;

    public float minigame_length = 3.0f;
    // Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        /// For a countdown timer to give feedback to player when the mini game is starting
        if (play_minigame)
        {
            countdown_timer -= 1 * Time.deltaTime;
            if (countdown_timer < 0)
            {
                actual_play_boolean = true;
                play_minigame = false;
            }
        }

        /// for the actual mini game to plau
        if (actual_play_boolean)
        {
            if (minigame_length < 0)
            {
                minigame_length -= 1 * Time.deltaTime;
            }
        }



	}

    public void SetMiniGame(bool boolean)
    {
        play_minigame = boolean;
    }

   
}
