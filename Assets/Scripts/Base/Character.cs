using System.Security.Cryptography;
using UnityEngine;

public abstract class Character : MonoBehaviour
{
    public Define.EntityType entityType;
    public FiniteStateMachine finiteStateMachine { get; private set; }

    public Rigidbody rigidBody;

    protected Animator animator;

    protected int hashIdle = Animator.StringToHash("isIdle");
    protected int hashMove = Animator.StringToHash("isMove");


    protected virtual void Awake()
    {
        animator = GetComponentInChildren<Animator>();

        if (finiteStateMachine == null)
            finiteStateMachine = new FiniteStateMachine();

        rigidBody = GetComponent<Rigidbody>();

    }

    protected virtual void Start() { }

    protected virtual void Update()
    {
        finiteStateMachine.CurrentState.OnStateUpdate();
    }

    protected virtual void FixedUpdate()
    {
        finiteStateMachine.CurrentState.OnStateFixedUpdate();
    }

}