using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class VRPlayerMovement : MonoBehaviour
{
    [Header("Input")]
    public InputActionReference moveAction; 

    [Header("Movement Settings")]
    public float moveSpeed = 2f;
    public Transform headTransform; 

    private Rigidbody rb;

    private void Awake()    
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnEnable() => moveAction.action.Enable();
    private void OnDisable() => moveAction.action.Disable();

    private void FixedUpdate()
    {
        Vector2 input = moveAction.action.ReadValue<Vector2>();
        Vector3 forward = new Vector3(headTransform.forward.x, 0, headTransform.forward.z).normalized;
        Vector3 right = new Vector3(headTransform.right.x, 0, headTransform.right.z).normalized;
        Vector3 direction = forward * input.y + right * input.x;

        Vector3 velocity = direction * moveSpeed;
        rb.velocity = new Vector3(velocity.x, rb.velocity.y, velocity.z);
    }
}
