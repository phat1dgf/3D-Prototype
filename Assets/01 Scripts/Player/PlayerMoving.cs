using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoving : M_MonoBehaviour
{
    [SerializeField] private Rigidbody _rigid;
    [SerializeField] private Vector3 _movement;
    [SerializeField] private float _speed, _jumpHeight;
    [SerializeField] private bool _isJumping;
    [SerializeField] private LayerMask _layerMask;

    protected override void Reset()
    {
        base.Reset();
        this.transform.localPosition = new Vector3(0,-1,0);
        _speed = 10;
        _jumpHeight = 10;
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
        JumpingTracking();
        Moving();
        Jumping();
    }
    private void Moving()
    {
        _movement = Vector3.zero;
        _movement.x = Input.GetAxisRaw("Horizontal");
        _movement.z = Input.GetAxisRaw("Vertical");

        Vector3 moveDirection = _movement.normalized * _speed;
        moveDirection.y = this._rigid.velocity.y; 

        this._rigid.velocity = moveDirection;
    }

    private void Jumping()
    {
        if(Input.GetKeyDown(KeyCode.Space) && !_isJumping)
        {
            this._rigid.AddForce(Vector3.up * _jumpHeight, ForceMode.Impulse);
        }
    }
    private void JumpingTracking()
    {
        bool isHitting = Physics.CheckSphere(this.transform.position, 0.3f, _layerMask);
        _isJumping = !isHitting;
    }

}
