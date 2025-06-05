using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerLook : M_MonoBehaviour
{
    [Header("Looking Settings")]
    [SerializeField] private float _sensitivity = 10f;
    [SerializeField] private float _xRot = 0f;

    [Header("References")]
    [SerializeField] private Transform _cameraTransform; 
    [SerializeField] private PlayerController _playerController;

    protected override void Reset()
    {
        base.Reset();
        _sensitivity = 10f;
    }

    private void Start()
    {
        _cameraTransform.position = this.transform.position;
        _cameraTransform.parent = this.transform;
        
    }
    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadPlayerController();
        LoadCameraTransform();
    }

    private void LoadPlayerController()
    {
        if (_playerController != null) return;
        _playerController = GetComponentInParent<PlayerController>();
    }

    private void LoadCameraTransform()
    {
        if (_cameraTransform != null) return;
        _cameraTransform = Camera.main.transform;
    }

    private void Update()
    {
        LookAround();
    }

    private void LookAround()
    {
        float mouseX = Input.GetAxis("Mouse X") * _sensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * _sensitivity;

        _playerController.transform.Rotate(Vector3.up * mouseX);

        _xRot -= mouseY;
        _xRot = Mathf.Clamp(_xRot, -80f, 80f);

        this.transform.localRotation = Quaternion.Euler(_xRot, 0f, 0f);
    }
}

