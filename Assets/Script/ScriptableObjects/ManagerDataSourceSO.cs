using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ManagerDataSource", menuName = "ManagerDataSource")]
public class ManagerDataSourceSO : ScriptableObject
{
    private DropPoolManager _dropManager;
    private LevelManager _levelManager;
    private GameManager _gameManager;
    private EnemyManager _enemyManager;
    private UiManager _uiManager;
    private ViewMapManager _viewMapManager;

    public DropPoolManager dropManager { get => _dropManager; set => _dropManager = value; }
    public LevelManager levelManager { get => _levelManager; set => _levelManager = value; }
    public GameManager gameManager { get => _gameManager; set => _gameManager = value; }
    public EnemyManager enemyManager { get => _enemyManager; set => _enemyManager = value; }
    public UiManager uiManager { get => _uiManager; set => _uiManager = value; }
    public ViewMapManager viewMapManager { get => _viewMapManager; set => _viewMapManager = value; }
}
