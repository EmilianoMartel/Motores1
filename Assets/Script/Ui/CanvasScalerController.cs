using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
public class CanvasScalerController : MonoBehaviour
{
    [SerializeField] private CanvasScaler _canvasScaler;

    private void Update()
    {
        SetAspectRatioMatch();
    }

    private void SetAspectRatioMatch()
    {
        float currentAspectRatio = (float)Screen.height / Screen.width;
        float canvasScalerAspectRatio = _canvasScaler.referenceResolution.y / _canvasScaler.referenceResolution.x;
        if (currentAspectRatio < canvasScalerAspectRatio)
        {
            _canvasScaler.matchWidthOrHeight = 1f;
        }
        else
        {
            _canvasScaler.matchWidthOrHeight = 0f;
        }
    }
}
