using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit_StateMachine
{

    private class StateList
    {
        string name;
        IState State;

        public StateList(string _name, IState _State)//Public constructor
        {
            name = _name;
            State = _State;
        }

        public IState GetState() { return State; }
        public string GetName() {return name; }
    }

    List<StateList> ListOfStates = new List<StateList>();

    private IState currentlyRunningState;
    private IState previousState;

    public void ChangeState(string statename)
    {
        //Debug.Log(statename);

        if (FindState(statename) == null)
        {
            Debug.Log("State : '" + statename + "' not found state not changed");
            return;
        }

        if (this.currentlyRunningState != null)
        {
            this.currentlyRunningState.Exit();//Exiting the state
        }


        this.previousState = this.currentlyRunningState;

        this.currentlyRunningState = FindState(statename);

        this.currentlyRunningState.Enter();
    }

    public void ExecuteStateUpdate()
    {
        var runningState = this.currentlyRunningState;
        if (runningState != null)
        {
            runningState.Execute();
        }
    }

    public string GetCurrentStateName()
    {
        return FindStateName(currentlyRunningState);
    }

    public IState GetCurrentState()
    {
        return currentlyRunningState;
    }

    public IState GetPreviousState()
    {
        return previousState;
    }

    public void AddState(string _name, IState _state)
    {
        for (int i = 0; i < ListOfStates.Count; ++i)
        {
            if (ListOfStates[i].GetName() == _name)
            {
                Debug.Log(_name + " Cannot be added to list");
                return;
            }
        }

        ListOfStates.Add(new StateList(_name, _state));
    }

    IState FindState(string _name)
    {
        IState ToReturn = null;

        for (int i = 0; i < ListOfStates.Count; ++i)
        {
            if (ListOfStates[i].GetName() == _name)
            {
                ToReturn = ListOfStates[i].GetState();
                break;//Since found, no longer need to continue looping
            }
        }

        return ToReturn;
    }

    string FindStateName(IState _state)
    {
        string ToReturn = null;

        for (int i = 0; i < ListOfStates.Count; ++i)
        {
            if (ListOfStates[i].GetState() == _state)
            {
                ToReturn = ListOfStates[i].GetName();
            }
        }

        return ToReturn; 
    }

}
