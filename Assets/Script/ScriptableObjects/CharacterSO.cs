using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Character Data", menuName = "CharacterData")]
public class CharacterSO : ScriptableObject
{
    [SerializeField] private int _maxLife;
    [SerializeField] private float _speed;
    [SerializeField] private float _shootTimeRest;
    [SerializeField] private float _shootAnimDelay;

    public int maxLife { get { return _maxLife; } }
    public float speed { get { return _speed; } }
    public float shootTimeRest { get { return _shootTimeRest; } set { _shootTimeRest = value; } }
    public float shootAnimDelay { get { return _shootAnimDelay; } }
}
