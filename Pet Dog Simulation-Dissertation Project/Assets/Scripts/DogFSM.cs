using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

public class DogFSM : MonoBehaviour
{
    public State currentState;

    public Animator animator;

    public PlayerMovement player;

    void Start()
    {
        currentState = new IdleState(this);
        currentState.Enter();
    }

    void Update()
    {
        float distance = Vector3.Distance(player.transform.position, transform.parent.position);

        if (distance <= 8)
        {
            LookAtPlayer();
            Debug.Log("Player is close");
        }
       
      
    }

    private void LateUpdate()
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

    private void LookAtPlayer()
    {
        Vector3 direction = (player.transform.position - transform.parent.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.parent.rotation = Quaternion.Slerp(transform.parent.rotation, lookRotation, Time.deltaTime * 10f);
       
    }

    public bool isAlignedWithPlayer()
    {
        return false;
    }
}
