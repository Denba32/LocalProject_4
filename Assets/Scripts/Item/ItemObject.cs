using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractable
{
    public string GetInteractPrompt();
    public void OnInteract();
}

public class ItemObject : MonoBehaviour, IInteractable
{
    public ItemData data;
    public virtual string GetInteractPrompt()
    {
        if (data == null)
        {
            Debug.Log("�ش� �����ۿ� �����Ͱ� �������� �ʽ��ϴ�.");
            return "";
        }

        string str = $"{data.displayName}\n{data.description}";
        return str;
    }

    public virtual void OnInteract()
    {

    }
}