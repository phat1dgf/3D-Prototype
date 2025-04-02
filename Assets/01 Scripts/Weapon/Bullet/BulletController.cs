using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class BulletController : M_MonoBehaviour
{
    [SerializeField] private Rigidbody _rigid;
    
    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadRigidbody();
    }

    protected override void Reset()
    {
        base.Reset();
        _rigid.useGravity = false;
        _rigid.freezeRotation = true;
    }
    private void LoadRigidbody()
    {
        if (_rigid != null) return;
        this._rigid = this.GetComponent<Rigidbody>();
    }
}
