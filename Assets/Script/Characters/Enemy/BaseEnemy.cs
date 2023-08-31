using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void EnemyKill(Enemy enemy);
public class Enemy : Character
{
    public EnemyKill enemyKill;

    //One more timeShoot
    protected float p_timeShoot;

    void Awake()
    {
        p_mainCamera = FindAnyObjectByType<Camera>();
        CameraLimit();
        TimeShootSelection();
    }

    protected void TimeShootSelection()
    {
        p_timeShoot = Random.Range(p_shootTimeRest, 5);
    }

    protected override void Kill()
    {
        enemyKill?.Invoke(this);
        base.Kill();
    }
}
