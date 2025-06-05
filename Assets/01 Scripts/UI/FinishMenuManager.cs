using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FinishMenuManager : M_MonoBehaviour
{
    [SerializeField] private Button _replay, _mainMenu;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadButtons();
    }

    private void LoadButtons()
    {
        if (_replay != null) return;
        _replay = transform.Find(CONSTANT.NameObject_Replay).GetComponent<Button>();
        if (_mainMenu != null) return;
        _mainMenu = transform.Find(CONSTANT.NameObject_MainMenu).GetComponent<Button>();
    }

    private void Start()
    {
        _replay.onClick.AddListener(() =>
        {
            this.Replay();
        });
        _mainMenu.onClick.AddListener(() =>
        {
            GameManager.Instance.MoveToMainMenu();
        });
    }
    private void Replay()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;
        if (currentSceneName == CONSTANT.SceneName_Lv1)
        {
            GameManager.Instance.MoveToLv1(GameManager.Instance.EasyMode);
            return;
        }
        if (currentSceneName == CONSTANT.SceneName_Lv2)
        {
            GameManager.Instance.MoveToLv2(GameManager.Instance.MediumMode);
            return;
        }
        if (currentSceneName == CONSTANT.SceneName_Lv3)
        {
            GameManager.Instance.MoveToLv3(GameManager.Instance.HardMode);
            return;
        }
    }
}
