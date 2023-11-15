using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hazard : MonoBehaviour
{
    [SerializeField] private int _damage = 10;
    private bool _canHazard = true;

    //TODO: TP2 - Unclear name
    public bool canHazard { set { _canHazard = value; } }

    private void OnTriggerEnter2D(Collider2D col)
    {
        HealthPoints hp;
        Debug.Log($"{name} collided with {col.name}");

        if (col.gameObject.TryGetComponent(out hp) && _canHazard)
        {
            Debug.Log($"{name} try damaged {col.name}");
            hp.TakeDamage(_damage);
        }
    }
}
