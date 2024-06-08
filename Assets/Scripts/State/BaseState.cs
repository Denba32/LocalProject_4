
using UnityEngine;

//행동 쓸 때마다 상속
public abstract class BaseState
{
    protected Character character;
    protected Animator animator;
    protected int hashParameter;

    protected bool isAnimationFinish;

    protected BaseState(Character character, Animator animator, int hashParameter)
    {
        this.character = character;
        this.animator = animator;
        this.hashParameter = hashParameter;
    }

    public abstract void DoChecks();
    public virtual void OnStateEnter()
    {
        DoChecks();

        animator.SetBool(hashParameter, true);
    }


    public virtual void OnStateExit()
    {
        animator.SetBool(hashParameter, false);
    }

    public abstract void OnStateUpdate();
    public abstract void OnStateFixedUpdate() ;


}
