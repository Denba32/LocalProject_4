using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


/// <summary>
/// 플레이어를 컨트롤하는 상태
/// </summary>
public class PlayerState : BaseState
{
    protected PlayerController player;
    protected PlayerData playerData;
    protected bool isExitingState;

    private Camera cam = Camera.main;

    protected FiniteStateMachine finiteStateMachine;

    private Vector2 moveControl = Vector2.zero;
    private Vector3 dir;

    private float fallSpeed = 0.05f;
    private float yVelocity = 0f;
    private float limitedYVelocity = 3f;

    protected readonly int hashXVelocity = Animator.StringToHash("xVelocity");
    protected readonly int hashYVelocity = Animator.StringToHash("yVelocity");


    private bool canRun = false;
    public PlayerState(PlayerController character, PlayerData playerData, Animator animator, int hashParameter) : base(character, animator, hashParameter)
    {
        player = character;
        this.playerData = playerData;

        finiteStateMachine = player.finiteStateMachine;
    }

    public override void DoChecks()
    {

    }

    public override void OnStateEnter()
    {
        base.OnStateEnter();
        isExitingState = false;
    }

    public override void OnStateExit()
    {
        base.OnStateExit();

        isExitingState = true;
    }

    public override void OnStateUpdate()
    {
        if (!isExitingState)
        {
            if(player.isRun)
            {
                canRun = CharacterManager.Instance.Player.condition.UseStamina(0.2f);
            }
            else
            {
                canRun = false;
            }
            
            moveControl = canRun ? Vector2.MoveTowards(moveControl, player.curMovementInput * 2.0f, 100f * Time.deltaTime) : Vector2.MoveTowards(moveControl, player.curMovementInput, 10.0f * Time.deltaTime);
            animator.SetFloat(hashXVelocity, moveControl.magnitude);
        }
    }

    public override void OnStateFixedUpdate()
    {
    }

    #region 플레이어 필수 로직

    protected bool CheckGround()
    {
        Ray[] rays = rays = new Ray[4]{
            new Ray(player.transform.position + (player.transform.forward * 0.1f) + (player.transform.up * 0.1f), Vector3.down),
            new Ray(player.transform.position + (-player.transform.forward * 0.1f) + (player.transform.up * 0.1f), Vector3.down),
            new Ray(player.transform.position + (player.transform.right * 0.1f) + (player.transform.up * 0.1f), Vector3.down),
            new Ray(player.transform.position + (-player.transform.right * 0.1f) +  (player.transform.up * 0.1f), Vector3.down),
        };
        for (int i = 0; i < rays.Length; i++)
        {
            Debug.DrawRay(rays[i].origin, rays[i].direction * 0.2f, Color.red);

            if (Physics.Raycast(rays[i], 0.2f, playerData.groundLayerMask))
            {
                return true;
            }
        }
        return false;
    }

    protected void Move(float moveSpeed)
    {
        Vector3 forward = cam.transform.forward;
        forward.y = 0;
        forward = forward.normalized;


        Vector3 right = cam.transform.right;
        right.y = 0;
        right = right.normalized;

        // 방향이 요구됨
        dir = forward * player.curMovementInput.y + right * player.curMovementInput.x;

        if(canRun)
        {
            dir *= moveSpeed * 3f;
        }
        else
        {
            dir *= moveSpeed;
        }

        dir.y = player.rigidBody.velocity.y;

        player.rigidBody.velocity = dir;
    }
    protected void Sprint(float moveSpeed)
    {
        Vector3 forward = cam.transform.forward;
        forward.y = 0;
        forward = forward.normalized;

        Vector3 right = cam.transform.right;
        right.y = 0;
        right = right.normalized;

        dir = forward * 1 + right * player.curMovementInput.x;

        if (player.isSprint)
        {
            dir *= moveSpeed * 3f;
        }
        else
        {
            dir *= moveSpeed;
        }

        dir.y = player.rigidBody.velocity.y;

        player.rigidBody.velocity = dir;
    }


    protected void Turn(float turnSpeed)
    {
        Vector3 horizontalDir = new Vector3(dir.x, 0, dir.z).normalized;

        Quaternion targetRotation = Quaternion.LookRotation(horizontalDir);

        Quaternion currentYRotation = Quaternion.Euler(0, animator.transform.eulerAngles.y, 0);

        // Slerp를 사용하여 부드럽게 회전합니다.
        animator.transform.rotation = Quaternion.Slerp(currentYRotation, targetRotation, turnSpeed * Time.fixedDeltaTime);
    }

    #endregion
}