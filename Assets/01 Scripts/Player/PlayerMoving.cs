using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoving : M_MonoBehaviour
{
    [SerializeField] private Rigidbody _rigid;
    [SerializeField] private Vector3 _movement;
    [SerializeField] private float _speed, _runSpeed, _walkSpeed, _jumpHeight;
    [SerializeField] private bool _isJumping;
    [SerializeField] private LayerMask _layerMask;

    protected override void Reset()
    {
        base.Reset();
        this.transform.localPosition = new Vector3(0,-1,0);
        _walkSpeed = 10;
        _speed = _walkSpeed;
        _runSpeed = 18;
        _jumpHeight = 10;
        _layerMask = LayerMask.GetMask("Ground");
    }
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadRigidbody();
    }
    private void LoadRigidbody()
    {
        if (this._rigid != null) return;
        this._rigid = this.GetComponentInParent<Rigidbody>();
    }
    
    private void Update()
    {
        JumpTracking();
        Moving();
        Jumping();
        Running();
    }

    private void Running()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift)) 
        { 
            this._speed = _runSpeed;
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            this._speed = _walkSpeed;
        }
    }

    private void Moving()
    {
        _movement = Vector3.zero;
        _movement.x = Input.GetAxisRaw("Horizontal");
        _movement.z = Input.GetAxisRaw("Vertical");

        if (!_isJumping)
        {
            Vector3 moveDirection = _movement.normalized * _speed;
            moveDirection.y = this._rigid.velocity.y; 

            this._rigid.velocity = moveDirection;
        }
        else
        {
            Vector3 currentHorizontalVelocity = new Vector3(this._rigid.velocity.x, 0, this._rigid.velocity.z);
            Vector3 newDirection = _movement.normalized * _speed;

            if (_movement.magnitude > 0 && Vector3.Dot(currentHorizontalVelocity.normalized, newDirection.normalized) < 0.5f)
            {
                this._rigid.velocity = new Vector3(0, this._rigid.velocity.y, 0);
            }
        }
    }

    private void Jumping()
    {
        if(Input.GetKeyDown(KeyCode.Space) && !_isJumping)
        {
            this._rigid.AddForce(Vector3.up * _jumpHeight, ForceMode.Impulse);
        }
    }
    private void JumpTracking()
    {
        bool isHitting = Physics.CheckSphere(this.transform.position, 0.3f, _layerMask);
        _isJumping = !isHitting;
    }

}
