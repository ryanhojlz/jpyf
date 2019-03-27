using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_PS4
using UnityEngine.PS4;
using UnityEngine.PS4.VR;
using UnityEngine.XR;
#endif

public class ControllerManager : MonoBehaviour
{
    // Num of User to detect
    public int numUsers;
    // Number of controllers you choose to connect
    public List<int> m_controller;

    // List of controller gameobjects if there is time to make dynamic make dynamic for now hardcode
    public List<GameObject> m_list_of_playerobj;

	// Use this for initialization
	void Start ()
    {
#if UNITY_PS4
        // Search of user to detect
        for (int i = 0; i < numUsers; i++)
        {
            var user = PS4Input.GetUsersDetails(i);
            // If is valid user, if 0 = means that the user on the PS4 is not detected
            if (user.status != 0)
            {
                // Push to Index
                m_controller.Add(i);
                m_list_of_playerobj[i].GetComponent<Test_PlayerObject>()._playerId = i;
                m_list_of_playerobj[i].GetComponent<Test_PlayerObject>()._stickid = i + 1;
                //m_list_of_playerobj[i].GetComponent<Test_PlayerObject>().ChangePadColor(i);
            }
        }
#endif
    }

    // Using Awake here because Manager needs to be turn on first
    private void Awake()
    {

    }

    // Update is called once per frame
    void Update ()
    {
		
	}


}
