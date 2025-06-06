using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(LineRenderer))]
public class VRAimingAndShooting : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform rayOrigin;
    [SerializeField] private WeaponController weaponController;

    [Header("Input")]
    [SerializeField] private InputActionReference triggerAction;

    [Header("Settings")]
    [SerializeField] private float rayDistance = 100f;

    private LineRenderer lineRenderer;
    private Vector3 hitPoint;
    public Vector3 HitPoint => hitPoint;

    private void OnEnable() => triggerAction.action.Enable();
    private void OnDisable() => triggerAction.action.Disable();

    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = 2;
        lineRenderer.startWidth = 0.01f;
        lineRenderer.endWidth = 0.005f;
        lineRenderer.material = new Material(Shader.Find("Unlit/Color"));
        lineRenderer.material.color = Color.red;
    }

    private void Update()
    {
        Aiming();
    }

    private void Aiming()
    {
        RaycastHit hit;
        Vector3 origin = rayOrigin.position;
        Vector3 direction = rayOrigin.forward;

        Vector3 endPoint = origin + direction * rayDistance;

        if (Physics.Raycast(origin, direction, out hit, rayDistance))
        {
            endPoint = hit.point;
            GameObject aimedTarget = hit.collider.transform.root.gameObject;

            if (aimedTarget.CompareTag(CONSTANT.Tag_Target) && triggerAction.action.WasPressedThisFrame())
            {
                Shooting(aimedTarget, hit);
            }
        }

        hitPoint = endPoint;
        lineRenderer.SetPosition(0, origin);
        lineRenderer.SetPosition(1, endPoint);
    }

    private void Shooting(GameObject aimedTarget, RaycastHit hit)
    {
        MeshRenderer renderer = aimedTarget.GetComponentInChildren<MeshRenderer>();
        if (renderer == null) return;

        if (renderer.material.color != weaponController.CurrentColor) return;

        IGetHit objGetHit = hit.collider.GetComponent<IGetHit>();
        objGetHit?.GetHit();
    }
}
