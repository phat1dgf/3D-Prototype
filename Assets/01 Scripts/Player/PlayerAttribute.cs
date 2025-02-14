using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CapsuleCollider))]

public class PlayerAttribute : M_MonoBehaviour
{
    [SerializeField] private Collider _colli;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadCollider();
    }
    private void LoadCollider()
    {
        if (this._colli != null) return;
        this._colli = this.GetComponent<Collider>();
    }
}
