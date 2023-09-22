using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public abstract class Character : MonoBehaviour
{
    [SerializeField] private HealthPoints healthPoints;

    //Movement
    [SerializeField] protected float p_speed = 1.0f;
    protected Vector3 p_direction;
    protected Vector3 p_attackDirection;

    //Shoot
    [SerializeField] protected float p_shootTimeRest;

    protected float p_actualTime = 0;
    protected static float timeEndGame = 2f;

    private void Start()
    {
        healthPoints.death += Kill;
    }

    protected void Movement(Vector2 direction)
    {
        transform.Translate(direction * p_speed * Time.deltaTime);
    }

    protected virtual void Kill()
    {
        gameObject.SetActive(false);
    }

    public Vector2 GetDirection()
    {
        return p_direction;
    }

    public Vector2 ShootDirection()
    {
        return p_attackDirection;
    }

    public float GetShootColdDown()
    {
        return p_shootTimeRest;
    }
}
