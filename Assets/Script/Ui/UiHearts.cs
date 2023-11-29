using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class UiHearts : MonoBehaviour
{
    [SerializeField] private List<Sprite> _spritesList = new List<Sprite>();
    [SerializeField] private Image _heartView;

    private void Awake()
    {
        if (_spritesList.Count > 3 || _spritesList.Count <= 0)
        {
            Debug.LogError(message: $"{name}: Sprite list is null\n Check and assigned one, remember the position 0 is full heart, position 3 is empty heart\nDisabling component");
            enabled = false;
            return;
        }
    }

    private void Start()
    {
        _heartView.sprite = _spritesList[0];
    }

    public void ChangeLifeSprite(int index)
    {
        if (index < 0 || index > _spritesList.Count - 1)
        {
            Debug.LogError($"{name}: index is out off range. Check call");
            return;
        }
        _heartView.sprite = _spritesList[index];
    }
}
