using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SliderController : MonoBehaviour
{
    public TextMeshProUGUI valueText;
    int progress = 0;
    public Slider slider;

    public void OnSliderChanged(float value)
    {
        valueText.text = value.ToString();
    }
    public void Update()
    {
        progress++;
        slider.value = progress;
    }
}
