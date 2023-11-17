using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class CharacterView : MonoBehaviour
{
    [SerializeField] protected Animator p_animator;
    [SerializeField] protected Character p_character;
    [SerializeField] protected SpriteRenderer p_spriteRenderer;

    protected Vector2 p_direction;
    protected Vector2 p_attackDirection;
    protected float p_dirX;
    protected float p_dirY;
    protected float p_attackX;
    protected float p_attackY;
    protected bool p_isMoving;
    protected bool p_isAttacking;
    protected bool p_isDead;
    protected bool p_isDamaged;

    [SerializeField] private float _timeBetweenColors = 0.3f;
    //TODO: TP2 - Spelling error/Code in spanish/Code in spanglish (DONE)
    [SerializeField] private int _numberOfRepetitionAnimation = 2;
    [SerializeField] private Color _damagedColor;
    [SerializeField] private Color _baseColor;

    //Parameters
    [SerializeField] protected string p_animatorParameterDirX = "dir_x";
    [SerializeField] protected string p_animatorParameterDirY = "dir_y";
    [SerializeField] protected string p_animatorParameterIsMoving = "isMoving";
    [SerializeField] protected string p_animatorParameterAttackDirX = "attack_dir_x";
    [SerializeField] protected string p_animatorParameterAttackDirY = "attack_dir_y";
    [SerializeField] protected string p_animatorParameterIsAttacking = "isAttacking";
    [SerializeField] protected string p_animatorParameterIsDeath = "isDeath";

    private void Reset()
    {
        p_animator = GetComponent<Animator>();
    }

    //TODO: TP2 - Should be done in OnEnable
    private void OnEnable()
    {
        p_character.isAttackingEvent += IsAttacking;
        p_character.isDeadEvent += IsDead;
    }

    private void OnDisable()
    {
        p_character.isAttackingEvent -= IsAttacking;
        p_character.isDeadEvent -= IsDead;
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
        p_animator.SetFloat(p_animatorParameterDirX, p_dirX);
        p_animator.SetFloat(p_animatorParameterDirY, p_dirY);
        p_animator.SetBool(p_animatorParameterIsMoving, p_isMoving);
        p_animator.SetFloat(p_animatorParameterAttackDirX, p_attackX);
        p_animator.SetFloat(p_animatorParameterAttackDirY, p_attackY);
    }

    protected void IsAttacking(bool isAttacking)
    {
        p_isAttacking = isAttacking;
        p_animator.SetBool(p_animatorParameterIsAttacking, p_isAttacking);
    }

    protected void IsDead(bool isDead)
    {
        p_isDead = isDead;
        p_animator.SetBool(p_animatorParameterIsDeath, p_isDead);
    }

    public void IsDamaged()
    {
        StartCoroutine(Damaged());
    }
    
    private IEnumerator Damaged()
    {
        for (int i = 0; i < _numberOfRepetitionAnimation; i++)
        {
            p_spriteRenderer.color = _damagedColor;
            yield return new WaitForSeconds(_timeBetweenColors);
            p_spriteRenderer.color = _baseColor;
            yield return new WaitForSeconds(_timeBetweenColors);
        }
    }
}