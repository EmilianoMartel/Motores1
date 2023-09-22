using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void Death();
public class HealthPoints : MonoBehaviour
{
    public Death death;

    [SerializeField] private int _originalLife = 10;
    private int _life;

    private void Start()
    {
        _life = _originalLife;
    }

    public void GetDamage(int damage)
    {
        _life -= damage;
        if(_life <= 0)
        {
            Death();
        }
    }

    private void Death()
    {
        death?.Invoke();
        _life = _originalLife;
    }
}
