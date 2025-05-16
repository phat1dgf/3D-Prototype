using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SubMenuManager : M_MonoBehaviour
{
    [SerializeField] private Button _resume, _mainMenu;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadButtons();
    }

    private void LoadButtons()
    {
        if (_resume != null) return;
        _resume = GameObject.Find(CONSTANT.NameObject_Resume).GetComponent<Button>();
        if (_mainMenu != null) return;
        _mainMenu = GameObject.Find(CONSTANT.NameObject_MainMenu).GetComponent<Button>();
    }

    private void Start()
    {
        _resume.onClick.AddListener(() =>
        {
            this.Resume();
        });
        _mainMenu.onClick.AddListener(() =>
        {
            GameManager.Instance.MoveToMainMenu();
        });
    }
    private void Resume()
    {
        GameManager.Instance.Playing();
        this.transform.gameObject.SetActive(false);
    }
}
