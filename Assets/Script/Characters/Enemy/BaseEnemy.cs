using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public delegate void EnemyKill(BaseEnemy enemy);
public class BaseEnemy : Character
{
    public EnemyKill enemyKill;

    //Managers
    [SerializeField] protected EnemyManager _enemyManager;
    [SerializeField] private ViewMapManager _viewMapManager;

    //Positions
    protected Vector2 p_currentPosition;
    protected Vector2 p_nextPosition;
    protected Vector2 p_directionToNextPosition;
    protected int p_row;
    protected int p_column;
    protected int p_currentRowPosition;
    protected int p_currentColumnPosition;

    [SerializeField] protected float _treshold = 0.0001f;

    //Shooting
    [SerializeField] private float _maxTimeShoot = 5.0f;
    protected float _timeShoot;
    [SerializeField] protected bool _canShoot;

    void Awake()
    {
        p_mainCamera = FindAnyObjectByType<Camera>();
        CameraLimit();
        TimeShootSelection();
    }

    private void Start()
    {
        p_row = _enemyManager._positionMatriz.GetLength(1);
        p_column = _enemyManager._positionMatriz.GetLength(0);
        //_indexListPosition = Random.Range(0, _positionList.Count);
        //_nextPosition = _positionList[_indexListPosition];
    }

    private void Update()
    {
        
    }

    protected void TimeShootSelection()
    {
        _timeShoot = Random.Range(p_shootTimeRest, _maxTimeShoot);
    }

    protected override void Kill()
    {
        enemyKill?.Invoke(this);
        base.Kill();
    }
}
