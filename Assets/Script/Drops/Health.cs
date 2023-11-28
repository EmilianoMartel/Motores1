using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class Health : MonoBehaviour
{
    [SerializeField] private int _health;

    private void OnCollisionEnter2D(Collision2D col)
    {
        HealthPoints hp;
        Debug.Log($"{name} collided with {col.gameObject.name}");
        if (col.gameObject.TryGetComponent(out hp))
        {
            Debug.Log($"{name} health {col.gameObject.name}");
            if (hp.Healing(_health))
            {
                gameObject.SetActive(false);
            }
            else
            {
                Vector3 move = transform.position - col.gameObject.transform.position;
                transform.position += move.normalized * Time.deltaTime;
            }
        }
    }
}
