using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public delegate void EnemyKill(BaseEnemy enemy);
public class BaseEnemy : Character
{
    public EnemyKill enemyKill;

    //Positions
    [SerializeField] private List<Vector2> _positionList= new List<Vector2>();
    [SerializeField] private ViewMapManager _viewMapManager;
    private int _indexListPosition = 0;
    private int _currentIndexPosition;
    private Vector2 _currentPosition;
    private Vector2 _nextPosition;
    private Vector2 _directionToNextPosition;

    [SerializeField] private float _treshold = 0.0001f;
    //One more timeShoot
    protected float _timeShoot;

    void Awake()
    {
        p_mainCamera = FindAnyObjectByType<Camera>();
        CameraLimit();
        TimeShootSelection();
        _viewMapManager.floorPosition += PositionListSpawner;
    }

    private void Start()
    {
        //_indexListPosition = Random.Range(0, _positionList.Count);
        //_nextPosition = _positionList[_indexListPosition];
    }

    private void Update()
    {
        PatrolMovement();
    }

    protected void TimeShootSelection()
    {
        _timeShoot = Random.Range(p_shootTimeRest, 5);
    }

    protected override void Kill()
    {
        enemyKill?.Invoke(this);
        base.Kill();
    }

    protected void RandomIndexPosition()
    {
        _currentIndexPosition = _indexListPosition;
        _indexListPosition = Random.Range(0, _positionList.Count);
        if (_currentIndexPosition == _indexListPosition)
        {
            RandomIndexPosition();
        }
        _nextPosition = _positionList[_indexListPosition];
    }

    protected void PatrolMovement()
    {
        _currentPosition = transform.position;
        _directionToNextPosition = _nextPosition - _currentPosition;
        _directionToNextPosition.Normalize();

        Move(_directionToNextPosition);

        if ((_currentPosition - _nextPosition).magnitude <= _treshold)
        {
            RandomIndexPosition();
        }
    }

    private void PositionListSpawner(Vector2 position)
    {
        _positionList.Add(position);
    }
}
