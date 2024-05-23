using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : State
{
    public IdleState(DogFSM fsm) : base(fsm)
    {
    }

    public override void Enter()
    {
        Debug.Log("Entering Idle State");
        fsm.animator.SetTrigger("Idle");
        
    }

    public override void Execute()
    {
        Debug.Log("I am idling!");
    }

    public override void Exit()
    {
       
    }

   
}
