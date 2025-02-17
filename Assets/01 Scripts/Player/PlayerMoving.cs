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
    private Vector3 _jumpDirection;


    protected override void Reset()
    {
        base.Reset();
        this.transform.localPosition = new Vector3(0,-1,0);
        _walkSpeed = 10;
        _speed = _walkSpeed;
        _runSpeed = 18;
        _jumpHeight = 4;
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
        Vector3 moveDirection = (_movement.x * transform.right + _movement.z * transform.forward).normalized;
        if (_isJumping)
        {
            if (moveDirection != Vector3.zero && Vector3.Dot(moveDirection, _jumpDirection) < 0.95f)
            {
                moveDirection = _jumpDirection;
            }
            else if (moveDirection == Vector3.zero)
            {
                moveDirection = _jumpDirection;
            }
        }
        
         moveDirection.y = _rigid.velocity.y;
         _rigid.MovePosition(_rigid.position + moveDirection * _speed * Time.deltaTime);
        
    }


    private void Jumping()
    {
        if(Input.GetKeyDown(KeyCode.Space) && !_isJumping)
        {
            _isJumping = true;
            _jumpDirection = (_movement.x * transform.right + _movement.z * transform.forward).normalized;
            _rigid.AddForce(Vector3.up * _jumpHeight, ForceMode.Impulse);
        }
    }
    private void JumpTracking()
    {
        bool isHitting = Physics.CheckSphere(this.transform.position, 0.3f, _layerMask);
        _isJumping = !isHitting;
        Debug.Log(_isJumping);
        Debug.Log(isHitting);
    }

}
