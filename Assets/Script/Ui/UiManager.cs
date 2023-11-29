using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    [SerializeField] private float _waitForManager = 1f;
    
    //Managers
    [SerializeField] private ManagerDataSourceSO _dataSource;
    private GameManager _gameManager;
    [SerializeField] private HealthPoints _playerHealthPoints;
    private LevelManager _levelManager;
    private PausingLogic _pausingLogic;

    [SerializeField] private TMPro.TMP_Text _waveText;

    [SerializeField] private Image _endGameImage;
    [SerializeField] private GameObject _panelEndGame;
    [SerializeField] private GameObject _chargeScreen;
    [SerializeField] private GameObject _pausedScreen;
    [SerializeField] private GameObject _buttonLayout;

    //EndGameImages
    [SerializeField] private Sprite _winImage;
    [SerializeField] private Sprite _loseImage;

    private void OnEnable()
    {
        if (_levelManager)
        {
            _levelManager.showActualWave += ShowActualWave;
        }
        if (_gameManager)
        {
            _gameManager.endGame += EndGame;
            _gameManager.resetGame += ResetGame;
        }
        if (_pausingLogic)
        {
            _pausingLogic.isPausingEvent += PausingEvent;
        }
    }

    private void OnDisable()
    {
        if (_levelManager)
        {
            _levelManager.showActualWave -= ShowActualWave;
        }
        if (_gameManager)
        {
            _gameManager.endGame -= EndGame;
            _gameManager.resetGame -= ResetGame;
        }
        if (_pausingLogic)
        {
            _pausingLogic.isPausingEvent -= PausingEvent;
        }
    }

    private void Awake()
    {
        NullReferenceController();
        _panelEndGame.SetActive(false);
        _pausedScreen.SetActive(false); 
        _buttonLayout.SetActive(false);
        _dataSource.uiManager = this;
        StartCoroutine(SetManagers());
    }

    private IEnumerator SetManagers()
    {
        yield return new WaitForSeconds(_waitForManager);
        if (_dataSource.gameManager)
        {
            _gameManager = _dataSource.gameManager;
            _gameManager.endGame += EndGame;
            _gameManager.resetGame += ResetGame;
        } 
        if (_dataSource.levelManager)
        {
            _levelManager = _dataSource.levelManager;
            _levelManager.showActualWave += ShowActualWave;
        }
        if (_dataSource.pausingLogic)
        {
            _pausingLogic = _dataSource.pausingLogic;
            _pausingLogic.isPausingEvent += PausingEvent;
        }

        _chargeScreen.SetActive(false);
    }

    //TODO: TP2 - Unclear name(Done)
    private void NullReferenceController()
    {
        if (!_buttonLayout)
        {
            Debug.LogError(message: $"{name}: Button Layout is null\n Check and assigned one\nDisabling component");
            enabled = false;
            return;
        }
        if (!_pausedScreen)
        {
            Debug.LogError(message: $"{name}: Paused Screen is null\n Check and assigned one\nDisabling component");
            enabled = false;
            return;
        }
        if (!_chargeScreen)
        {
            Debug.LogError(message: $"{name}: Charge Screen is null\n Check and assigned one\nDisabling component");
            enabled = false;
            return;
        }
        if (_waveText == null)
        {
            Debug.LogError(message: $"{name}: WaveText is null\n Check and assigned one\nDisabling component");
            enabled = false;
            return;
        }
        if (_panelEndGame == null)
        {
            Debug.LogError(message: $"{name}: Panel end game is null \n Check and assigned one\nDisabling component");
            enabled = false;
            return;
        //TODO: TP2 - Fix - Avoid try-catch blocks (Done)
        }
        if (!_playerHealthPoints)
        {
            Debug.LogError(message: $"{name}: PlayerHealthPoints is null\n Check and assigned one\nDisabling component");
            enabled = false;
            return;
        }
        if (!_dataSource)
        {
            Debug.LogError(message: $"{name}: DataSource is null\n Check and assigned one\nDisabling component");
            enabled = false;
            return;
        }
    }

    private void ShowActualWave(int numWave)
    {
        _waveText.text = numWave.ToString();
    }

    private void EndGame(bool playerWins)
    {
        _panelEndGame.SetActive(true);
        _buttonLayout.SetActive(true);
        if (playerWins)
        {
            _endGameImage.sprite = _winImage;
        }
        else
        {
            _endGameImage.sprite = _loseImage;
        }
    }

    private void ResetGame()
    {
        _waveText.text = "0";
        _panelEndGame.SetActive(false);
        _buttonLayout.SetActive(false);
    }

    private void PausingEvent()
    {
        if (_pausedScreen.activeSelf)
        {
            _pausedScreen.SetActive(false);
            _buttonLayout.SetActive(false);
            Time.timeScale = 1f;
        }
        else
        {
            _pausedScreen.SetActive(true);
            _buttonLayout.SetActive(true);
            Time.timeScale = 0f;
        }
    }
}