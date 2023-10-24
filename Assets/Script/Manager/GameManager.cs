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
    [SerializeField] private float _endGameDelay = 1f;

    private void Awake()
    {
        _playerHealth.dead += PlayerDeath;
        _levelManager.endBossFight += BossDeath;
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

    private IEnumerator EndGame(bool isPlayerWin)
    {
        yield return new WaitForSeconds(_endGameDelay);
        endGame?.Invoke(isPlayerWin);
        _gamePlay.SetActive(false);
    }
}
