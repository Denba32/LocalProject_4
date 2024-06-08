using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerSprintState : PlayerGroundState
{
    private Coroutine co_Sprint;
    private float turnSpeed = 20f;
    private float sprintSpeed;
    private float runTime = 3f;
    public PlayerSprintState(PlayerController character, PlayerData playerData, Animator animator, int hashParameter) : base(character, playerData, animator, hashParameter)
    {
        sprintSpeed = this.playerData.moveSpeed;
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void OnStateEnter()
    {
        base.OnStateEnter();
        Debug.Log("버프 Sprint 상태");


        if (co_Sprint != null)
        {
            player.StopCo(co_Sprint);
        }
        co_Sprint = player.StartSprint(sprintSpeed, runTime);
        sprintSpeed = playerData.moveSpeed;
    }

    public override void OnStateExit()
    {
        base.OnStateExit();
    }
    public override void OnStateUpdate()
    {
        base.OnStateUpdate();

        Turn(turnSpeed);
        if(!player.isSprint)
        {
            if(player.isRun)
            {
                finiteStateMachine.ChangeState(player.MoveState);

            }
            else
            {
                finiteStateMachine.ChangeState(player.IdleState);

            }
        }
    }

    public override void OnStateFixedUpdate()
    {
        base.OnStateFixedUpdate();
        Sprint(sprintSpeed);
    }

}