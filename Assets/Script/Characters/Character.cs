using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public delegate void StartAttack();
public abstract class Character : MonoBehaviour
{
    //Delegates
    public StartAttack startAttack;

    [SerializeField] protected HealthPoints p_healthPoints;
    [SerializeField] protected CharacterView p_characterView;

    //Movement
    [SerializeField] protected float p_speed = 1.0f;
    protected Vector3 p_direction;
    protected Vector3 p_attackDirection;

    //Shoot
    [SerializeField] protected float p_shootTimeRest;
    [SerializeField] protected bool p_isAttacking = false;

    protected float p_actualTime = 0;
    protected static float timeEndGame = 2f;

    public Vector3 attackDirection { get { return p_attackDirection; } }
    public Vector3 direction { get { return p_direction; } }

    private void Start()
    {
        NullReferenceController();
        SuscriptionsDelegates();
    }

    protected void NullReferenceController()
    {
        if (p_healthPoints == null)
        {
            Debug.LogError(message: $"{name}: HealthPoints is null\n Check and assigned one\nDisabling component");
            enabled = false;
            return;
        }
        if (p_characterView == null)
        {
            Debug.LogError(message: $"{name}: CharacterView is null\n Check and assigned one\nDisabling component");
            enabled = false;
            return;
        }
    }

    protected void SuscriptionsDelegates()
    {
        p_healthPoints.death += Kill;
    }

    protected void Movement(Vector2 direction)
    {
        transform.Translate(direction * p_speed * Time.deltaTime);
    }

    protected virtual void Kill()
    {
        gameObject.SetActive(false);
    }

    protected virtual void StartAttack()
    {
        p_isAttacking = true;
        startAttack?.Invoke();
    }

    protected virtual void EndAttack()
    {
        p_isAttacking = false;
    }
}
