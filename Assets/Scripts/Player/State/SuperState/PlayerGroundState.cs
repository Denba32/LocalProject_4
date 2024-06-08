using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundState : PlayerState
{
    private float weight = 0f;
    private bool isGround = false;
    public PlayerGroundState(PlayerController character, PlayerData playerData, Animator animator, int hashParameter) : base(character, playerData, animator, hashParameter)
    {
        weight = this.playerData.weight;
    }

    public override void DoChecks()
    {
        base.DoChecks();
        animator.SetFloat(hashYVelocity, 0);
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
    }

    public override void OnStateUpdate()
    {
        base.OnStateUpdate();

        #region Transition 

        isGround = CheckGround();


        Debug.Log("지상 상태" + isGround);

        if (!isExitingState)
        {
            if(isGround)
            {
                if (player.isJump)
                {
                    Debug.Log("Ground -> Jump");
                    finiteStateMachine.ChangeState(player.JumpState);
                }
            }
            else
            {
                if (player.rigidBody.velocity.y <= -1f)
                {
                    finiteStateMachine.ChangeState(player.AirState);

                }
            }
        }

        #endregion
    }
}
