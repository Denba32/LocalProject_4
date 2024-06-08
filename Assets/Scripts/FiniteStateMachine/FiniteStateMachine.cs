using System;
using UnityEngine;
using UnityEngine.XR;

public class FiniteStateMachine
{
    public BaseState CurrentState { get; private set; }

    public FiniteStateMachine() { }
    public FiniteStateMachine(BaseState baseState)
    {
        CurrentState = baseState;
        ChangeState(CurrentState);
    }

    public void Initialize(BaseState baseState)
    {
        if(baseState == CurrentState)
        {
            Debug.Log("State가 존재하지 않습니다.");
            return;
        }
        CurrentState = baseState;
        CurrentState.OnStateEnter();
    }
    public void ChangeState(BaseState baseState)
    {
        if (baseState == null)
        {
            Debug.Log("State가 존재하지 않습니다.");
            return;
        }

        if(CurrentState == baseState)
        {
            return;
        }

        CurrentState.OnStateExit();

        CurrentState = baseState;

        CurrentState.OnStateEnter();
    }
}
