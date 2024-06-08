using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : PlayerGroundState
{
    public PlayerIdleState(PlayerController character, PlayerData playerData, Animator animator, int hashParameter) : base(character, playerData, animator, hashParameter)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void OnStateEnter()
    {
        base.OnStateEnter();

        Debug.Log("À¯ÈÞ »óÅÂ");

        Stop();
    }

    public override void OnStateExit()
    {
        base.OnStateExit();
    }

    public override void OnStateUpdate()
    {
        base.OnStateUpdate();

        if (!isExitingState)
        {
            // Change To Move State
            if (player.curMovementInput != Vector2.zero)
            {
                finiteStateMachine.ChangeState(player.MoveState);
            }
        }
    }
    void Stop() => player.rigidBody.velocity = Vector3.zero;
}
