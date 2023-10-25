using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    //Managers
    [SerializeField] private GameManager _gameManager;
    [SerializeField] private HealthPoints _playerHealthPoints;
    [SerializeField] private LevelManager _levelManager;

    [SerializeField] private TMPro.TMP_Text _waveText;
    [SerializeField] private Image _lifeImage;
    [SerializeField] private List<Sprite> _lifeSpriteList = new List<Sprite>();

    [SerializeField] private Image _endGameImage;
    [SerializeField] private GameObject _panelEndGame;

    //EndGameImages
    [SerializeField] private Sprite _winImage;
    [SerializeField] private Sprite _loseImage;

    private void Awake()
    {
        NullReferenceControll();
        _panelEndGame.SetActive(false);
    }

    private void ShowLife(int actualLife)
    {
        _lifeImage.sprite = _lifeSpriteList[actualLife];
    }

    private void NullReferenceControll()
    {
        if (_lifeImage == null)
        {
            Debug.LogError(message: $"{name}: LifeImage is null\n Check and assigned one\nDisabling component");
            enabled = false;
            return;
        }
        if (_waveText == null)
        {
            Debug.LogError(message: $"{name}: WaveText is null\n Check and assigned one\nDisabling component");
            enabled = false;
            return;
        }
        if (_lifeSpriteList.Count != 7)
        {
            Debug.LogError(message: $"{name}: LifeSprite list should be 6\n Check the list, remember 0 is 0 lifepoints\nDisabling component");
            enabled = false;
            return;
        }
        if (_panelEndGame == null)
        {
            Debug.LogError(message: $"{name}: Panel end game is null \n Check and assigned one\nDisabling component");
            enabled = false;
            return;
        }
        try
        {
            _playerHealthPoints.damaged += ShowLife;
        }
        catch (System.Exception)
        {
            Debug.LogError(message: $"{name}: PlayerHealthPoints is null\n Check and assigned one\nDisabling component");
            enabled = false;
            return;
        }
        try
        {
            _levelManager.showActualWave += ShowActualWave;
        }
        catch (System.Exception)
        {
            Debug.LogError(message: $"{name}: LevelManager is null\n Check and assigned one\nDisabling component");
            enabled = false;
            return;
        }
        try
        {
            _gameManager.endGame += EndGame;
            _gameManager.resetGame += ResetGame;
        }
        catch (System.Exception)
        {
            Debug.LogError(message: $"{name}: GameManager is null\n Check and assigned one\nDisabling component");
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
        _lifeImage.sprite = _lifeSpriteList[_lifeSpriteList.Count - 1];
        _panelEndGame.SetActive(false);
    }
}