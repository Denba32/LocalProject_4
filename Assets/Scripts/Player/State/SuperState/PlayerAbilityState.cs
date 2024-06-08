using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// 지상, 공중
/// </summary>
public class PlayerAbilityState : PlayerState
{
    protected bool isGround = false;

    protected bool isAbilitydone = false;
    private float weight;

    public PlayerAbilityState(PlayerController character, PlayerData playerData, Animator animator, int hashParameter) : base(character, playerData, animator, hashParameter)
    {
        weight = this.playerData.weight;
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
    public override void OnStateUpdate()
    {
        base.OnStateUpdate();

        isGround = CheckGround();

        if (isAbilitydone)
        {
            if (player.rigidBody.velocity.y < 0.01f && isGround)
            {
                finiteStateMachine.ChangeState(player.IdleState);
            }
            else if(player.rigidBody.velocity.y < 0.01f && !isGround)
            {
                finiteStateMachine.ChangeState(player.AirState);
            }
        }
    }

    public override void OnStateFixedUpdate()
    {
        base.OnStateFixedUpdate();

    }

}
