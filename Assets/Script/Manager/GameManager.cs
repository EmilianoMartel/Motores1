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

    [SerializeField] private HealthPoints _playerHealth;
    [SerializeField] private LevelManager _levelManager;

    [SerializeField] private GameObject _gamePlay;

    private void Awake()
    {
        _playerHealth.dead += PlayerDeath;
        _levelManager.endBossFight += BossDeath;
    }

    private void PlayerDeath()
    {
        _gamePlay.SetActive(false);
        endGame?.Invoke(false);
    }

    private void BossDeath()
    {
        _gamePlay.SetActive(false);
        endGame?.Invoke(true);
    }

    private void ResetGame()
    {
        resetGame?.Invoke();
    }
}
