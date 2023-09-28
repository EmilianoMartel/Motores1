using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void SpawnEnemy(int column, int row);
public class LevelManager : MonoBehaviour
{
    const int DIFF_MATRIX_ENEMY = 2;

    //Delegates
    public SpawnEnemy spawnEnemy;

    //Level variables for dificult
    [SerializeField] private int _minEnemy = 1;
    [SerializeField] private float _levelDificult = 1.0f;
    [SerializeField] private List<GameObject> _dataPrefabList;

    //Managers
    [SerializeField] private EnemyManager _enemyManager;

    private List<ArrayLayout> _dataList = new List<ArrayLayout>();
    private ArrayLayout _dataLayout;

    private void Start()
    {
        if (_enemyManager == null)
        {
            Debug.LogError(message: $"{name}: Enemy Manager is null \n Check and assigned one\nDisabling component");
            enabled = false;
            return;
        }
        if (_dataPrefabList.Count == 0)
        {
            Debug.LogError(message: $"{name}: DataPrefabList is null \n Check and assigned one\nDisabling component");
            enabled = false;
            return;
        }
        SetDataList();
    }

    private void SetDataList()
    {
        foreach (GameObject data in _dataPrefabList)
        {
            if (data == null)
            {
                Debug.LogError(message: $"{name}: GameObject is null\n Check and assigned one\nDisabling component");
                enabled = false;
                return;
            }
            if (data.gameObject.GetComponent<LevelMapData>() == null)
            {
                Debug.LogError(message: $"{name}: {data.name} dont have a MapData\n Check and assigned one\nDisabling component");
                enabled = false;
                return;
            }
            else
            {
                _dataList.Add(data.gameObject.GetComponent<LevelMapData>().ArrayLayout);
            }
        }
    }

    [ContextMenu("StartLevel")]
    private void StartLevel()
    {
        ArrayLayout.State state;
        RandomLevel();
        for (int f_row = _dataLayout.rows.Length - 1; f_row >= 0; f_row--)
        {
            for (int f_column = 0; f_column < _dataLayout.rows[f_row].row.Length; f_column++)
            {
                state = _dataLayout.rows[f_row].row[f_column];
                switch (state)
                {
                    case ArrayLayout.State.EmptyFloor:
                        break;
                    case ArrayLayout.State.Rock:
                        break;
                    case ArrayLayout.State.Spawner:
                        spawnEnemy?.Invoke(f_column - DIFF_MATRIX_ENEMY, f_row - DIFF_MATRIX_ENEMY);
                        break;
                    case ArrayLayout.State.ObjectSpawner:
                        break;
                    case ArrayLayout.State.WallRight:
                        break;
                    case ArrayLayout.State.WallLeft:
                        break;
                    case ArrayLayout.State.WallDown:
                        break;
                    case ArrayLayout.State.WallTop:
                        break;
                    case ArrayLayout.State.WallLeftDown:
                        break;
                    case ArrayLayout.State.WallRightDown:
                        break;
                }
            }
        }
    }

    private void RandomLevel()
    {
        int index = Random.Range(0, _dataList.Count);
        _dataLayout = _dataList[index];
    }
}
