using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Condition : MonoBehaviour
{
    public Define.ConditionType type;

    public float curValue;
    public float startValue;
    public float maxValue;
    public float passiveValue;
    public Image uiBar;

    private void Start()
    {
        Init();
        curValue = startValue;
    }

    private void Update()
    {
        uiBar.fillAmount = GetPercentage();
    }

    private void Init()
    {
        switch(type)
        {
            case Define.ConditionType.Hp:
                maxValue = CharacterManager.Instance.Player.controller.playerData.maxHP;
                startValue = CharacterManager.Instance.Player.controller.playerData.maxHP;
                break;
            case Define.ConditionType.Stamina:
                maxValue = CharacterManager.Instance.Player.controller.playerData.maxStamina;
                startValue = CharacterManager.Instance.Player.controller.playerData.maxStamina;

                break;
        }
    }
    private float GetPercentage()
    {
        return curValue / maxValue;
    }

    public void Add(float value)
    {
        curValue = Mathf.Min(curValue + value, maxValue);
    }

    public void Subtract(float value)
    {
        curValue = Mathf.Max(curValue - value, 0); 
    }
}
