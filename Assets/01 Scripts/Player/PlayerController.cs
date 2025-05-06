using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : M_MonoBehaviour
{
    [SerializeField] private PlayerLook _playerLook;
    public PlayerLook PlayerLook => _playerLook;
    [SerializeField] private WeaponController _weaponController;
    public WeaponController WeaponController => _weaponController;
    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadPlayerLook();
        LoadWeaponController();
    }

    private void LoadWeaponController()
    {
        if (_weaponController != null) return;
        _weaponController = this.GetComponentInChildren<WeaponController>();
    }

    private void LoadPlayerLook()
    {
        if (_playerLook != null) return;
        _playerLook = this.GetComponentInChildren<PlayerLook>();
    }
    
}
