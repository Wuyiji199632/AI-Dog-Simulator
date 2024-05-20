using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DogState
{
    Idle,
    Hungry,
    Thirsty,
    Tired,
    Playing,
    SeekingAttention,
    FollowingCommand
}

public abstract class State
{
    protected DogFSM fsm;

    public State(DogFSM fsm)
    {
        this.fsm = fsm;
    }

    public abstract void Enter();
    public abstract void Execute();
    public abstract void Exit();
}
