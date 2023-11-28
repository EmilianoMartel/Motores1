using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Potion : MonoBehaviour
{
    [SerializeField] private int _hearts;

    private void OnCollisionEnter2D(Collision2D col)
    {
        HealthPoints hp;
        Debug.Log($"{name} collided with {col.gameObject.name}");
        if (col.gameObject.TryGetComponent(out hp))
        {
            Debug.Log($"{name} change max life {col.gameObject.name}");
            hp.ChangeFullLife(_hearts * 2);
            gameObject.SetActive(false);
        }
    }
}
