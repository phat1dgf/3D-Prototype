using System;
using UnityEngine;
using UnityEngine.XR;

public class VRPlayerLook : MonoBehaviour
{
    [SerializeField] private Transform xrCamera; 
    [SerializeField] private Transform playerBody; 

    private void LateUpdate()
    {
        Vector3 lookDirection = xrCamera.forward;
        lookDirection.y = 0;
        playerBody.forward = lookDirection.normalized;
    }
    
}

