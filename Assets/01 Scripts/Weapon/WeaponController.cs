using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : M_MonoBehaviour
{
    private Color red = CONSTANT.Red;
    private Color yellow = CONSTANT.Yellow;
    private Color blue = CONSTANT.Blue;

    private Color orange = CONSTANT.Orange;
    private Color purple = CONSTANT.Purple;
    private Color green = CONSTANT.Green;

    private Color? firstColor = null;
    private Color? secondColor = null;

    [SerializeField] private bool _isMixing = false;
    public bool IsMixing => _isMixing;

    [SerializeField] private Color _currentColor;
    public Color CurrentColor => _currentColor;

    protected override void LoadComponents()
    {
        base.LoadComponents();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            Publisher.Notify(CONSTANT.Action_ChooseColor, KeyCode.V,_isMixing);
            if (_isMixing)
            {
                Invoke(nameof(CancelMixMode),0.1f);
            }
            else
            {
                StartMixMode();
            }
            return;
        }

        if (Input.GetKeyDown(KeyCode.Z))
        {
            HandleColorInput(red);
            Publisher.Notify(CONSTANT.Action_ChooseColor, KeyCode.Z);
        }
        else if (Input.GetKeyDown(KeyCode.X))
        {
            HandleColorInput(yellow);
            Publisher.Notify(CONSTANT.Action_ChooseColor, KeyCode.X);
        }
        else if (Input.GetKeyDown(KeyCode.C))
        {
            HandleColorInput(blue);
            Publisher.Notify(CONSTANT.Action_ChooseColor, KeyCode.C);
        }
    }

    void HandleColorInput(Color color)
    {
        _currentColor = color;

        if (_isMixing)
        {
            if (firstColor == null)
            {
                firstColor = color;
            }
            else if (secondColor == null)
            {
                secondColor = color;

                Color mixed = MixColors(firstColor.Value, secondColor.Value);
                _currentColor = mixed;

                Invoke(nameof(CancelMixMode), 0.1f);
            }
        }
        Publisher.Notify(CONSTANT.Action_MixColorUpdated, firstColor, secondColor);
    }

    void StartMixMode()
    {
        _isMixing = true;
        firstColor = null;
        secondColor = null;
    }

    void CancelMixMode()
    {
        _isMixing = false;
        firstColor = null;
        secondColor = null;
        Publisher.Notify(CONSTANT.Action_CancelMixMode);
    }

    Color MixColors(Color a, Color b)
    {
        if ((a == red && b == yellow) || (a == yellow && b == red))
            return orange;
        if ((a == red && b == blue) || (a == blue && b == red))
            return purple;
        if ((a == yellow && b == blue) || (a == blue && b == yellow))
            return green;

        return a; 
    }
}
