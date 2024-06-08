using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpState : PlayerAbilityState
{
    private float jumpPower;
    private float moveSpeed;

    private float yVelocity = 0;
    private float limitedYVelocity = 5f;

    private float turnSpeed = 20f;
    public PlayerJumpState(PlayerController character, PlayerData playerData, Animator animator, int hashParameter) : base(character, playerData, animator, hashParameter)
    {
        jumpPower = this.playerData.jumpPower;
        moveSpeed = this.playerData.moveSpeed;
    }

    public override void DoChecks()
    {
        base.DoChecks();

        Debug.Log("Jump State");
    }

    public override void OnStateEnter()
    {
        base.OnStateEnter();

        Jump();
        isAbilitydone = true;
    }

    public override void OnStateExit()
    {
        base.OnStateExit();
    }
    public override void OnStateUpdate()
    {
        base.OnStateUpdate();

        Debug.Log(player.rigidBody.velocity.y + "ÀÇ °ª");


        if (!isAbilitydone)
        {
            Turn(turnSpeed);

        }

    }

    public override void OnStateFixedUpdate()
    {
        base.OnStateFixedUpdate();

        Move(moveSpeed);
    }


    private void Jump()
    {
        if(CheckGround() && player.isJump)
        {
            player.rigidBody.AddForce(Vector3.up * jumpPower * 0.2f, ForceMode.VelocityChange);
        }
    }
}
