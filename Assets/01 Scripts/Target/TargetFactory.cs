using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetFactory : M_MonoBehaviour
{
    private static TargetFactory _instance;
    public static TargetFactory Instance => _instance;

    [SerializeField] private GameObject _targetDefault;
    [SerializeField] private Dictionary<Target, GameObject> _targetMap = new();
    [SerializeField] private Dictionary<GameObject, List<GameObject>> _pool = new();


    protected override void Awake()
    {
        base.Awake();
        if(_instance == null)
        {
            _instance = this;
        }
        if(_instance.gameObject.GetInstanceID() != this.gameObject.GetInstanceID())
        {
            Destroy(this.gameObject);
        }
        _targetMap[Target.targetDefault] = _targetDefault;
    }
    public GameObject CreateTarget(Target type, Vector3 position, Quaternion rotation)
    {
        GameObject prefab = _targetMap[type];

        if (!_pool.ContainsKey(prefab))
        {
            _pool[prefab] = new List<GameObject>(); 
        }

        foreach (GameObject target in _pool[prefab])
        {
            if (target.activeSelf) continue;
            target.transform.position = position;
            target.transform.rotation = rotation;
            target.SetActive(true);
            return target;
        }

        GameObject newTarget = Instantiate(prefab,position,rotation);
        newTarget.transform.parent = this.transform;
        _pool[prefab].Add(newTarget);
        return newTarget;

    }

    public enum Target
    {
        targetDefault
    }
}
