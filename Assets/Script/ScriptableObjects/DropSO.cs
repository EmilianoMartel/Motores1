using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

[CreateAssetMenu(fileName = "Drop Data", menuName = "DropData")]
public class DropSO : ScriptableObject
{
    [SerializeField] private GameObject _dropObject;
    [SerializeField] private int _dropRate;

    public GameObject dropObject { get { return _dropObject; } }
    public int dropRate { get { return _dropRate; } }
}
