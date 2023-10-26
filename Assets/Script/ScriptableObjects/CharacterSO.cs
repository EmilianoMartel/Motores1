using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Character Data", menuName = "CharacterData")]
public class CharacterScriptableObject : ScriptableObject
{
    //TODO finish the SO for Characters, and create the logic
    [SerializeField] private int _maxLife;
    [SerializeField] private float _speed;
    [SerializeField] private float _shootTimeRest;
    [SerializeField] private float _shootAnimDelay;

}
