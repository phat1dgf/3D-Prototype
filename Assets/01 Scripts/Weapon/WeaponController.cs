using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : M_MonoBehaviour
{
    [SerializeField] private MeshRenderer _meshRenderer;

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
        LoadMeshRenderer();
    }

    private void LoadMeshRenderer()
    {
        if (_meshRenderer != null) return;
        _meshRenderer = this.GetComponentInChildren<MeshRenderer>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            if (_isMixing)
            {
                CancelMixMode();
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
        }
        else if (Input.GetKeyDown(KeyCode.X))
        {
            HandleColorInput(yellow);
        }
        else if (Input.GetKeyDown(KeyCode.C))
        {
            HandleColorInput(blue);
        }
    }

    void HandleColorInput(Color color)
    {
        _meshRenderer.material.color = color;
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
                _meshRenderer.material.color = mixed;
                _currentColor = mixed;

                CancelMixMode(); 
            }
        }
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
