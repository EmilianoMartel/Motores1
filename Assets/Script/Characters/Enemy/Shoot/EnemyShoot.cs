using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public delegate void StartAttack(Vector2 directionShoot);
public delegate void StartMultipleAttack(List<Vector2> listDirection);
public abstract class EnemyShoot : MonoBehaviour
{
    //Delegates
    public StartAttack startAttack;
    public StartMultipleAttack startMultipleAttack;

    [SerializeField] protected CharacterView p_characterView;
    protected Vector2 p_directionShoot;
    protected int p_directionY = 0;
    protected int p_directionX = 0;

    protected virtual void Awake()
    {
        if (!p_characterView)
        {
            Debug.LogError(message: $"{name}: CharacterView is null\n Check and assigned one\nDisabling component");
            enabled = false;
            return;
        }
    }

    public virtual void Shoot(int numShoot)
    {
        List<Vector2> list = new List<Vector2>();
        for (int i = 0; i < numShoot; i++)
        {
            p_directionShoot = GetDirection();
            list.Add(p_directionShoot);
        }
        startMultipleAttack?.Invoke(list);
    }

    public virtual void Shoot()
    {
        p_directionShoot = GetDirection();
        startAttack?.Invoke(p_directionShoot);
    }

    protected virtual Vector2 GetDirection()
    {
        Vector2 direction = new Vector2(0,0);
        return direction;
    }
}
