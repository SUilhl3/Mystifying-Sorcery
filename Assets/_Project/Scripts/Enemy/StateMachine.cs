using System.Collections.Generic;
using UnityEngine;

public class StateMachine
{
    public class State
    {
        public string name;
        public System.Action onEnter;
        public System.Action onExit;
        public System.Action onStay;

    }

    Dictionary<string, State> states = new Dictionary<string, State>();

    [SerializeField] State currentState;
    [SerializeField] State initialState;

    public State CreateState(string name)
    {
        var newState = new State();
        newState.name = name;
        if (states.Count == 0)
        {
            initialState = newState;
        }
        states[name] = newState;
        return newState;
    }

    public void Update()
    {
        if (states.Count == 0 || initialState == null)
        {
            Debug.Log("No states");
        }
        if (currentState == null)
        {
            TransitionTo(initialState);
        }

        if (currentState.onStay != null)
        {
            currentState.onStay();
        }
    }

    public void TransitionTo(State newState)
    {
        if (newState == null)
        {
            Debug.Log("new state is null");
            return;
        }

        if (currentState != null && currentState.onExit != null)
        {
            currentState.onExit();
        }

        currentState = newState;

        if(newState.onEnter != null)
        {
            newState.onEnter();
        }
    }

    public void TransitionTo(string newStateName)
    {
        if (!states.ContainsKey(newStateName))
        {
            Debug.Log("doesn't contain state + " +  newStateName);
            return;
        }
        var state = states[newStateName];
        TransitionTo(state);
    }

}
