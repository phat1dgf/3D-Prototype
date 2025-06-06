using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreManager : M_MonoBehaviour
{
    private static ScoreManager _instance;
    public static ScoreManager Instance => _instance;

    [SerializeField] private TextMeshProUGUI _scoreText;
    [SerializeField] private int _scoreToWin;
    public int ScoreToWin => _scoreToWin;
    [SerializeField] private int _currentScore;
    public int CurrentScore => _currentScore;
    [SerializeField] private bool _isFinished;
    public bool IsFinished => _isFinished;

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
            Destroy(this.gameObject);    
        }
    }

    protected override void Reset()
    {
        base.Reset();
        _scoreToWin = 15;
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadScoreText();
    }

    private void LoadScoreText()
    {
        _scoreText = this.GetComponentInChildren<TextMeshProUGUI>();
    }
    private void Start()
    {
        _currentScore = 0;
        _isFinished = false;
        GameManager.Instance.Playing();
    }
    private void Update()
    {
        if (_currentScore >= _scoreToWin) 
        {
            Finish();
        }
    }

    private void Finish()
    {
        _isFinished = true;
        GameManager.Instance.Pause();
    }

    private void OnEnable()
    {
        Publisher.AddListeners(CONSTANT.Action_ShootTarget, UpdateScore);
    }
    private void OnDisable()
    {
        Publisher.RemoveListeners(CONSTANT.Action_ShootTarget, UpdateScore);
    }
    private void UpdateScore(object[] obj)
    {
        _currentScore += 1;
        _scoreText.text = "Score: " + _currentScore.ToString();
    }

}
