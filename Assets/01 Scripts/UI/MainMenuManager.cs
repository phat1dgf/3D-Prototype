using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuManager : M_MonoBehaviour
{
    private static MainMenuManager _instance;
    public static MainMenuManager Instance => _instance;

    [SerializeField] private GameObject _mainMenu, _modes;
    [SerializeField] private Button _play, _exit, _easy, _medium, _hard, _back;

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

    private void Start()
    {
        GameManager gameManager = GameManager.Instance;
        _mainMenu.SetActive(true);
        _modes.SetActive(false);
        _play.onClick.AddListener(() =>
        {
            _modes.SetActive(true);
            _mainMenu.SetActive(false);
        });
        _exit.onClick.AddListener(() =>
        {
            gameManager.ExitGame();
        });
        _easy.onClick.AddListener(() =>
        {
            gameManager.MoveToLv1(gameManager.EasyMode);
        });
        _medium.onClick.AddListener(() =>
        {
            gameManager.MoveToLv2(gameManager.MediumMode);
        });
        _hard.onClick.AddListener(() =>
        {
            gameManager.MoveToLv3(gameManager.HardMode);
        });
        _back.onClick.AddListener(() =>
        {
            _mainMenu.SetActive(true);
            _modes.SetActive(false);
        });
    }
    
    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadMainMenu();
        LoadModes();
    }

    private void LoadMainMenu()
    {
        if (_mainMenu != null) return;
        _mainMenu = GameObject.Find(CONSTANT.NameObject_MainMenu);
        if (_play != null) return;
        _play = GameObject.Find(CONSTANT.NameObject_Play).GetComponent<Button>();       
        if (_exit != null) return;
        _exit = GameObject.Find(CONSTANT.NameObject_Exit).GetComponent<Button>();
    }

    private void LoadModes()
    {
        if (_modes != null) return;
        _modes = GameObject.Find(CONSTANT.NameObject_Modes);
        if (_easy != null) return;
        _easy = GameObject.Find(CONSTANT.NameObject_Easy).GetComponent<Button>();
        if (_medium != null) return;
        _medium = GameObject.Find(CONSTANT.NameObject_Medium).GetComponent<Button>();
        if (_hard != null) return;
        _hard = GameObject.Find(CONSTANT.NameObject_Hard).GetComponent<Button>();
        if (_back != null) return;
        _back = GameObject.Find(CONSTANT.NameObject_Back).GetComponent<Button>();
    }
}
