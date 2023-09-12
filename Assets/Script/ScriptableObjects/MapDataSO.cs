using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewMap Data", menuName = "NewMap")]
public class MapDataSO : ScriptableObject
{
    [SerializeField] private List<Sprite> _topWallList;
    [SerializeField] private List<Sprite> _rightWallList;
    [SerializeField] private List<Sprite> _leftWallList;
    [SerializeField] private List<Sprite> _downWallList;
    [SerializeField] private List<Sprite> _floorList;
    [SerializeField] private Sprite _leftDownWallCorner;
    [SerializeField] private Sprite _rightDownWallCorner;

    public List<Sprite> TopWallList { get { return _topWallList; } }
    public List<Sprite> RightWallList { get { return _rightWallList; } }
    public List<Sprite> LeftWallList { get { return _leftWallList; } }
    public List<Sprite> DownWallList { get { return _downWallList; } }
    public List<Sprite> FloorList { get { return _floorList; } }
    public Sprite LeftDownWallCorner  { get { return _leftDownWallCorner; } }
    public Sprite RightDownWallCorner { get { return _rightDownWallCorner; } }
}
