using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimPlayer : MonoBehaviour
{
    private Animator _anime;
    private SpriteRenderer _spriteRender;
    private bool _isRolling, _isWalking, _isRunning = false;
    private bool _leftSide, _rightSide;
    
    // Start is called before the first frame update
    void Start()
    {
        _anime = GetComponent<Animator>();
        _spriteRender = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        _isWalking = Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0;
        _isRunning = Input.GetKey(KeyCode.LeftShift) && _isWalking;
        _isRolling = Input.GetMouseButton(1);

        _anime.SetBool("isWalking", _isWalking);
        _anime.SetBool("isRunning", _isRunning);
        _anime.SetBool("isRolling", _isRolling);

        // flipping the animation
        FlipInput();
    }

    void FlipInput()
    {
        _leftSide = Input.GetAxis("Horizontal") < 0;
        _rightSide = Input.GetAxis("Horizontal") > 0;
        if (_isWalking)
            if (_leftSide)
                _spriteRender.flipX = true;
            else if(_rightSide)
                _spriteRender.flipX = false;
    }
}
