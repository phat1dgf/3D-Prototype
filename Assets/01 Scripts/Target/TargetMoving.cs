using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetMoving : M_MonoBehaviour
{
    [SerializeField] private TargetController _targetController;
    [SerializeField] private Rigidbody _rigid;
    [SerializeField] private Vector3 _movement;
    [SerializeField] private float _speed;
    [SerializeField] private GameObject targetDestiny, spawnedPos;
    private Vector3 moveDir;
    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadTargetController();
        LoadRigidbody();
    }
    private void OnEnable()
    {
        targetDestiny = GameObject.Find("TargetDestiny");
        spawnedPos = GameObject.Find("TargetSpawn");
        moveDir = targetDestiny.transform.position - spawnedPos.transform.position;
    }
    private void LoadTargetController()
    {
        if (_targetController != null) return;
        _targetController = GetComponentInParent<TargetController>();
    }

    private void LoadRigidbody()
    {
        if (_rigid != null) return;
        _rigid = GetComponentInParent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        this.Moving();
    }

    private void Moving()
    {
        

        _movement = moveDir.normalized;
        _rigid.velocity = _movement * _speed;
    }
}
