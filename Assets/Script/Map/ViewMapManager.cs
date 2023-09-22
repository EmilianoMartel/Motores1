using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public delegate void FloorPosition(Vector2 vector2, int column, int row);
public class ViewMapManager : MonoBehaviour
{
    public enum State
    {
        EmptyFloor,
        Rock,
        Spawner,
        ObjectSpawner,
        Wall
    }

    //Delegates
    public FloorPosition floorPosition;

    public int _row; //High
    public int _column; //Width

    //Table
    [SerializeField] private Grid _grid;
    private State[,] _tableDimention => new State[_column, _row];
    [SerializeField] private GameObject _wallPrefab;
    [SerializeField] private GameObject _floorPrefab;
    private GameObject _spawnMap;
    [SerializeField] private MapDataSO _mapData;
    public ArrayLayout data;

    //FloorList
    [SerializeField] private GameObject _floorListParent;

    //Index
    private int index;

    private void Start()
    {
        if (_column <= 0 || _row <= 0)
        {
            Debug.LogError(message: $"{name}: The column and row is too small \n Check column and row\nDisabling component");
            enabled = false;
            return;
        }
        SpawnTable();
    }

    private void SpawnTable()
    {
        for (int f_row = 0; f_row < _row; f_row++)
        {
            for (int f_column = 0; f_column < _column; f_column++)
            {
                if (f_column == 0 || f_row == 0 || f_column == _column - 1 || f_row == _row - 1)
                {
                    SpawnWalls(f_column, f_row);
                }
                else
                {
                    SpawnFloor(f_column,f_row);
                }
            }
        }
    }

    private void SpawnWalls(int f_column,int f_row)
    {
        _spawnMap = Instantiate(_wallPrefab, transform.position + _grid.CellToWorld(new Vector3Int(f_column, f_row, 10)), Quaternion.identity);
        _spawnMap.transform.parent = transform;

        SpriteRenderer spriteRenderer = _spawnMap.GetComponent<SpriteRenderer>();
        if (f_column == 0 && f_row == 0)
        {
            spriteRenderer.sprite = _mapData.LeftDownWallCorner;
        }
        else if (f_column == _column - 1 && f_row == 0)
        {
            spriteRenderer.sprite = _mapData.RightDownWallCorner;
        }
        else if (f_row == 0)
        {
            index = Random.Range(0, _mapData.DownWallList.Count);
            spriteRenderer.sprite = _mapData.DownWallList[index];
        }
        else if (f_column == 0)
        {
            index = Random.Range(0, _mapData.LeftWallList.Count);
            spriteRenderer.sprite = _mapData.LeftWallList[index];
        }
        else if (f_column == _column - 1)
        {
            index = Random.Range(0, _mapData.RightWallList.Count);
            spriteRenderer.sprite = _mapData.RightWallList[index];
        }
        else if (f_row == _row - 1)
        {
            index = Random.Range(0, _mapData.TopWallList.Count);
            spriteRenderer.sprite = _mapData.TopWallList[index];
        }
    }

    private void SpawnFloor(int f_column, int f_row)
    {
        _spawnMap = Instantiate(_floorPrefab, transform.position + _grid.CellToWorld(new Vector3Int(f_column, f_row, 10)), Quaternion.identity);
        SpriteRenderer spriteRenderer = _spawnMap.GetComponent<SpriteRenderer>();
        index = Random.Range(0, _mapData.FloorList.Count);
        spriteRenderer.sprite = _mapData.FloorList[index];
        floorPosition?.Invoke(_spawnMap.transform.position, f_column - 1, f_row - 1);
        _spawnMap.transform.parent = _floorListParent.transform;
    }
}
