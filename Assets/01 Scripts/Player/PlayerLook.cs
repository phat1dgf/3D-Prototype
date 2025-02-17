using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerLook : M_MonoBehaviour
{
    [Header("--------------Looking-------------")]

    [SerializeField] private float _mouseX;
    [SerializeField] private float _mouseY;
    [SerializeField] private float _sensitivity;
    [SerializeField] private float _xRot;

    [Space(3)]
    [Header("--------------Cinemachine Camera-------------")]

    [SerializeField] private CinemachineVirtualCamera _firstLookCam;
    [SerializeField] private CinemachineVirtualCamera _thirdLookCam;
    [SerializeField] private CinemachineVirtualCamera _currentCam;
    [SerializeField] private List<CinemachineVirtualCamera> _listCam = new();

    [Space(3)]
    [Header("--------------Components-------------")]

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
        this.LoadListCam();
        this.LoadFirstCam();
        this.LoadThirdCam();
    }

    private void LoadThirdCam()
    {
        if (_thirdLookCam != null) return;
        _thirdLookCam = transform.Find(CONSTANT.ThirdCamName).GetComponent<CinemachineVirtualCamera>();
    }

    private void LoadFirstCam()
    {
        if (_firstLookCam != null) return;
        _firstLookCam = transform.Find(CONSTANT.FirstCamName).GetComponent<CinemachineVirtualCamera>();
    }

    private void LoadListCam()
    {
        _listCam.Clear();
        foreach (CinemachineVirtualCamera cam in this.GetComponentsInChildren<CinemachineVirtualCamera>())
        {        
            _listCam.Add(cam);
        }
    }

    private void Start()
    {
        SetStartCam(_thirdLookCam);      
    }

    private void SetStartCam(CinemachineVirtualCamera startCam)
    {
        _currentCam = startCam;
        foreach (CinemachineVirtualCamera cam in _listCam)
        {
            if (cam == startCam)
                cam.Priority = 20;
            else 
                cam.Priority = 10;
        }
    }
    private void SwitchCam()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            if (_listCam.Count == 0) return; 

            for (int i = 0; i < _listCam.Count; i++)
            {
                _listCam[i].Priority = 10; 

                if (_listCam[i] == _currentCam)
                {
                    
                    _currentCam = (i == _listCam.Count - 1) ? _listCam[0] : _listCam[i + 1];
                    _currentCam.Priority = 20;
                    break; 
                }
            }
        }
    }

    private void LoadPlayerController()
    {
        this._playerController = GetComponentInParent<PlayerController>();
    }

    private void Update()
    {
        Looking();
        SwitchCam();
    }
    private void Looking()
    {
        _mouseX = Input.GetAxis("Mouse X") * _sensitivity;
        _mouseY = Input.GetAxis("Mouse Y") * _sensitivity;

        _playerController.transform.Rotate(0, _mouseX, 0);

        _xRot -= _mouseY;
        _xRot = Mathf.Clamp(_xRot, -80, 80);
        _playerController.transform.localRotation = Quaternion.Euler(_xRot, 0, 0);
    }
}
