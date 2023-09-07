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
    private List<BaseEnemy> _enemyList;
    private BaseEnemy _enemy;

    private void Awake()
    {
        _row = _viewMapManager._row - 2;
        _column = _viewMapManager._column - 2;
        _positionMatriz = new Vector2[_column, _row];
        _viewMapManager.floorPosition += PositionListSpawner;
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

    private void InvokeEnemy()
    {

    }
}
