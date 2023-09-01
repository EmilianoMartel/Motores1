using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Character : MonoBehaviour
{
    //Life
    [SerializeField] protected int p_life;

    //Movement
    [SerializeField] protected float p_speed;
    [SerializeField] protected Camera p_mainCamera;
    public Vector3 p_direction;
    protected float p_upperLimit;
    protected float p_lowerLimit;
    protected float p_leftLimit;
    protected float p_rightLimit;
    float yPos;

    //Shoot
    [SerializeField] protected GameObject p_pointShoot;
    [SerializeField] protected float p_shootTimeRest;
    [SerializeField] protected BulletManager p_bulletManager;
    private Vector3 _bulletDirection;

    protected float p_actualTime;
    protected static float timeEndGame = 2f;

    private void Awake()
    {
        if (p_mainCamera != null)
        {
            CameraLimit();
        }
    }
    
    private void Update()
    {
        p_actualTime += Time.deltaTime;
    }

    protected void Move(Vector2 direction)
    {
        Vector3 newPosition = transform.position + (Vector3)(direction * p_speed * Time.deltaTime);
        newPosition.x = Mathf.Clamp(newPosition.x, p_leftLimit, p_rightLimit);
        newPosition.y = Mathf.Clamp(newPosition.y, p_lowerLimit, p_upperLimit);
        transform.position = newPosition;
    }

    public virtual void Damage(int damage)
    {
        p_life -= damage;
        if (p_life <= 0)
        {
            gameObject.SetActive(false);
        }
    }

    protected virtual void CharacterShoot()
    {
        _bulletDirection = p_pointShoot.transform.position - transform.position;
        p_bulletManager.Shoot(_bulletDirection);
    }

    protected virtual void Kill()
    {
        gameObject.SetActive(false);
    }

    protected void CameraLimit()
    {
        float height = p_mainCamera.orthographicSize;
        float width = height * p_mainCamera.aspect;
        p_upperLimit = p_mainCamera.transform.position.y + height;
        p_lowerLimit = p_mainCamera.transform.position.y - height;
        p_leftLimit = p_mainCamera.transform.position.x - width;
        p_rightLimit = p_mainCamera.transform.position.x + width;
    }
}
