using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class VulnerableStateController : MonoBehaviour
{
    //Delegates
    public Action<bool> isVulnerable;

    private void VulnerableState()
    {
        isVulnerable?.Invoke(true);
    }

    //TODO: TP2 - Spelling error/Code in spanish/Code in spanglish(DONE)
    private void InvincibilityState()
    {
        isVulnerable?.Invoke(false);
    }

    public void InvulnerabilityCall(bool isVulnerableInt)
    {
        isVulnerable?.Invoke(isVulnerableInt);
    //TODO: TP1 - Unused method/variable(DONE)
    }
}
