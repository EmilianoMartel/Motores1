using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void IsVulnerable(bool isVulnerable);
public class VulnerableStateController : MonoBehaviour
{
    //Delegates
    public IsVulnerable isVulnerable;

    private void VulnerableState()
    {
        isVulnerable?.Invoke(true);
    }

    private void InvensibleState()
    {
        isVulnerable?.Invoke(false);
    }
}
