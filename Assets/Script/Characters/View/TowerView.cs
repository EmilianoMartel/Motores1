using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;


public class TowerView : CharacterView
{
    [SerializeField] private BaseEnemy _enemy;

    private bool _canAttack;

    //Parameters
    [SerializeField] private string _animatorParameterCanAttack = "canAttack";

    private void Start()
    {
        if (_enemy == null)
        {
            Debug.LogError(message: $"{name}: Enemy is null\n Check and assigned one\nDisabling component");
            enabled = false;
            return;
        }
        p_character.isAttackingEvent += IsAttacking;
        p_character.isDeadEvent += IsDeath;
    }

    private void Update()
    {
        _canAttack = _enemy.canAttack;
        p_animator.SetBool(_animatorParameterCanAttack, _canAttack);
    }
}
