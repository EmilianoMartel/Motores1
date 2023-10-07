using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void UpState();
public delegate void DownState();

public class TowerView : CharacterView
{
    //Delegates
    public UpState upState;
    public DownState downState;
    private bool _isVulnerable;

    //Parameters
    [SerializeField] private string _animatorParameterIsVulnerable = "isVulnerable";

    private void Update()
    {
        p_animator.SetBool(_animatorParameterIsVulnerable, _isVulnerable);
    }

    private void UpState()
    {
        upState?.Invoke();
        _isVulnerable = true;
    }

    private void DownState()
    {
        downState?.Invoke();
        _isVulnerable = false;
    }
}
