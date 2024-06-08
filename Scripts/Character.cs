using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;

public abstract class BaseState
{
    protected Character _character;

    protected BaseState(Character character)
    {
        _character = character;
    }

    public abstract void OnStateEnter();
    public abstract void OnStateUpdate();
    public abstract void OnStateExit();
}
