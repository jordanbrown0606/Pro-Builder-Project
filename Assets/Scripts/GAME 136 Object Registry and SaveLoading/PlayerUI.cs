using System.Collections;
using System.Collections.Generic;
using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    public Slider healthSlider;

    private void OnEnable()
    {
        EventManager.OnUpdateHealthBar += UpdateHealthBar;
    }

    private void OnDisable()
    {
        EventManager.OnUpdateHealthBar -= UpdateHealthBar;
    }

    private void UpdateHealthBar(int current, int max)
    {
        healthSlider.value = current / (float)max;
    }
}
