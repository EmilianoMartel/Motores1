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
    protected int p_randomY = 0;
    protected int p_randomX = 0;

    private void Start()
    {
        if (p_characterView == null)
        {
            Debug.LogError(message: $"{name}: CharacterView is null\n Check and assigned one\nDisabling component");
            enabled = false;
            return;
        }
    }

    public void Shoot(int numShoot)
    {
        List<Vector2> list = new List<Vector2>();
        for (int i = 0; i < numShoot; i++)
        {
            p_directionShoot = GetDirection();
            list.Add(p_directionShoot);
        }
        startMultipleAttack?.Invoke(list);
    }

    public void Shoot()
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
