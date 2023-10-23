using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    [SerializeField] private TMPro.TMP_Text _waveText;
    [SerializeField] private Image _lifeImage;
    [SerializeField] private List<Sprite> _lifeSpriteList = new List<Sprite>();
    [SerializeField] private HealthPoints _playerHealthPoints;
    [SerializeField] private LevelManager _levelManager;
    [SerializeField] private TMPro.TMP_Text _winOrLose;

    private void Awake()
    {
        NullReferenceControll();
        _playerHealthPoints.damaged += ShowLife;
        _levelManager.showActualWave += ShowActualWave;
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
        if (_playerHealthPoints == null)
        {
            Debug.LogError(message: $"{name}: PlayerHealthPoints is null\n Check and assigned one\nDisabling component");
            enabled = false;
            return;
        }
        if (_lifeSpriteList.Count != 7)
        {
            Debug.LogError(message: $"{name}: LifeSprite list should be 6\n Check the list, remember 0 is 0 lifepoints\nDisabling component");
            enabled = false;
            return;
        }
        if (_levelManager == null)
        {
            Debug.LogError(message: $"{name}: LevelManager is null\n Check and assigned one\nDisabling component");
            return;
        }
    }

    private void ShowActualWave(int numWave)
    {
        _waveText.text = numWave.ToString();
    }
}
