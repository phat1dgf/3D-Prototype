using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetColliding : M_MonoBehaviour,IGetHit
{
    [SerializeField] Collider _targetColli;
    protected override void Reset()
    {
        base.Reset();
        _targetColli.isTrigger = true;
    }
    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadCollider();
    }

    private void LoadCollider()
    {
        if (_targetColli != null) return;
        _targetColli = this.GetComponent<Collider>();
    }

    public void GetHit()
    {
        this.transform.parent.gameObject.SetActive(false);
        Publisher.Notify(CONSTANT.Action_ShootTarget);
    }
}
