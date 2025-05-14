using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class TargetSpawner : M_MonoBehaviour
{
    [SerializeField] private float spawnDelay;
    Coroutine targetSpawningCoroutine;
    
    IEnumerator TargetSpawningCoroutine()
    {
        WaitForSeconds waitSpawnDelay = new WaitForSeconds(spawnDelay);
        while (true)
        {
            yield return waitSpawnDelay;
            GameObject spawnedTarget = TargetFactory.Instance.CreateTarget(TargetFactory.Target.targetDefault,this.transform.position,Quaternion.identity);
            spawnedTarget.GetComponentInChildren<MeshRenderer>().material.color = RandomColor();
        }
    }

    private void Start()
    {
        spawnDelay = GameManager.Instance.TargetSpawnDelay;
    }

    private Color RandomColor()
    {
        int index = UnityEngine.Random.Range(0, 6); 

        switch (index)
        {
            case 0: return CONSTANT.Red;
            case 1: return CONSTANT.Yellow;
            case 2: return CONSTANT.Blue;
            case 3: return CONSTANT.Orange;
            case 4: return CONSTANT.Purple;
            case 5: return CONSTANT.Green;
            default: return CONSTANT.Red; 
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
