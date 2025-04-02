using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletFactory : M_MonoBehaviour
{
    private static BulletFactory _instance;
    public static BulletFactory Instance => _instance;
    [SerializeField] private GameObject _bulletDefault;
    [SerializeField] private Dictionary<Bullet, GameObject> _bulletMap = new();
    [SerializeField] private Dictionary<GameObject, List<GameObject>> _pool = new();


    protected override void Awake()
    {
        base.Awake();
        if (_instance == null)
        {
            _instance = this;
        }
        if (_instance.gameObject.GetInstanceID() != this.gameObject.GetInstanceID())
        {
            Destroy(this.gameObject);
        }
        _bulletMap[Bullet.bulletDefault] = _bulletDefault;
    }
    public GameObject CreateTarget(Bullet type, Vector3 position, Quaternion rotation)
    {
        GameObject prefab = _bulletMap[type];

        if (!_pool.ContainsKey(prefab))
        {
            _pool[prefab] = new List<GameObject>();
        }

        foreach (GameObject bullet in _pool[prefab])
        {
            if (bullet.activeSelf) continue;
            bullet.transform.position = position;
            bullet.transform.rotation = rotation;
            bullet.SetActive(true);
            return bullet;
        }

        GameObject newBullet = Instantiate(prefab, position, rotation);
        newBullet.transform.parent = this.transform;
        _pool[prefab].Add(newBullet);
        return newBullet;

    }

    public enum Bullet
    {
        bulletDefault
    }
}
