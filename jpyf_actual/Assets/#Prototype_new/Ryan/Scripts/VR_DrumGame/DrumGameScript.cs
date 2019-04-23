using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class DrumGameScript : MonoBehaviour
{
    
    // Drum stick Objects for Game
    public Transform m_LeftDrumstick = null;
    public Transform m_RightDrumStick = null;

   

    // Boolean to check minigame
    public bool b_miniGamePlay = false;

    // Boolean way
    //public bool b_Left_StickHit = false;
    //public bool b_Right_StickHit = false;

    // Cart Reference
    public Push_CartScript m_pushCartRef = null;

    // Counter way
    public int i_hitCounter = 0;

    // Game Activity Timer .. e.g refresh timer everytime an action is taken
    public float f_activity_timer = 0;
    
	// Use this for initialization
	void Start ()
    {
        // Initializing childs
        m_RightDrumStick = this.transform.parent.GetChild(1).GetChild(0);
        m_LeftDrumstick = this.transform.parent.GetChild(2).GetChild(0);
        m_pushCartRef = GameObject.Find("PushingObjects").transform.GetChild(2).GetComponent<Push_CartScript>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (b_miniGamePlay)
        {

        }
        else if (!b_miniGamePlay)
        {

        }
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform == m_LeftDrumstick || other.transform == m_RightDrumStick)
        {
            // When i hit the counter goes up by 1
            ++i_hitCounter;
            Debug.Log("Got hitto");
            b_miniGamePlay = true;
            f_activity_timer = 10;
        }
    }


}
