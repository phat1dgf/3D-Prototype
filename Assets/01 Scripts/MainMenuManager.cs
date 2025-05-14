using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuManager : M_MonoBehaviour
{
    private static MainMenuManager _instance;
    public static MainMenuManager Instance => _instance;
    protected override void Awake()
    {
        base.Awake();
        if(_instance == null)
        {
            _instance = this;
            return;
        }
        if(_instance.gameObject.GetInstanceID() != this.gameObject.GetInstanceID())
        {
            Destroy(gameObject);
        }
    }
}
