using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnState : State
{
    public TurnState(DogFSM fsm) : base(fsm)
    {
    }

    public override void Enter()
    {
        fsm.animator.SetTrigger("Turn");
    }

    public override void Execute()
    {
      
    }

    public override void Exit()
    {
        
    }


}
