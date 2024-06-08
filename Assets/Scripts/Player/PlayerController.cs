using System;
using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;
using System.Collections;
using UnityEngine.UI;

public class PlayerController : Character
{
    private PlayerInput playerInput;
    public PlayerData playerData;

    private readonly int hashPause = Animator.StringToHash("isPause");
    private readonly int hashJump = Animator.StringToHash("isJump");
    private readonly int hashFall = Animator.StringToHash("isFall");
    private readonly int hashLand = Animator.StringToHash("isLanding");
    private readonly int hashSprint = Animator.StringToHash("isSprint");
    private readonly int hashAttack = Animator.StringToHash("isAttack");


    #region State

    public PlayerIdleState IdleState { get; private set; }
    public PlayerMoveState MoveState { get; private set; }
    public PlayerSprintState SprintState { get; private set; }
    public PlayerInAirState AirState { get; private set; }
    public PlayerJumpState JumpState { get; private set; }
    public PlayerLandState LandState { get; private set; }
    public PlayerAttackState AttackState { get; private set; }
    public PlayerPauseState PauseState { get; private set; }


    #endregion

    public GameObject phone;

    [Header("Move")]
    // 달리기
    public bool isRun = false;
    // 이동
    public Vector2 curMovementInput;
    private Coroutine co_StaminaOver;

    [Header("Jump")]
    public bool isJump = false;

    [Header("Look")]
    public CinemachineVirtualCamera playCamera;
    public CinemachineVirtualCamera pauseCamera;

    public Transform cameraContainer;
    public float limitedXLook;
    public float zoomPower;
    private float camCurXRot;
    private float camCurYRot;
    public float lookSensitivity;
    private Vector2 mouseDelta;

    public bool isPause = false;
    public bool isSprint = false;

    public Action inventory;

    private bool isStaminaOver = false;
    protected override void Awake()
    {
        base.Awake();
        playerInput = GetComponent<PlayerInput>();
        
        IdleState = new PlayerIdleState(this, playerData, animator, hashIdle);
        MoveState = new PlayerMoveState(this, playerData, animator, hashMove);
        SprintState = new PlayerSprintState(this, playerData, animator, hashSprint);
        JumpState = new PlayerJumpState(this, playerData, animator, hashJump);
        AirState = new PlayerInAirState(this, playerData, animator, hashFall);
        LandState = new PlayerLandState(this, playerData, animator, hashLand);
        AttackState = new PlayerAttackState(this, playerData, animator, hashAttack);
        PauseState = new PlayerPauseState(this, playerData, animator, hashPause);

        finiteStateMachine.Initialize(IdleState);
    }

    protected override void Start()
    {
        base.Start();
    }

    private void OnEnable()
    {
        AddListener(playerInput.actions["Move"], OnMove);
        AddListener(playerInput.actions["Look"], OnLook);
        AddListener(playerInput.actions["Run"], OnRun);
        AddListener(playerInput.actions["Zoom"], OnZoom);
        AddListener(playerInput.actions["Jump"], OnJump);
        AddListener(playerInput.actions["Menu"], OnMenu);
        AddListener(playerInput.actions["Attack"], OnAttack);

        // AddListener(playerInput.actions["Inventory"], OnInventory);

        #region Subscribe 

        GameManager.Instance.OnPlay += OnStart;
        GameManager.Instance.OnPause += OnPause;

        CharacterManager.Instance.Player.condition.OnStaminaOver += StartStaminaOver;
        #endregion
    }


    private void OnDisable()
    {
        RemoveListener(playerInput.actions["Move"], OnMove);
        RemoveListener(playerInput.actions["Look"], OnLook);
        RemoveListener(playerInput.actions["Run"], OnRun);
        RemoveListener(playerInput.actions["Zoom"], OnZoom);
        RemoveListener(playerInput.actions["Jump"], OnJump);
        RemoveListener(playerInput.actions["Menu"], OnMenu);
        RemoveListener(playerInput.actions["Attack"], OnAttack);

        // RemoveListener(playerInput.actions["Inventory"], OnInventory);

        #region UnSubscribe

        GameManager.Instance.OnPlay -= OnStart;
        GameManager.Instance.OnPause -= OnPause;

        CharacterManager.Instance.Player.condition.OnStaminaOver -= StartStaminaOver;

        #endregion
    }

    protected override void Update()
    {
        base.Update();
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    private void LateUpdate()
    {
        if (!isPause)
        {
            CameraLook();
            CameraZoom();
        }
    }

    void CameraLook()
    {
        // 상하 회전 제한
        camCurXRot += mouseDelta.y * lookSensitivity ;
        camCurXRot = Mathf.Clamp(camCurXRot, -limitedXLook, limitedXLook);

        camCurYRot += mouseDelta.x * lookSensitivity;

        // 카메라의 회전
        cameraContainer.localEulerAngles = new Vector3(-camCurXRot, camCurYRot, 0);
    }

    void CameraZoom()
    {
        if(playCamera != null)
        {
            float zoom = playCamera.m_Lens.FieldOfView;
            zoom -= zoomPower;
            playCamera.m_Lens.FieldOfView = Mathf.Clamp(zoom, 15f, 80f);
        }
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            curMovementInput = context.ReadValue<Vector2>().normalized;
        }
        else if (context.phase == InputActionPhase.Canceled)
        {
            curMovementInput = Vector2.zero;
        }
    }
    private void OnRun(InputAction.CallbackContext context)
    {
        if(!isStaminaOver)
        {
            if (context.phase == InputActionPhase.Started)
            {
                isRun = true;
            }

            else if (context.phase == InputActionPhase.Canceled)
            {
                isRun = false;
            }
        }
        else
        {
            isRun = false;
        }
    }
    public void OnLook(InputAction.CallbackContext callback)
    {
        mouseDelta = callback.ReadValue<Vector2>();
    }
    public void OnZoom(InputAction.CallbackContext callback)
    {
        zoomPower = callback.ReadValue<float>();
    }
    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
        {
            isJump = true;
        }
        else if(context.phase == InputActionPhase.Canceled)
        {
            isJump = false;
        }
    }
    public void OnMenu(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
        {
            isPause = !isPause;

            if(isPause)
            {
                GameManager.Instance.Pause();
            }
            else
            {
                GameManager.Instance.Play();
            }
        }
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
        {

        }
    }
    ////public void OnInventory(InputAction.CallbackContext context)
    ////{
    ////    if (context.phase == InputActionPhase.Started)
    ////    {
    ////        inventory?.Invoke();
    ////        ToggleCursor();
    ////    }
    ////}


    private void AddListener(InputAction action, Action<InputAction.CallbackContext> callback)
    {
        action.started += callback;
        action.performed += callback;
        action.canceled += callback;
    }

    private void RemoveListener(InputAction action, Action<InputAction.CallbackContext> callback)
    {
        action.started -= callback;
        action.performed -= callback;
        action.canceled -= callback;
    }

    private void OnStart()
    {
        phone.SetActive(false);
        isPause = false;
        if(pauseCamera != null && playCamera != null)
        {
            pauseCamera.enabled = false;
            playCamera.enabled = true;
        }

        finiteStateMachine.ChangeState(IdleState);
    }

    private void OnPause()
    {
        isPause = true;

        if (pauseCamera != null && playCamera != null)
        {
            playCamera.enabled = false;

            pauseCamera.enabled = true;
        }

        finiteStateMachine.ChangeState(PauseState);
        phone.SetActive(true);

    }

    #region Coroutine

    public Coroutine StartSprint(float speed, float time)
    {
        Coroutine routine = StartCoroutine(Sprint(speed, time));

        return routine;
    }

    private IEnumerator Sprint(float speed , float time)
    {
        float currentTime = 0;

        finiteStateMachine.ChangeState(SprintState);

        isSprint = true;

        playerData.moveSpeed *= speed;
        while (time > currentTime)
        {
            currentTime += Time.deltaTime;
            yield return null;
        }

        playerData.moveSpeed /= speed;

        isSprint = false;
        yield break;
    }

    public void StopCo(Coroutine coroutine)
    {
        StopCoroutine(coroutine);
    }


    public void StartStaminaOver()
    {
        if(co_StaminaOver != null)
            StopCoroutine(co_StaminaOver);
        co_StaminaOver = StartCoroutine(StaminaOver(3f));
    }
    private IEnumerator StaminaOver(float time)
    {
        isStaminaOver = true;
        isRun = false;
        yield return CoroutineHelper.WaitForSeconds(time);

        isStaminaOver = false;

        yield break;
    }
    #endregion
}