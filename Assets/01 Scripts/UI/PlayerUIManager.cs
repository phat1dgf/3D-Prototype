using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUIManager : M_MonoBehaviour
{
    [SerializeField] private GameObject _colorMix, _mixColor1, _mixColor2;
    [SerializeField] private RectTransform _red, _yellow, _blue, _mix;
    [SerializeField] private Sprite _redSprite, _yellowSprite, _blueSprite, _graySprite, _greenSprite, _purpleSprite, _orangeSprite;
    [SerializeField] private Image _currentColor;
    [SerializeField] private WeaponController _weaponController;

    private Vector3 scaleSize = new Vector3(1.25f, 1.25f, 1.25f);

    private void Start()
    {
        _colorMix.SetActive(false);
        Publisher.AddListeners(CONSTANT.Action_ChooseColor,ChooseColor);
        Publisher.AddListeners(CONSTANT.Action_MixColorUpdated, OnMixColorUpdated);
        Publisher.AddListeners(CONSTANT.Action_CancelMixMode, CancelMixMode);
        LoadWeapon();
    }

    private void OnDestroy()
    {
        Publisher.RemoveListeners(CONSTANT.Action_ChooseColor, ChooseColor);
        Publisher.RemoveListeners(CONSTANT.Action_MixColorUpdated, OnMixColorUpdated);
        Publisher.RemoveListeners(CONSTANT.Action_CancelMixMode, CancelMixMode);
    }

    private void Update()
    {
        UpdateCurrentColorUI(); 
    }

    private void UpdateCurrentColorUI()
    {
        Color curColor = _weaponController.CurrentColor;

        if (curColor == CONSTANT.Red)
            _currentColor.sprite = _redSprite;
        else if (curColor == CONSTANT.Green)
            _currentColor.sprite = _greenSprite;
        else if (curColor == CONSTANT.Blue)
            _currentColor.sprite = _blueSprite;
        else if (curColor == CONSTANT.Purple)
            _currentColor.sprite = _purpleSprite;
        else if (curColor == CONSTANT.Orange)
            _currentColor.sprite = _orangeSprite;
        else if (curColor == CONSTANT.Yellow)
            _currentColor.sprite = _yellowSprite;
        else _currentColor.sprite = _graySprite;
    }

    private void CancelMixMode(object[] obj)
    {
        _colorMix.SetActive(false);
    }

    private void OnMixColorUpdated(object[] datas)
    {
        Color? color1 = datas[0] as Color?;
        Color? color2 = datas[1] as Color?;

        UpdateMixColorSprite(_mixColor1, color1);
        UpdateMixColorSprite(_mixColor2, color2);
    }

    private void UpdateMixColorSprite(GameObject mixColorUI, Color? color)
    {
        Image img = mixColorUI.GetComponent<Image>();

        if (!color.HasValue)
        {
            img.sprite = _graySprite;
            return;
        }

        if (color == CONSTANT.Red)
            img.sprite = _redSprite;
        else if (color == CONSTANT.Blue)
            img.sprite = _blueSprite;
        else if (color == CONSTANT.Yellow)
            img.sprite = _yellowSprite;
        else
            img.sprite = _graySprite; 
    }



    private void ChooseColor(object[] datas)
    {
        if (datas.Length == 0 || datas[0].GetType() != typeof(KeyCode)) return;

        KeyCode key = (KeyCode)datas[0];
        bool? isMixing = datas.Length > 1 ? (bool?)datas[1] : null;

        HandleWeaponUI(key, isMixing);
    }


    private void HandleWeaponUI(KeyCode key, bool? isMixing)
    {
        switch(key)
        {
            case KeyCode.Z:
                ScaleWeaponUI(_red);
                break;
            case KeyCode.X:
                ScaleWeaponUI(_yellow);
                break;
            case KeyCode.C: 
                ScaleWeaponUI(_blue);
                break;
            case KeyCode.V:
                MixColorUI(isMixing);
                break;
        }
    }

    private void MixColorUI(bool? isMixing)
    {
        if(isMixing == null)
        {
            Debug.Log("isMixing null");
            return;
        }
        if (isMixing == true)
        {
            _colorMix.SetActive(false);
        }
        else
        {
            _colorMix.SetActive(true);
        }
    }

    private void ScaleWeaponUI(RectTransform weaponUI)
    {
        List<RectTransform> weaponUIs = new List<RectTransform>();
        weaponUIs.Add(_red);
        weaponUIs.Add(_yellow);
        weaponUIs.Add(_blue);
        foreach (RectTransform weapon in weaponUIs)
        {
            weapon.localScale = Vector3.one;
            if(weapon == weaponUI)
            {
                weapon.localScale = scaleSize;
            }
        }
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadColorMix();
        LoadColors();
        LoadCurrentColor();
    }

    private void LoadWeapon()
    {
        if (_weaponController != null) return;
        _weaponController = FindAnyObjectByType<WeaponController>();
    }

    private void LoadCurrentColor()
    {
        if (_currentColor != null) return;
        _currentColor = GameObject.Find(CONSTANT.NameObject_CurrentColor).GetComponent<Image>();
    }

    private void LoadColorMix()
    {
        if (_colorMix != null) return;
        _colorMix = GameObject.Find(CONSTANT.NameObject_ColorMix);
    }

    private void LoadColors()
    {
        if (_red != null) return;
        _red = GameObject.Find(CONSTANT.NameObject_Red).GetComponent<RectTransform>();
        if (_blue != null) return;
        _blue = GameObject.Find(CONSTANT.NameObject_Blue).GetComponent<RectTransform>();
        if (_yellow != null) return;
        _yellow = GameObject.Find(CONSTANT.NameObject_Yellow).GetComponent<RectTransform>();
        if (_mix != null) return;
        _mix = GameObject.Find(CONSTANT.NameObject_Mix).GetComponent<RectTransform>();
        if (_mixColor1 != null) return;
        _mixColor1 = GameObject.Find(CONSTANT.NameObject_MixColor1);
        if (_mixColor2 != null) return;
        _mixColor2 = GameObject.Find(CONSTANT.NameObject_MixColor2);
    }
}
