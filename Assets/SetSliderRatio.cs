using System;
using UnityEngine;
using UnityEngine.UI;

public class SetSliderRatio : MonoBehaviour
{
    [SerializeField] private Image image;

    public void SetValue(float ratio)
    {
        image.fillAmount = Mathf.Clamp01(ratio);
    }
}