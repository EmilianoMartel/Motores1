using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShooterManager : MonoBehaviour
{
    [SerializeField] protected List<BulletManager> p_bulletManagerList;
    [SerializeField] protected Character p_character;

    //Index
    protected int p_pointShootIndex = 0;
    protected int p_bulletManagerIndex = 0;

    //BulletDirection
    protected Vector3 p_bulletDirection;

    //ColdDown
    [SerializeField] protected float p_shootTimeRest;

    //Shooting
    [SerializeField] private float _maxTimeShoot = 5.0f;
    protected int _typeShoot;
    [SerializeField] protected bool _canShoot;

    private void Awake()
    {
        if (p_bulletManagerList.Count == 0 || p_bulletManagerList[0] == null)
        {
            Debug.LogError(message: $"{name}: BulletManager is null\n Check and assigned one\nDisabling component");
            enabled = false;
            return;
        }
    }

    private void BulletSelection()
    {
        _typeShoot = Random.Range(0, p_bulletManagerList.Count);
    }

    [ContextMenu("Shoot")]
    public virtual void ShootAction()
    {
        p_bulletManagerList[p_bulletManagerIndex].Shoot(p_bulletDirection);
    }
}
