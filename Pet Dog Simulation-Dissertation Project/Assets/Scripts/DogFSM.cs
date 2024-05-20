using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

public class DogFSM : MonoBehaviour
{
    private State currentState;

    public Animator animator;

    void Start()
    {
        currentState = new IdleState(this);
        currentState.Enter();
    }

    void Update()
    {
        currentState.Execute();
    }

    public void ChangeState(State newState)
    {
        currentState.Exit();
        currentState = newState;
        currentState.Enter();
    }

    public bool IsHungry() { return false/* logic to determine if dog is hungry */; }
    public bool IsThirsty() { return false /* logic to determine if dog is thirsty */; }
    public bool IsTired() { return false /* logic to determine if dog is tired */; }
    public bool PlayerWantsToPlay() { return false/* logic to determine if player wants to play */; }
    public bool WantsAttention() { return false /* logic to determine if dog wants attention */; }
    public bool PlayerGivesCommand() { return false/* logic to determine if player gives a command */; }
}
