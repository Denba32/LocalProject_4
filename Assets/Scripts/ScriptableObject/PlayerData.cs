using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="CharacterData", menuName ="ChracterInfo", order =0)]
public class PlayerData : ScriptableObject
{
    [Header("Player Normal Info")]
    public string characterName;
    public string characterDescription;
    public float maxHP;
    public float maxStamina;
    public float moveSpeed;
    public float jumpPower;
    public float weight;
    public LayerMask groundLayerMask;
    private void OnValidate()
    {
#if UNITY_EDITOR
        characterName = this.name;
        UnityEditor.EditorUtility.SetDirty(this);
#endif
    }
}
