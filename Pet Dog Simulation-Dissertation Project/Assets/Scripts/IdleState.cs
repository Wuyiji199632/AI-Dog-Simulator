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
    }

    public override void Execute()
    {
        Debug.Log("I am idling!");
    }

    public override void Exit()
    {
        throw new System.NotImplementedException();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
