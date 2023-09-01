using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class CharacterView : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private Character _character;
    private Vector2 _direction;
    private float _dirX;
    private float _dirY;
    private bool _isMoving;

    //Parameters
    [SerializeField] private string _animatorParameterDirX = "dir_x";
    [SerializeField] private string _animatorParameterDirY = "dir_y";
    [SerializeField] private string _animatorParameterIsMoving = "isMoving";

    private void Reset()
    {
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        _direction = _character.p_direction;
        _dirX = _direction.x;
        _dirY = _direction.y;
        _isMoving = _direction != Vector2.zero;
        _animator.SetFloat(_animatorParameterDirX, _dirX);
        _animator.SetFloat(_animatorParameterDirY, _dirY);
        _animator.SetBool(_animatorParameterIsMoving, _isMoving);
    }
}
