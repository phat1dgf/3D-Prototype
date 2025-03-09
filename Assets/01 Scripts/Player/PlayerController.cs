using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : M_MonoBehaviour
{
    [SerializeField] private PlayerLook _playerLook;
    public PlayerLook PlayerLook => _playerLook;
    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadPlayerLook();
    }

    private void LoadPlayerLook()
    {
        if (_playerLook != null) return;
        _playerLook = this.GetComponentInChildren<PlayerLook>();
    }
}
