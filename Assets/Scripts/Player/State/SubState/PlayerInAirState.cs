using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

/// <summary>
/// 공중
/// </summary>
public class PlayerInAirState : PlayerState
{
    private bool isGround = false;

    private float yVelocity;

    private float weight;


    private float moveSpeed;
    private float turnSpeed = 20.0f;
    public PlayerInAirState(PlayerController character, PlayerData playerData, Animator animator, int hashParameter) : base(character, playerData, animator, hashParameter)
    {
        weight = playerData.weight;
        moveSpeed = this.playerData.moveSpeed;
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void OnStateEnter()
    {
        base.OnStateEnter();

        Debug.Log("Player In AirState");
    }

    public override void OnStateExit()
    {
        base.OnStateExit();
    }
    public override void OnStateUpdate()
    {
        base.OnStateUpdate();

        isGround = CheckGround();

        animator.SetFloat(hashYVelocity, player.rigidBody.velocity.y);

        #region Transition

        Debug.Log("낙하 상태의 y가속도 : " + player.rigidBody.velocity.y);
        if (player.rigidBody.velocity.y < 0.01f && isGround)
        {
            finiteStateMachine.ChangeState(player.LandState);
        }

        Turn(turnSpeed);
        #endregion
    }

    public override void OnStateFixedUpdate()
    {
        base.OnStateFixedUpdate();

        Move(moveSpeed);

    }


}