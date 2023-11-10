using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private int _health;

    private void OnTriggerEnter2D(Collider2D col)
    {
        HealthPoints hp;
        Debug.Log($"{name} collided with {col.name}");
        if (col.gameObject.TryGetComponent(out hp))
        {
            Debug.Log($"{name} health {col.name}");
            hp.Health(_health);
        }
    }
}
