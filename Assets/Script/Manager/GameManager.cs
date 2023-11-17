using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void ResetGame();
public delegate void EndGame(bool playerWins);
public class GameManager : MonoBehaviour
{
    //Delegate
    public EndGame endGame;
    public ResetGame resetGame;

    [SerializeField] private ManagerDataSourceSO _dataSourceSO;
    [SerializeField] private HealthPoints _playerHealth;
    [SerializeField] private LevelManager _levelManager;
    [SerializeField] private EnemyManager _enemyManager;
    [SerializeField] private DropPoolManager _dropManager;

    [SerializeField] private GameObject _gamePlay;
    [SerializeField] private float _endGameDelay = 1f;

    private void OnEnable()
    {
        //Subscription to delegates
        _levelManager.endBossFight += BossDeath;
        _playerHealth.dead += PlayerDeath;
    }

    private void OnDisable()
    {
        _levelManager.endBossFight -= BossDeath;
        _playerHealth.dead -= PlayerDeath;
    }

    private void Awake()
    {
        NullController();
        _dataSourceSO.gameManager = this;
    }

    private void NullController()
    {
        //TODO: TP2 - Fix - Avoid Try-catch blocks
        if (_playerHealth == null)
        {
            Debug.LogError(message: $"{name}: PlayerHealth is null\n Check and assigned one\nDisabling component");
            enabled = false;
            return;
        }
        if (_levelManager == null)
        {
            Debug.LogError(message: $"{name}: LevelManagers is null\n Check and assigned one\nDisabling component");
            enabled = false;
            return;
        }
        if (!_dataSourceSO)
        {
            Debug.LogError(message: $"{name}: DataSource is null\n Check and assigned one\nDisabling component");
            enabled = false;
            return;
        }
    }

    private void PlayerDeath()
    {
        StartCoroutine(EndGame(false));
    }

    private void BossDeath()
    {
        StartCoroutine(EndGame(true));
    }

    public void ResetGame()
    {
        resetGame?.Invoke();
        _gamePlay.SetActive(true);
    }

    private IEnumerator EndGame(bool didPlayerWin)
    {
        yield return new WaitForSeconds(_endGameDelay);
        endGame?.Invoke(didPlayerWin);
        _gamePlay.SetActive(false);
    //TODO: TP2 - Unclear name - didPlayerWin / playerWon
    }
}
