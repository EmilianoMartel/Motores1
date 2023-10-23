using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelMapData : MonoBehaviour
{
    [SerializeField] private ArrayLayout data;

    public ArrayLayout ArrayLayout { get { return data; } }
}
