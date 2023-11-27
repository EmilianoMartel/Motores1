using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void NextLevel();
public class Stair : MonoBehaviour
{
    public NextLevel nextLevel;

    public bool isActiveStair = true;

    private void OnTriggerEnter2D(Collider2D col)
    {
        Debug.Log($"{name} collided with {col.name}");
        nextLevel?.Invoke();
    }

    [ContextMenu("Start Level")]
    private void StartLevel()
    {
        nextLevel?.Invoke();
    }
}