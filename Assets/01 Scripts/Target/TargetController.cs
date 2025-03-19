using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetController : M_MonoBehaviour
{
    [SerializeField] private float despawnTime;
    Coroutine despawnTarget;
    protected override void Reset()
    {
        base.Reset();
        despawnTime = 8;
    }
    
    private void Start()
    {
        despawnTarget = StartCoroutine(DespawnTarget());
    }
    IEnumerator DespawnTarget()
    {
        yield return new WaitForSeconds(despawnTime);
        this.gameObject.SetActive(false);
    }
    private void OnEnable()
    {
        despawnTarget = StartCoroutine(DespawnTarget());
    }
    private void OnDisable()
    {
        if (despawnTarget == null) return;
        StopCoroutine(despawnTarget);
        despawnTarget = null;
    }
}
