using System.Collections;
using System.Collections.Generic;
using UnityEditor.XR;
using UnityEngine;

public delegate void ShootMoment();
public delegate void EndAttack();

[RequireComponent(typeof(Animator))]
public class CharacterView : MonoBehaviour
{
    //Delegates
    public ShootMoment shootMoment;
    public EndAttack endAttack;

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

    private void Start()
    {
        _character.startAttack += StartAttack;
    }

    private void Update()
    {
        _direction = _character.direction;
        _attackDirection = _character.attackDirection;
        _dirX = _direction.x;
        _dirY = _direction.y;
        _attackX = _attackDirection.x;
        _attackY = _attackDirection.y;
        _isMoving = _direction != Vector2.zero;
        //_isAttacking = _attackDirection != Vector2.zero;
        _animator.SetFloat(_animatorParameterDirX, _dirX);
        _animator.SetFloat(_animatorParameterDirY, _dirY);
        _animator.SetBool(_animatorParameterIsMoving, _isMoving);
        _animator.SetFloat(_animatorParameterAttackDirX, _attackX);
        _animator.SetFloat(_animatorParameterAttackDirY, _attackY);
        _animator.SetBool(_animatorParameterIsAttacking, _isAttacking);
    }

    private void AttackingMoment()
    {
        shootMoment?.Invoke();
    }

    private void StartAttack()
    {
        _isAttacking = true;
    }

    private void EndAttack()
    {
        Debug.Log($"{name} EndAttack animator Event called");
        _isAttacking = false;
        endAttack?.Invoke();
    }
}