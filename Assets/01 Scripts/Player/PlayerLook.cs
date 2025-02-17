using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLook : M_MonoBehaviour
{
    [SerializeField] private float _mouseX, _mouseY, _sensitivity, _xRot;
    [SerializeField] private PlayerController _playerController;

    protected override void Reset()
    {
        base.Reset();
        _sensitivity = 10;
    }
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadPlayerController();
    }
    private void Start()
    {
        Cursor.visible = false;
    }
    private void LoadPlayerController()
    {
        this._playerController = GetComponentInParent<PlayerController>();
    }

    private void Update()
    {
        Looking();
    }
    private void Looking()
    {
        _mouseX = Input.GetAxis("Mouse X") * _sensitivity;
        _mouseY = Input.GetAxis("Mouse Y") * _sensitivity;

        _playerController.transform.Rotate(0, _mouseX, 0);

        _xRot -= _mouseY;
        _xRot = Mathf.Clamp(_xRot, -80, 80);
        this.transform.localRotation = Quaternion.Euler(_xRot, 0, 0);
    }
}
