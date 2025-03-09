using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAiming : M_MonoBehaviour
{
    [SerializeField] private PlayerLook _playerLook;
    [SerializeField] private PlayerController _playerController;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadPlayerController();
        LoadPlayerLook();
    }

    private void LoadPlayerController()
    {
        if (_playerController != null) return;
        _playerController = this.GetComponentInParent<PlayerController>();
    }

    private void LoadPlayerLook()
    {
        if (_playerLook != null) return;
        _playerLook = this._playerController.PlayerLook;
    }

    private void FixedUpdate()
    {
        RaycastHit hit;
        bool ray = Physics.Raycast(_playerLook.transform.position,_playerLook.transform.forward, out hit, Mathf.Infinity);
        Debug.DrawRay(_playerLook.transform.position, _playerLook.transform.forward * 10f, Color.magenta);
    }
    
}
