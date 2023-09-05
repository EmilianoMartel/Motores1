using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void FloorPosition(Vector2 vector2);
public class ViewMapManager : MonoBehaviour
{
    public FloorPosition floorPosition;

    [SerializeField] private int _row; //High
    [SerializeField] private int _column; //Width
    private GameObject[,] _tableDimention => new GameObject[_row, _column];
    [SerializeField] private List<GameObject> _topWallList;
    [SerializeField] private List<GameObject> _rightWallList;
    [SerializeField] private List<GameObject> _leftWallList;
    [SerializeField] private List<GameObject> _downWallList;
    [SerializeField] private List<GameObject> _floorList;
    [SerializeField] private GameObject _leftDownWallCorner;
    [SerializeField] private GameObject _rightDownWallCorner;
    private GameObject _wall;

    //Index
    private int index;

    private void Start()
    {
        SpawnTable();
    }

    private void SpawnTable()
    {
        for (int f_row = 0; f_row < _row; f_row++)
        {
            for (int f_column = 0; f_column < _column; f_column++)
            {
                if (f_column == 0 && f_row == 0)
                {
                    _wall = Instantiate(_leftDownWallCorner, transform.position + new Vector3(f_column, f_row, 10), Quaternion.identity);
                    _wall.transform.parent = transform;
                }
                else if (f_column == _column - 1 && f_row == 0)
                {
                    _wall = Instantiate(_rightDownWallCorner, transform.position + new Vector3(f_column, f_row, 10), Quaternion.identity);
                    _wall.transform.parent = transform;
                }
                else if (f_row == 0)
                {
                    index = Random.Range(0,_downWallList.Count);
                    _wall = Instantiate(_downWallList[index], transform.position + new Vector3(f_column, f_row, 10), Quaternion.identity);
                    _wall.transform.parent = transform;
                }
                else if (f_column == 0)
                {
                    index = Random.Range(0, _leftWallList.Count);
                    _wall = Instantiate(_leftWallList[index], transform.position + new Vector3(f_column, f_row, 10), Quaternion.identity);
                    _wall.transform.parent = transform;
                }
                else if (f_column == _column - 1)
                {
                    index = Random.Range(0, _rightWallList.Count);
                    _wall = Instantiate(_rightWallList[index], transform.position + new Vector3(f_column, f_row, 10), Quaternion.identity);
                    _wall.transform.parent = transform;
                }
                else if(f_row == _row - 1)
                {
                    index = Random.Range(0, _topWallList.Count);
                    _wall = Instantiate(_topWallList[index], transform.position + new Vector3(f_column, f_row, 10), Quaternion.identity);
                    _wall.transform.parent = transform;
                }
                else
                {
                    _wall = Instantiate(_floorList[index], transform.position + new Vector3(f_column, f_row, 10), Quaternion.identity);
                    floorPosition?.Invoke(_wall.transform.position);
                    _wall.transform.parent = transform;
                }
            }
        }
    }
}
