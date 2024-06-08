using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : Character
{
    public EnemyData enemyData;

    public EnemyIdleState IdleState { get; private set; }
    public EnemyPatrolState PatrolState { get; private set; }
    public EnemyChaseState ChaseState { get; private set; }
    public EnemyCombatState CombatState { get; private set; }


    private readonly int hashChase = Animator.StringToHash("isChase");

    protected override void Awake()
    {
        base.Awake();

        IdleState = new EnemyIdleState(this, enemyData, animator, hashIdle);
        PatrolState = new EnemyPatrolState(this, enemyData, animator, hashMove);
        ChaseState = new EnemyChaseState(this, enemyData, animator, hashChase);

        finiteStateMachine.Initialize(IdleState);
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
    }
}