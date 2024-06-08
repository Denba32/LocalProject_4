using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public Define.GameState state;

    public event Action OnInitialize;
    public event Action OnPlay;
    public event Action OnPause;


    protected override void Awake()
    {
        base.Awake();
        Initialize();

    }
    private void Start()
    {
        Application.targetFrameRate = 120;
        Play();
    }
    public void Initialize()
    {
        ActiveCursor(false);

        OnInitialize?.Invoke(); 
    }
    public void Play()
    {
        ActiveCursor(false);
        OnPlay?.Invoke();
    }

    public void Pause()
    {
        ActiveCursor(true);

        OnPause?.Invoke();
    }

    private void ActiveCursor(bool active)
    {
        Cursor.lockState = active ? CursorLockMode.None : CursorLockMode.Locked;
        Cursor.visible = active ? true : false;
    }
}
