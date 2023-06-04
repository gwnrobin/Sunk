using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class FSM 
{
    private State currentState;

    internal virtual void Initialize()
    {
        GotoState(GetInitialState());
    }

    internal void Update()
    {
        currentState?.Update();
    }

    internal void GotoState(State state)
    {
        currentState?.Exit();
        
        currentState = state;

        currentState?.Enter();
    }

    protected abstract State GetInitialState();
}
