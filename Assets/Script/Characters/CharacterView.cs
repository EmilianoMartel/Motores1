using System.Collections;
using System.Collections.Generic;
using UnityEditor.XR;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class CharacterView : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private Character _character;
    private Vector2 _direction;
    private Vector2 _attackDirection;
    private float _dirX;
    private float _dirY;
    private float _attackX;
    private float _attackY;
    private bool _isMoving;
    private bool _isAttacking;

    //Parameters
    [SerializeField] private string _animatorParameterDirX = "dir_x";
    [SerializeField] private string _animatorParameterDirY = "dir_y";
    [SerializeField] private string _animatorParameterIsMoving = "isMoving";
    [SerializeField] private string _animatorParameterAttackDirX = "attack_dir_x";
    [SerializeField] private string _animatorParameterAttackDirY = "attack_dir_y";
    [SerializeField] private string _animatorParameterIsAttacking = "isAttacking";

    private void Reset()
    {
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        _direction = _character.GetDirection();
        _attackDirection = _character.ShootDirection();
        _dirX = _direction.x;
        _dirY = _direction.y;
        _attackX = _attackDirection.x;
        _attackY = _attackDirection.y;
        _isMoving = _direction != Vector2.zero;
        _isAttacking = _attackDirection != Vector2.zero;
        _animator.SetFloat(_animatorParameterDirX, _dirX);
        _animator.SetFloat(_animatorParameterDirY, _dirY);
        _animator.SetBool(_animatorParameterIsMoving, _isMoving);
        _animator.SetFloat(_animatorParameterAttackDirX, _attackX);
        _animator.SetFloat(_animatorParameterAttackDirY, _attackY);
        _animator.SetBool(_animatorParameterIsAttacking, _isAttacking);
    }
}
