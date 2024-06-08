using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
public class UIManager : Singleton<UIManager>
{
    private readonly int hashPause = Animator.StringToHash("isPaused");
    private readonly int hashInventory = Animator.StringToHash("isInventory");

    public GameObject inventory;
    public GameObject menu;

    public event Action OnInventory;

    public Animator animator;
    
    public TMP_Text promptText;


    private void OnEnable()
    {
        GameManager.Instance.OnPause += OpenMenu;
        GameManager.Instance.OnPlay += CloseMenu;
    }

    private void OnDisable()
    {
        GameManager.Instance.OnPause -= OpenMenu;
        GameManager.Instance.OnPlay -= CloseMenu;
    }


    public void OpenMenu()
    {
        menu.SetActive(true);
        animator.SetBool(hashPause, true);
    }

    public void CloseMenu()
    {
        animator.SetBool(hashPause, false);
    }

    public void OpenInventory()
    {
        inventory.SetActive(true);
        animator.SetBool(hashInventory, true);
    }

    public void CloseInventory()
    {
        animator.SetBool(hashInventory, false);
    }

}
