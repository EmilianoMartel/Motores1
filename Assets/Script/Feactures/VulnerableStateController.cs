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

    //TODO: TP2 - Spelling error/Code in spanish/Code in spanglish
    private void InvensibleState()
    {
        isVulnerable?.Invoke(false);
    }

    private void CallInvulnerability(bool isVulnerable)
    {
        if (isVulnerable)
        {
            InvensibleState();
        }
        else
        {
            VulnerableState();
        }
    //TODO: TP1 - Unused method/variable
    }
}
