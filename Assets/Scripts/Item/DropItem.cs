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

        // ĳ���Ϳ��� �ڽ��� �����͸� �Ѱ��� (�̰� ���� ���� ����ϱ�?)
        CharacterManager.Instance.Player.itemData = data;
        // Invoke�� ���ؼ� OnInteract�� �ش��ϴ� ���� ����
        CharacterManager.Instance.Player.addItem?.Invoke();
        // �������� �Ծ��⿡, �ش� ������ �ı�
        Destroy(gameObject);
    }
}