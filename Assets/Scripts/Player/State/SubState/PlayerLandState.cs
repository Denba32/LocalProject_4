using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// TODO : 관리 필요
public class PlayerLandState : PlayerGroundState
{
    private bool isGround = false;
    public PlayerLandState(PlayerController character, PlayerData playerData, Animator animator, int hashParameter) : base(character, playerData, animator, hashParameter)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void OnStateEnter()
    {
        base.OnStateEnter();


    }

    public override void OnStateExit()
    {
        base.OnStateExit();
    }

    public override void OnStateFixedUpdate()
    {
        base.OnStateFixedUpdate();
        isGround = CheckGround();
    }

    public override void OnStateUpdate()
    {
        base.OnStateUpdate();

        if(!isExitingState)
        {
            if (isGround)
            {
                finiteStateMachine.ChangeState(player.IdleState);
            }
        }
    }
}
