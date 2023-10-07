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

    [SerializeField] protected Animator p_animator;
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
        p_animator = GetComponent<Animator>();
    }

    private void Start()
    {
        if (_character == null)
        {
            Debug.LogError(message: $"{name}: Character is null\n Check and assigned one\nDisabling component");
            enabled = false;
            return;
        }
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
        p_animator.SetFloat(_animatorParameterDirX, _dirX);
        p_animator.SetFloat(_animatorParameterDirY, _dirY);
        p_animator.SetBool(_animatorParameterIsMoving, _isMoving);
        p_animator.SetFloat(_animatorParameterAttackDirX, _attackX);
        p_animator.SetFloat(_animatorParameterAttackDirY, _attackY);
    }

    private void AttackingMoment()
    {
        shootMoment?.Invoke();
    }

    private void StartAttack()
    {
        _isAttacking = true;
        p_animator.SetBool(_animatorParameterIsAttacking, _isAttacking);
    }

    private void EndAttack()
    {
        Debug.Log($"{name} EndAttack animator Event called");
        _isAttacking = false;
        p_animator.SetBool(_animatorParameterIsAttacking, _isAttacking);
        endAttack?.Invoke();
    }
}