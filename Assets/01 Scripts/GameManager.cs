using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : M_MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager Instance => _instance;

    [SerializeField] private PlayerController _player;
    public PlayerController Player => _player;


    protected override void Awake()
    {
        base.Awake();
        if( _instance == null )
        {
            _instance = this;
            return;
        }
        if( _instance.gameObject.GetInstanceID() != this.gameObject.GetInstanceID() )
        {
            Destroy(this.gameObject); 
        }
        Init();
    }
    protected override void Reset()
    {
        base.Reset();
        Init();
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadPlayerController();
    }
    private void LoadPlayerController()
    {
        if (_player != null) return;
        _player = FindAnyObjectByType<PlayerController>();
    }
    private void Init()
    {
        
    }
    
}
