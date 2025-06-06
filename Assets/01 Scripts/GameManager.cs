using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : M_MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager Instance => _instance;

    [SerializeField] private float _targetSpawnDelay;
    public float TargetSpawnDelay => _targetSpawnDelay;

    

    private float _easyMode = 1.5f;
    public float EasyMode => _easyMode;
    private float _mediumMode = 1f;
    public float MediumMode => _mediumMode;
    private float _hardMode = 0.5f;
    public float HardMode => _hardMode;

    protected override void Awake()
    {
        base.Awake();
        if( _instance == null )
        {
            _instance = this;
            DontDestroyOnLoad(this);
            return;
        }
        if( _instance.gameObject.GetInstanceID() != this.gameObject.GetInstanceID() )
        {
            Destroy(this.gameObject); 
        }
    }
    protected override void Reset()
    {
        base.Reset();
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
    }
    public void MoveToMainMenu()
    {
        SceneManager.LoadScene(CONSTANT.SceneName_MainMenu);
    }
    public void MoveToLv1(float targetSpawnDelay)
    {
        SceneManager.LoadScene(CONSTANT.SceneName_Lv1);
        SceneManager.LoadScene(CONSTANT.SceneName_Gameplay, LoadSceneMode.Additive);
        _targetSpawnDelay = targetSpawnDelay;
    }
    public void MoveToLv2(float targetSpawnDelay)
    {
        SceneManager.LoadScene(CONSTANT.SceneName_Lv2);
        SceneManager.LoadScene(CONSTANT.SceneName_Gameplay, LoadSceneMode.Additive);
        _targetSpawnDelay = targetSpawnDelay;
    } 
    public void MoveToLv3(float targetSpawnDelay)
    {
        SceneManager.LoadScene(CONSTANT.SceneName_Lv3);
        SceneManager.LoadScene(CONSTANT.SceneName_Gameplay, LoadSceneMode.Additive);
        _targetSpawnDelay = targetSpawnDelay;
    }

    public void ExitGame()
    {
        Application.Quit();
    }
    public void Playing()
    {
        Time.timeScale = 1;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
    public void Pause()
    {
        Time.timeScale = 0;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
   
}
