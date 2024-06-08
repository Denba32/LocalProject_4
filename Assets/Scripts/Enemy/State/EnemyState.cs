using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyState : BaseState
{
    protected EnemyController character;

    protected EnemyData enemyData;
    
    public EnemyState(EnemyController character, EnemyData enemyData, Animator animator, int hashParameter) : base(character, animator, hashParameter)
    {
        this.character = character;
        this.enemyData = enemyData;
    }

    public override void DoChecks()
    {
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
        
    }

    public override void OnStateUpdate()
    {

    }
}
