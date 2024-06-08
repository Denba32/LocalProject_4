using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyData : ScriptableObject
{
    [Header("Enemy Status")]
    public string displayName;
    public string description;

    public GameObject enemyPrefab;

    [Header("Enemy Patrol Status")]
    public float patrolRadius;
    public float patrolFov;

    [Header("Enemy Chase Status")]
    public float moveSpeed;
    

    [Header("Enemy Attack Status")]
    public float maxHp;

    public float atk;
    public float attackSpeed;

    public float def;

}
