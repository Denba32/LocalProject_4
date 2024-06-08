using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackState : PlayerAbilityState
{
    private int attackCount = 0;
    public PlayerAttackState(PlayerController character, PlayerData playerData, Animator animator, int hashParameter) : base(character, playerData, animator, hashParameter)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    // 첫 회 공격
    public override void OnStateEnter()
    {
        base.OnStateEnter();
    }

    // 공격 정보 초기화
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
