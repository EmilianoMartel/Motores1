using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PausingLogic : MonoBehaviour
{
    public Action isPausingEvent;
    [SerializeField] private ManagerDataSourceSO _dataSource;

    private void Awake()
    {
        if (!_dataSource)
        {
            Debug.LogError($"{name}: DataSource is null\n Check and assigned one\nDisabling component.");
            enabled = false;
            return;
        }
        _dataSource.pausingLogic = this;
    }

    public void PausingFunction(InputAction.CallbackContext inputContext)
    {
        if (inputContext.started)
        {
            isPausingEvent?.Invoke();
        }
    }
}
