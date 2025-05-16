using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class UIManager : M_MonoBehaviour
{
    [SerializeField] private GameObject _subMenu;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadSubMenu();
    }

    private void LoadSubMenu()
    {
        if (_subMenu != null) return;
        _subMenu = GameObject.Find(CONSTANT.NameObject_SubMenu);
    }
    private void Start()
    {
        _subMenu.SetActive(false);
    }
    private void Update()
    {
        OpenSubMenu();
    }
    private void OpenSubMenu()
    {
        GameManager gameManager = GameManager.Instance;
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (_subMenu.activeSelf)
            {
                gameManager.Playing();
                _subMenu.SetActive(false);
            }
            else
            {
                gameManager.Pause();
                _subMenu.SetActive(true);
            }
        }
    }
}
