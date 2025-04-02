using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletColliding : M_MonoBehaviour
{
    [SerializeField] private Collider _colli;
    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadCollider();
    }
    protected override void Reset()
    {
        base.Reset();
        _colli.isTrigger = true;
    }
    private void LoadCollider()
    {
        if (_colli != null) return;
        this._colli = this.GetComponent<Collider>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag(CONSTANT.Tag_Target))
        {
            Destroy(this.gameObject);
            IGetHit target = collision.GetComponent<IGetHit>();
            if (target != null)
            {
                target.GetHit();
            }
        }
    }
}
