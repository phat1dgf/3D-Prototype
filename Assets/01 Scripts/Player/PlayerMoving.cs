using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerMoving : M_MonoBehaviour
{
    [Header("--------------Walk and Run-------------")]
    [SerializeField] private Vector3 _movement;
    [SerializeField] private float _speed, _runSpeed, _walkSpeed;    
    [SerializeField] private Vector3 vectorHorizontal, vectorVertical;
    
    [Space(3)]
    [Header("--------------Jump-------------")]
    [SerializeField] private Vector3 _jumpDir;
    [SerializeField] private bool _isJumping;
    [SerializeField] private LayerMask _groundLayerMask;
    [SerializeField] private float _jumpHeight;

    [Space(3)]
    [Header("--------------Components-------------")]
    [SerializeField] private Rigidbody _rigid;
    protected override void Reset()
    {
        base.Reset();
        this.transform.localPosition = new Vector3(0,-1,0);
        _walkSpeed = 10;
        _speed = _walkSpeed;
        _runSpeed = 18;
        _jumpHeight = 4;
        _groundLayerMask = LayerMask.GetMask(CONSTANT.LayerName_Ground);
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
        //Jumping();
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
        vectorHorizontal = Input.GetAxisRaw("Horizontal") * this.transform.parent.right;
        vectorVertical = Input.GetAxisRaw("Vertical") * this.transform.parent.forward;

        if (_isJumping)
        {
            Vector3 moveDir = vectorHorizontal + vectorVertical;
            if (moveDir != Vector3.zero && Vector3.Dot(moveDir, _jumpDir) > 0.5f)
            {
                return;
            }
            else if (moveDir == Vector3.zero)
            {
                return;
            }
            else if (moveDir != Vector3.zero && Vector3.Dot(moveDir, _jumpDir) < 0.5f)
            {
                this._rigid.velocity = new Vector3(0, this._rigid.velocity.y, 0);
                return;
            }
        }

        _movement.x = vectorHorizontal.x + vectorVertical.x;
        _movement.z = vectorHorizontal.z + vectorVertical.z;
        _movement.Normalize();
        this._rigid.velocity = new Vector3(_movement.x , this._rigid.velocity.y / _speed ,_movement.z ) * _speed;
    }

    private void Jumping()
    {
        if(Input.GetKeyDown(KeyCode.Space) && !_isJumping)
        {
            _isJumping = true;
            _jumpDir = (vectorHorizontal + vectorVertical).normalized;
            _rigid.AddForce(Vector3.up * _jumpHeight, ForceMode.Impulse);
        }
    }
    private void JumpTracking()
    {
        bool isHitting = Physics.CheckSphere(this.transform.position, 0.3f, _groundLayerMask);
        _isJumping = !isHitting;
    }

}
