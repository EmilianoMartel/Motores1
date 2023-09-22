using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hazard : MonoBehaviour
{
    [SerializeField] private int _damage = 10;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.GetComponent<HealthPoints>())
        {
            HealthPoints healthPoints = col.gameObject.GetComponent<HealthPoints>();
            healthPoints.GetDamage(_damage);
        }
    }
}
