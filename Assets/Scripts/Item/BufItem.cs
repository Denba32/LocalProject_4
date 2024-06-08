using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 먹으면 버프가 곧장 적용
public class BufItem : ItemObject
{
    private float value = 2f;
    private float time = 3f;
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            PlayerController player = CharacterManager.Instance.Player.controller;
            switch(data.bufType)
            {
                case BufType.Speed:
                    player.StartSprint(value, time);
                    break;
            }
            Destroy(gameObject);
            // TODO : 이동 속도 상승
        }
    }
}