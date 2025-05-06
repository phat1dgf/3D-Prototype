using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public class PlayerAiming : M_MonoBehaviour
{
    [SerializeField] private PlayerLook _playerLook;
    [SerializeField] private PlayerController _playerController;
    [SerializeField] private Vector3 hitPoint;
    [SerializeField] private WeaponController _weaponController;
    public Vector3 HitPoint => hitPoint;
    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadPlayerController();
        LoadPlayerLook();
        LoadWeaponController();
    }

    private void LoadWeaponController()
    {
        if (_weaponController != null) return;
        _weaponController = _playerController.WeaponController;
    }

    private void LoadPlayerController()
    {
        if (_playerController != null) return;
        _playerController = this.GetComponentInParent<PlayerController>();
    }

    private void LoadPlayerLook()
    {
        if (_playerLook != null) return;
        _playerLook = _playerController.PlayerLook;
    }

    private void Update()
    {
        Aiming();
    }
    private void Aiming()
    {
        RaycastHit hit;
        bool ray = Physics.Raycast(_playerLook.transform.position, _playerLook.transform.forward, out hit, Mathf.Infinity);
        Debug.DrawRay(_playerLook.transform.position, _playerLook.transform.forward * 10f, Color.magenta);
        
        if (ray)
        {
            GameObject aimedTarget = hit.collider.transform.parent.gameObject;
            if (!aimedTarget.CompareTag(CONSTANT.Tag_Target)) return;
            hitPoint = hit.point;
            if (Input.GetMouseButtonDown(0))
            {
                Shooting(aimedTarget, hit);
            }
        }
    }
    private void Shooting(GameObject aimedTarget, RaycastHit hit)
    {
        MeshRenderer renderer = aimedTarget.GetComponentInChildren<MeshRenderer>();
        if (renderer.material.color != _weaponController.CurrentColor) return;
        IGetHit objGetHit = hit.collider.GetComponent<IGetHit>();
        if (objGetHit != null)
        {
            objGetHit.GetHit();
        }
    }
}
