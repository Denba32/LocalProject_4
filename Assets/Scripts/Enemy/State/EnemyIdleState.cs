using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIdleState : EnemyState
{
    public EnemyIdleState(EnemyController character, EnemyData enemyData, Animator animator, int hashParameter) : base(character, enemyData, animator, hashParameter)
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
    }

    public override void OnStateUpdate()
    {
        base.OnStateUpdate();
    }
}
