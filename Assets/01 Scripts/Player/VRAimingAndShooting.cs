using UnityEngine;
using UnityEngine.InputSystem;

public class VRAimingAndShooting : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform rayOrigin; 
    [SerializeField] private WeaponController weaponController;

    [Header("Input")]
    [SerializeField] private InputActionReference triggerAction;

    [Header("Settings")]
    [SerializeField] private float rayDistance = 100f;

    [Header("Debug")]
    [SerializeField] private bool debugRay = true;

    private Vector3 hitPoint;
    public Vector3 HitPoint => hitPoint;

    private void OnEnable() => triggerAction.action.Enable();
    private void OnDisable() => triggerAction.action.Disable();

    private void Update()
    {
        Aiming();
    }

    private void Aiming()
    {
        RaycastHit hit;
        Vector3 origin = rayOrigin.position;
        Vector3 direction = rayOrigin.forward;

        if (debugRay)
            Debug.DrawRay(origin, direction * rayDistance, Color.magenta);

        if (Physics.Raycast(origin, direction, out hit, rayDistance))
        {
            GameObject aimedTarget = hit.collider.transform.root.gameObject;
            if (!aimedTarget.CompareTag(CONSTANT.Tag_Target)) return;

            hitPoint = hit.point;

            if (triggerAction.action.WasPressedThisFrame())
            {
                Shooting(aimedTarget, hit);
            }
        }
    }

    private void Shooting(GameObject aimedTarget, RaycastHit hit)
    {
        MeshRenderer renderer = aimedTarget.GetComponentInChildren<MeshRenderer>();
        if (renderer == null) return;

        if (renderer.material.color != weaponController.CurrentColor) return;

        IGetHit objGetHit = hit.collider.GetComponent<IGetHit>();
        if (objGetHit != null)
        {
            objGetHit.GetHit();
        }
    }
}
