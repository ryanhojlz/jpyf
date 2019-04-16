using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit_StateMachine : MonoBehaviour
{
    public enum States
    {
        None,
        Attacking,
        Chasing,
        Dead,
    }

    States m_currentState = States.None;
    States m_nextState = States.None;
    States m_previousState = States.None;

    // Use this for initialization
    void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    void ChangeState(States _nextState)
    {
        
    }

}
