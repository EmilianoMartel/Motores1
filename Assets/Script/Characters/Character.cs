using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public abstract class Character : MonoBehaviour
{
    //Delagates
    public Action<bool> isDeadEvent;
    public Action<bool> isAttackingEvent;
    public Action<bool> isDamagedEvent;

    [SerializeField] protected HealthPoints p_healthPoints;
    [SerializeField] protected CharacterView p_characterView;
    [SerializeField] protected VulnerableStateController p_stateController;
    [SerializeField] protected Hazard p_hazard;
    [SerializeField] protected CharacterSO p_characterData;

    //Dead
    [SerializeField] protected float p_deadDelay = 1.0f;
    protected bool p_isDead = false;

    //Movement
    protected Vector3 p_direction;
    protected Vector3 p_attackDirection;

    //Shoot
    [SerializeField] protected float p_shootTimeRest;
    [SerializeField] protected bool p_isAttacking = false;
    [SerializeField] protected float p_shootDelay = 1.0f;
    [SerializeField] protected float p_delayBetweenShoots = 1.0f;
    [SerializeField] protected int p_multipleShoot = 2;
    [SerializeField] protected BulletManager p_bulletManager;
    [SerializeField] protected Transform p_pointShoot;
    private Vector3 _realPointShoot;
    [SerializeField] private const float DIFF_X = 1.0f;
    [SerializeField] private const float DIFF_Y = 1.0f;

    protected float p_actualTime = 0;
    protected static float timeEndGame = 2f;

    public Vector3 attackDirection { get { return p_attackDirection; } set { p_attackDirection = value; } }
    public Vector3 direction { get { return p_direction; } }
    public int multipleShootValue { get { return p_multipleShoot; } set { p_multipleShoot = value; } }
    public BulletManager bulletManager { set { p_bulletManager = value; } }

    private void OnEnable()
    {
        p_hazard.canHazard = true;
        p_isDead = false;
        isDeadEvent?.Invoke(false);

        //SuscriptionDelegates
        p_healthPoints.dead += Kill;
    }

    private void OnDisable()
    {
        p_healthPoints.dead -= Kill;
    }

    private void Start()
    {
        NullReferenceController();
<<<<<<< HEAD
        //TODO: TP2 - Should be done in OnEnable
        SuscriptionsDelegates();
=======
        p_healthPoints.maxLife = p_characterData.maxLife;
>>>>>>> Martel/main
    }

    protected void NullReferenceController()
    {
        if (p_hazard == null)
        {
            Debug.LogError(message: $"{name}: Hazard is null\n Check and assigned one\nDisabling component");
            enabled = false;
            return;
        }
        if (p_healthPoints == null)
        {
            Debug.LogError(message: $"{name}: HealthPoints is null\n Check and assigned one\nDisabling component");
            enabled = false;
            return;
        }
        if (p_characterView == null)
        {
            Debug.LogError(message: $"{name}: CharacterView is null\n Check and assigned one\nDisabling component");
            enabled = false;
            return;
        }
        if (p_pointShoot == null)
        {
            Debug.LogError(message: $"{name}: PointShoot is null\n Check and assigned one\nDisabling component");
            enabled = false;
            return;
        }
<<<<<<< HEAD
    }

    //TODO: TP2 - Spelling error/Code in spanish/Code in spanglish
    protected void SuscriptionsDelegates()
    {
        p_healthPoints.dead += Kill;
=======
        if (p_characterData == null)
        {
            Debug.LogError(message: $"{name}: Character Data is null\n Check and assigned one\nDisabling component");
            enabled = false;
            return;
        }
>>>>>>> Martel/main
    }

    protected void Movement(Vector2 direction)
    {
        transform.Translate(direction * p_characterData.speed * Time.deltaTime);
    }

    protected virtual void Kill()
    {
        isDeadEvent?.Invoke(true);
        p_isDead = true;
        StartCoroutine(Dead());
    }

    private IEnumerator Dead()
    {
        p_hazard.canHazard = false;
        yield return new WaitForSeconds(p_deadDelay);
        gameObject.SetActive(false);
        p_isDead = false;
        isDeadEvent?.Invoke(false);
        p_healthPoints.maxLife = p_characterData.maxLife;
    }

    protected IEnumerator Shoot()
    {
        p_isAttacking = true;
        isAttackingEvent?.Invoke(true);
        ElectionSpawnShoot(p_attackDirection);
        yield return new WaitForSeconds(p_shootDelay);
        p_bulletManager.Shoot(p_attackDirection, _realPointShoot);
        p_isAttacking = false;
        isAttackingEvent?.Invoke(false);
    }

    protected IEnumerator Shoot(int cantShoot, List<Vector2> directionsList)
    {
        p_isAttacking = true;
        isAttackingEvent?.Invoke(true);
        yield return new WaitForSeconds(p_shootDelay);
        //TODO: TP2 - Spelling error/Code in spanish/Code in spanglish
        for (int i = 0; i < cantShoot; i++)
        {
            ElectionSpawnShoot(directionsList[i]);
            p_bulletManager.Shoot(directionsList[i], _realPointShoot);
            yield return new WaitForSeconds(p_delayBetweenShoots);
        }
        p_isAttacking = false;
        isAttackingEvent?.Invoke(false);
    }

    private void ElectionSpawnShoot(Vector3 direction)
    {
        if (direction.x > 0)
        {
            _realPointShoot = p_pointShoot.position;
            _realPointShoot += new Vector3(DIFF_X, 0);
        }
        else if (direction.x < 0)
        {
            _realPointShoot = p_pointShoot.position;
            _realPointShoot += new Vector3(-DIFF_X, 0);
        }
        else if (direction.y < 1)
        {
            _realPointShoot = p_pointShoot.position;
            _realPointShoot += new Vector3(0, -DIFF_Y);
        }
        else
        {
            _realPointShoot = p_pointShoot.position;
            _realPointShoot += new Vector3(0, DIFF_Y);
        }
    }
}
