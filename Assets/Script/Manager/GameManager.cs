using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private HealthPoints _playerHealth;
    [SerializeField] private LevelManager _levelManager;

    private void Awake()
    {
        _playerHealth.death += PlayerDeath;
        _levelManager.endBossFight += BossDeath;
    }

    private void PlayerDeath()
    {
        Debug.Log("player lose");
    }

    private void BossDeath()
    {
        Debug.Log("player win");
    }
}
