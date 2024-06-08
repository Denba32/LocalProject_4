
using UnityEngine;

public class PlayerMoveState : PlayerGroundState
{
    private float moveSpeed;

    private float turnSpeed = 20f;


    public PlayerMoveState(PlayerController character, PlayerData playerData, Animator animator, int hashParameter) : base(character, playerData, animator, hashParameter)
    {
        moveSpeed = playerData.moveSpeed;
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void OnStateEnter()
    {
        base.OnStateEnter();

        Debug.Log("Move ป๓ลย");

    }

    public override void OnStateExit()
    {
        base.OnStateExit();
    }
    public override void OnStateUpdate()
    {
        base.OnStateUpdate();

        #region Animation Veloctiy

        #endregion

        if(!isExitingState)
        {
            Turn(turnSpeed);

            if (player.isRun)
                CharacterManager.Instance.Player.condition.UseStamina(1f);

            // Move -> Idle
            if (Vector2.Distance(player.curMovementInput, Vector2.zero) < 0.01f)
            {
                finiteStateMachine.ChangeState(player.IdleState);
            }
            //if(player.isRun)
            //{
            //    finiteStateMachine.ChangeState(player.SprintState);
            //}


        }   
    }

    public override void OnStateFixedUpdate()
    {
        base.OnStateFixedUpdate();

        Move(moveSpeed);
    }
}
