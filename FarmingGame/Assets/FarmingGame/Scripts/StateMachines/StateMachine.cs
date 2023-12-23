using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
    protected State _currentState;
   
    public void SwitchState(State newState)
    {
        _currentState?.ExitState();
        _currentState = newState;
        _currentState.EnterState();
        Debug.Log(_currentState);
    }

        
}
