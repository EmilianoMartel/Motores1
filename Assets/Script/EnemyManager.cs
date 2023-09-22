using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] private ViewMapManager _viewMapManager;
    [SerializeField] private int _row = 1;
    [SerializeField] private int _column = 1;
    public Vector2[,] _positionMatriz;
    

    //EnemyList
    [SerializeField] private List<BaseEnemy> _enemyListPrefab;
    private List<BaseEnemy> _enemyList = new List<BaseEnemy>();
    private BaseEnemy _enemy;

    private void Awake()
    {
        if (_viewMapManager == null)
        {
            Debug.LogError(message: $"{name}: ViewManager is null\n Check and assigned one\nDisabling component");
            enabled = false;
            return;
        }
        _row = _viewMapManager._row - 2;
        _column = _viewMapManager._column - 2;
        _positionMatriz = new Vector2[_column, _row];
        _viewMapManager.floorPosition += PositionListSpawner;
    }

    private void Start()
    {
        InvokeEnemyList();
    }

    private void PositionListSpawner(Vector2 position, int column, int row)
    {
        if (column > _column || row > _row || column < 0 || row < 0)
        {
            Debug.LogError(message:$"{name}: Out of range \n Check column and row\nDisabling component");
            enabled = false;
            return;
        }
        _positionMatriz[column,row] = position;
    }

    [ContextMenu("InvokeEnemyList")]
    private void InvokeEnemyList()
    {
        for (int i = 0; i < _enemyListPrefab.Count; i++)
        {
            _enemy = Instantiate(_enemyListPrefab[i], transform.position, Quaternion.identity);
            _enemy.GetEnemyManager(this);
            _enemyList.Add(_enemy);
            _enemy.activateEnemy = false;
            _enemy.gameObject.SetActive(false);
        }
    }

    [ContextMenu("SpawnEnemy")]
    private void SpawnEnemy()
    {
        _enemyList[0].activateEnemy = true;
        _enemyList[0].gameObject.SetActive(true);
    }
}
