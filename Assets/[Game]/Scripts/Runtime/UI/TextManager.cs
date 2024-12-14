using System.Collections;
using TMPro;
using UnityEngine;

public class TextManager : MonoBehaviour
{
    public TextMeshProUGUI amount_Text;
    public float displayDuration = 2f;
    public float delayTime = 1f; 

    public void ShowText(string text)
    {
        StartCoroutine(DisplayTextWithDelay(text)); 
    }

    private IEnumerator DisplayTextWithDelay(string text)
    {
        yield return new WaitForSeconds(delayTime); 

        amount_Text.text = text;
        amount_Text.enabled = true;
        yield return new WaitForSeconds(displayDuration);
        amount_Text.enabled = false;
    }
}
