using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetSpawner : M_MonoBehaviour
{
    [SerializeField] private float spawnDelay;
    Coroutine targetSpawningCoroutine;
    

    private void Start()
    {
        targetSpawningCoroutine = StartCoroutine(TargetSpawningCoroutine());
    }

    protected override void Reset()
    {
        base.Reset();
        spawnDelay = 1.5f;
    }
    
    IEnumerator TargetSpawningCoroutine()
    {
        WaitForSeconds waitSpawnDelay = new WaitForSeconds(spawnDelay);
        while (true)
        {
            yield return waitSpawnDelay;
            TargetFactory.Instance.CreateTarget(TargetFactory.Target.targetDefault,this.transform.position,Quaternion.identity);
        }
    }
    private void OnDisable()
    {
        if (targetSpawningCoroutine == null) return;
        StopCoroutine(targetSpawningCoroutine);
        targetSpawningCoroutine = null;
    }
    private void OnEnable()
    {
        targetSpawningCoroutine = StartCoroutine(TargetSpawningCoroutine());
    }
    
}
