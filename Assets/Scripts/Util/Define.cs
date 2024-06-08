using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Define
{
    public enum EntityType
    {
        Player,
        Enemy
    }

    public enum GameState
    {
        Initialize,
        Play,
        Pause
    }

    public enum ConditionType
    {
        Hp,
        Hunger,
        Stamina,
    }

    public enum SoundType
    {
        Bgm,
        Effect,
        Voice,
        Max
    }
}
