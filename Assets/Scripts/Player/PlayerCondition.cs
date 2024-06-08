using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamagable
{
    void TakePhysicalDamage(int damabe);
}

/// <summary>
/// 플레이어의 스테이터스를 관리
/// </summary>
public class PlayerCondition : MonoBehaviour, IDamagable
{
    public UICondition uiCondition;
    Rigidbody rb;
    Condition health { get { return uiCondition.health; } }
    Condition stamina { get { return uiCondition.stamina; } }

    public event Action OnTakeDamage;
    public event Action OnStaminaOver;

    public float noHungerHealthDecay;
    private void Update()
    {
        stamina.Add(stamina.passiveValue * Time.deltaTime);

        if(health.curValue <= 0)
        {
            Die();
        }
    }

    public void Heal(float amount)
    {
        health.Add(amount);
    }

    private void Die()
    {
        Debug.Log("사망");
    }

    public void TakePhysicalDamage(int damabe)
    {
        health.Subtract(damabe);
        OnTakeDamage?.Invoke();
    }

    public bool UseStamina(float amount)
    {
        if(stamina.curValue - amount < 0f)
        {
            StaminaOver();
            return false;
        }

        stamina.Subtract(amount);
        return true;
    }
    public void StaminaOver()
    {
        OnStaminaOver?.Invoke();
    }
}
