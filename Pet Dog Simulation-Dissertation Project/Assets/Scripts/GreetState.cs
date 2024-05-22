using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreetState : State
{
    public GreetState(DogFSM fsm) : base(fsm)
    {
    }

    public override void Enter()
    {
        fsm.animator.SetTrigger("Greet");
    }

    public override void Execute()
    {
        Debug.Log("I am greeting!");
    }

    public override void Exit()
    {
       
    }
}
