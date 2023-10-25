using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : Character
{
    private Vector3 _inputAttack;
    [SerializeField] private GameManager _gameManger;

    private void OnEnable()
    {
        transform.position = new Vector3(0,0,-5);
    }

    private void Start()
    {
        NullReferenceController();
        SuscriptionsDelegates();
        p_actualTime = 10;
        try
        {
            _gameManger.resetGame += ActivePlayer;
        }
        catch (System.Exception)
        {
            Debug.LogError(message: $"{name}: GameManager is null\n Check and assigned one\nDisabling component");
            enabled = false;
            return;
        }
    }

    void Update()
    {
        PlayerMovement();
        if (p_actualTime > p_shootTimeRest && (_inputAttack.x != 0 || _inputAttack.y != 0) && p_isAttacking == false)
        {
            p_attackDirection = _inputAttack;
            StartCoroutine(Shoot());
            p_actualTime = 0;
        }
        p_actualTime += Time.deltaTime;
    }

    public void SetMoveValue(InputAction.CallbackContext inputContext)
    {
        p_direction = inputContext.ReadValue<Vector2>();
    }

    public void SetShootValue(InputAction.CallbackContext inputContext)
    {
        _inputAttack = inputContext.ReadValue<Vector2>();
    }

    private void PlayerMovement()
    {
        Movement(p_direction);
    }

    private void ActivePlayer()
    {
        gameObject.SetActive(true);
    }
}