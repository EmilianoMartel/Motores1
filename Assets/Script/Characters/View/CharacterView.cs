using System.Collections;
using System.Collections.Generic;
using UnityEditor.XR;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class CharacterView : MonoBehaviour
{
    [SerializeField] protected Animator p_animator;
    [SerializeField] protected Character p_character;
    protected Vector2 p_direction;
    protected Vector2 p_attackDirection;
    protected float p_dirX;
    protected float p_dirY;
    protected float p_attackX;
    protected float p_attackY;
    protected bool p_isMoving;
    protected bool p_isAttacking;

    //Parameters
    [SerializeField] protected string p_animatorParameterDirX = "dir_x";
    [SerializeField] protected string p_animatorParameterDirY = "dir_y";
    [SerializeField] protected string p_animatorParameterIsMoving = "isMoving";
    [SerializeField] protected string p_animatorParameterAttackDirX = "attack_dir_x";
    [SerializeField] protected string p_animatorParameterAttackDirY = "attack_dir_y";
    [SerializeField] protected string p_animatorParameterIsAttacking = "isAttacking";

    private void Reset()
    {
        p_animator = GetComponent<Animator>();
    }

    private void Start()
    {
        if (p_character == null)
        {
            Debug.LogError(message: $"{name}: Character is null\n Check and assigned one\nDisabling component");
            enabled = false;
            return;
        }
    }

    private void Update()
    {
        p_direction = p_character.direction;
        p_attackDirection = p_character.attackDirection;
        p_dirX = p_direction.x;
        p_dirY = p_direction.y;
        p_attackX = p_attackDirection.x;
        p_attackY = p_attackDirection.y;
        p_isMoving = p_direction != Vector2.zero;
        p_isAttacking = p_character.isAttacking;
        p_animator.SetFloat(p_animatorParameterDirX, p_dirX);
        p_animator.SetFloat(p_animatorParameterDirY, p_dirY);
        p_animator.SetBool(p_animatorParameterIsMoving, p_isMoving);
        p_animator.SetFloat(p_animatorParameterAttackDirX, p_attackX);
        p_animator.SetFloat(p_animatorParameterAttackDirY, p_attackY);
        p_animator.SetBool(p_animatorParameterIsAttacking, p_isAttacking);
    }
}