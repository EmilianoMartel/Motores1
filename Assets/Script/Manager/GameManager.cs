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
    private LevelManager _levelManager;
    private EnemyManager _enemyManager;
    private DropPoolManager _dropManager;

    [SerializeField] private GameObject _gamePlay;
    [SerializeField] private float _endGameDelay = 1f;

    private void OnEnable()
    {
        //Subscription to delegates
        if (_dataSourceSO.levelManager)
        {
            _levelManager = _dataSourceSO.levelManager;
            _levelManager.endBossFight += BossDeath;
        }
        if (_playerHealth)
        {
            _playerHealth.dead += PlayerDeath;
        }
        
    }

    private void OnDisable()
    {
        if (_levelManager)
        {
            _levelManager.endBossFight -= BossDeath;
        }
        _playerHealth.dead -= PlayerDeath;
    }

    private void Awake()
    {
        NullReferenceController();
        _dataSourceSO.gameManager = this;
    }

    private void NullReferenceController()
    {
        //TODO: TP2 - Fix - Avoid Try-catch blocks
        if (_playerHealth == null)
        {
            Debug.LogError(message: $"{name}: PlayerHealth is null\n Check and assigned one\nDisabling component");
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
    //TODO: TP2 - Unclear name - didPlayerWin / playerWon (DONE)
    }
}
