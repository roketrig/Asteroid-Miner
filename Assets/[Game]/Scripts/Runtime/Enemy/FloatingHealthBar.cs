using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingHealthBar : MonoBehaviour
{
    [SerializeField] private Renderer healthBarRenderer;
    private Material healthBarMaterial;

    private void Awake()
    {
        if (healthBarRenderer != null)
        {
            healthBarMaterial = healthBarRenderer.material;
        }
    }

    public void UpdateHealthBar(float currentValue, float maxValue)
    {
        if (healthBarMaterial != null)
        {
            float healthNormalized = currentValue / maxValue;
            healthBarMaterial.SetFloat("_Health", healthNormalized);
        }
    }
}
