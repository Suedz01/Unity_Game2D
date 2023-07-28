using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;

public class MovPlayer : MonoBehaviour
{
    private Animator _animator;
    private Rigidbody2D _rig;
    private Vector2 _direction;
    private float _walkSpeed = 2f, _runSpeed = 4f, _rollSpeed = 6f;
    private bool _isRolling, _isRunning, _isWalking;
    public float _speed;
    
    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
        _rig = GetComponent<Rigidbody2D>();
    }

    // Bom para captar Inputs
    void Update()
    {
        AnimatorStateInfo stateInfo = _animator.GetCurrentAnimatorStateInfo(0);
        bool isSpecificAnimationRunning = stateInfo.IsName("Roll") && stateInfo.normalizedTime < 1;

        _direction = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")).normalized;
        _isRolling = _animator.GetBool("isRolling") || isSpecificAnimationRunning;
        _isRunning = Input.GetKey(KeyCode.LeftShift);
        _isWalking = Input.GetAxis("Horizontal")!=0 && Input.GetAxis("Vertical")!=0;
        
        //Working with speed stuffs
        _speed = _isRunning ? _runSpeed : _walkSpeed;
        if (_isRolling)
            _speed = _rollSpeed;
    }
    
    // Bom para aplicar fisica
    void FixedUpdate()
    {
        //Updating the position
        _rig.MovePosition( _rig.position + ( _direction * _speed * Time.deltaTime ));
    }
}
