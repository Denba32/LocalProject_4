using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHitState : PlayerAbilityState
{
    public PlayerHitState(PlayerController character, PlayerData playerData, Animator animator, int hashParameter) : base(character, playerData, animator, hashParameter)
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
    public override void OnStateUpdate()
    {
        base.OnStateUpdate();
    }

    public override void OnStateFixedUpdate()
    {
        base.OnStateFixedUpdate();
    }

}