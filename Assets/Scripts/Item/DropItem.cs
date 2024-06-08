using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropItem : ItemObject
{
    private float rotSpeed = 0f;

    private void Update()
    {
        if(gameObject.activeInHierarchy)
        {
            Rotate();
        }
    }
    private void Rotate()
    {
        rotSpeed += 1.0f;
        transform.rotation = Quaternion.Euler(-20f, rotSpeed, 0);
    }

    public override void OnInteract()
    {
        base.OnInteract();

        // 캐릭터에게 자신의 데이터를 넘겨줌 (이게 정말 옳은 방법일까?)
        CharacterManager.Instance.Player.itemData = data;
        // Invoke를 통해서 OnInteract에 해당하는 동작 실행
        CharacterManager.Instance.Player.addItem?.Invoke();
        // 아이템을 먹었기에, 해당 아이템 파괴
        Destroy(gameObject);
    }
}