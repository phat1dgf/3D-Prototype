using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRPlayerController : MonoBehaviour
{
    [SerializeField] private GameObject spawnPos;

    private void Start()
    {
        spawnPos = GameObject.Find("SpawnPos");
        this.transform.position = spawnPos.transform.position;
    }
}
