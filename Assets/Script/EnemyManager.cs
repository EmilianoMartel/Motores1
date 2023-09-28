using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    //Managers
    [SerializeField] private ViewMapManager _viewMapManager;
    [SerializeField] private LevelManager _levelManager;

    private int _row = 1;
    private int _column = 1;
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
        if (_levelManager == null)
        {
            Debug.LogError(message: $"{name}: LevelManager is null\n Check and assigned one\nDisabling component");
            enabled = false;
            return;
        }
        _row = _viewMapManager._row - 2;
        _column = _viewMapManager._column - 2;
        _positionMatriz = new Vector2[_column, _row];

        //Delegates suscriptions
        _viewMapManager.floorPosition += PositionListSpawner;
        _levelManager.spawnEnemy += SpawnEnemyLogic;
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
    private void SpawnEnemyLogic(int f_column = 0, int f_row = 0)
    {
        CheckEnemyListActive();
        SpawnRandomEnemy(f_column, f_row);
    }

    [ContextMenu("SpawnRandomEnemy")]
    private void SpawnRandomEnemy(int f_column = 0, int f_row = 0)
    {
        int index = RandomIndexEnemy();
        while (_enemyList[index].activateEnemy)
        {
            index = RandomIndexEnemy();
        }
        _enemyList[index].gameObject.transform.position = _positionMatriz[f_column,f_row];
        _enemyList[index].activateEnemy = true;
        _enemyList[index].gameObject.SetActive(true);
    }

    private int RandomIndexEnemy()
    {
        return Random.Range(0, _enemyList.Count);
    }

    private void CheckEnemyListActive()
    {
        int count = 0;
        foreach (BaseEnemy enemy in _enemyList)
        {
            if (enemy.activateEnemy)
            {
                count++;
            }
        }
        if (count == _enemyList.Count)
        {
            _enemy = Instantiate(_enemyListPrefab[Random.Range(0, _enemyListPrefab.Count)], transform.position, Quaternion.identity);
            _enemy.GetEnemyManager(this);
            _enemyList.Add(_enemy);
            _enemy.activateEnemy = false;
            _enemy.gameObject.SetActive(false);
        }
    }
}
